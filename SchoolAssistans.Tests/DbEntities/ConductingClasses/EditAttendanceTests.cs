using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using SchoolAssistant.DAL.Models.Attendance;
using SchoolAssistant.DAL.Models.Lessons;
using SchoolAssistant.DAL.Models.Rooms;
using SchoolAssistant.DAL.Models.Staff;
using SchoolAssistant.DAL.Models.StudentsOrganization;
using SchoolAssistant.DAL.Models.StudentsParents;
using SchoolAssistant.DAL.Models.Subjects;
using SchoolAssistant.DAL.Repositories;
using SchoolAssistant.Infrastructure.Enums.Attendance;
using SchoolAssistant.Infrastructure.Models.ConductingClasses.ConductLesson;
using SchoolAssistant.Logic.ConductingClasses.ConductLesson;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolAssistans.Tests.DbEntities.ConductingClasses
{
    internal class EditAttendanceTests : BaseDbEntitiesTests
    {
        private IRepository<OrganizationalClass> _orgClassRepo = null!;
        private IRepository<Teacher> _teacherRepo = null!;
        private IRepositoryBySchoolYear<PeriodicLesson> _periLessonRepo = null!;
        private IRepositoryBySchoolYear<Lesson> _lessonRepo = null!;

        private IEditAttendanceService _service = null!;

        private OrganizationalClass _orgClass1 = null!;
        private OrganizationalClass _orgClass2 = null!;

        protected override async Task CleanDataAfterEveryTestAsync()
        {
            await TestDatabase.ClearDataAsync<Presence>();
            await TestDatabase.ClearDataAsync<Lesson>();
            await TestDatabase.ClearDataAsync<PeriodicLesson>();
            await TestDatabase.ClearDataAsync<Teacher>();
            await TestDatabase.ClearDataAsync<Subject>();
            await TestDatabase.ClearDataAsync<Room>();
            await TestDatabase.ClearDataAsync<Student>();
            await TestDatabase.ClearDataAsync<OrganizationalClass>();
        }

        protected override async Task SetupDataForEveryTestAsync()
        {
            await _configRepo.Records.DefaultLessonDuration.SetAndSaveAsync(_DefDuration);

            (_orgClass1, _orgClass2) = await FakeData.ScheduleOf_4f_5f_Classes_WithLessonEntities(await _Year, _orgClassRepo, _teacherRepo, _periLessonRepo);

            Assert.IsTrue(await _periLessonRepo.ExistsAsync(x => x.TakenLessons.Any()));
        }

        protected override void SetupServices()
        {
            _orgClassRepo = new Repository<OrganizationalClass>(_Context, null);
            _teacherRepo = new Repository<Teacher>(_Context, null);
            _periLessonRepo = new RepositoryBySchoolYear<PeriodicLesson>(_Context, null, _schoolYearRepo);
            _lessonRepo = new RepositoryBySchoolYear<Lesson>(_Context, null, _schoolYearRepo);

            _service = new EditAttendanceService(_lessonRepo);
        }

        private int _DefDuration => 45;

        [Test]
        public async Task ShouldSetPresentForEveryStudent()
        {
            using var timer = new TestTimer();

            var lesson = await _lessonRepo.AsQueryableByYear.ByCurrent().FirstOrDefaultAsync();
            if (lesson is null)
                Assert.Fail("lesson with topic should exist, badly prepared test data");

            var res = await _service.EditAsync(new AttendanceEditJson
            {
                id = lesson!.Id,
                students = lesson.FromSchedule.ParticipatingOrganizationalClass.Students.Select(x => new StudentPresenceEditJson
                {
                    id = x.Id,
                    presence = PresenceStatus.Present
                }).ToArray()
            });

            _lessonRepo.UseIndependentDbContext();
            Assert.IsTrue(await _lessonRepo.ExistsAsync(x => x.Id == lesson.Id
                && x.PresenceOfStudents.All(y => y.Status == PresenceStatus.Present)), "update failed");

            Assert.IsTrue(res.success, res.message);
        }

        [Test]
        public async Task ShouldSetAbsentForEveryStudent()
        {
            using var timer = new TestTimer();

            var lesson = await _lessonRepo.AsQueryableByYear.ByCurrent().FirstOrDefaultAsync();
            if (lesson is null)
                Assert.Fail("lesson with topic should exist, badly prepared test data");

            var res = await _service.EditAsync(new AttendanceEditJson
            {
                id = lesson!.Id,
                students = lesson.FromSchedule.ParticipatingOrganizationalClass.Students.Select(x => new StudentPresenceEditJson
                {
                    id = x.Id,
                    presence = PresenceStatus.Absent
                }).ToArray()
            });

            _lessonRepo.UseIndependentDbContext();
            Assert.IsTrue(await _lessonRepo.ExistsAsync(x => x.Id == lesson.Id
                && x.PresenceOfStudents.All(y => y.Status == PresenceStatus.Absent)), "update failed");

            Assert.IsTrue(res.success, res.message);
        }

        [Test]
        public async Task ShouldNotUpdatePresenct_WhenStatusNotPassed()
        {
            using var timer = new TestTimer();

            var lesson = await _lessonRepo.AsQueryableByYear.ByCurrent().FirstOrDefaultAsync();
            if (lesson is null)
                Assert.Fail("lesson with topic should exist, badly prepared test data");

            var presences = lesson.PresenceOfStudents.OrderBy(x => x.StudentId).Select(x => new
            {
                id = x.StudentId,
                presence = x.Status
            });

            var studentModels = presences.Select((x, index) => new StudentPresenceEditJson
            {
                id = x.id,
                presence = (index % 2 == 0 ? x.presence : null)
            });

            var res = await _service.EditAsync(new AttendanceEditJson
            {
                id = lesson!.Id,
                students = studentModels.ToArray()
            });

            _lessonRepo.UseIndependentDbContext();
            var lessonAgain = await _lessonRepo.AsQueryableByYear.ByCurrent().FirstOrDefaultAsync(x => x.Id == lesson.Id);
            Assert.IsNotNull(lessonAgain);

            var newPresences = lessonAgain.PresenceOfStudents.OrderBy(x => x.StudentId).Select(x => new
            {
                id = x.StudentId,
                presence = x.Status
            });

            for (int i = 0; i < newPresences.Count(); i++)
            {
                var toMatch = studentModels.ElementAt(i).presence ?? presences.ElementAt(i).presence;
                Assert.AreEqual(toMatch, newPresences.ElementAt(i).presence);
            }

            Assert.IsTrue(res.success, res.message);
        }




        #region Fails

        [Test]
        public async Task ShouldFail_WhenLessonIdIsinvalid()
        {
            using var timer = new TestTimer();
            var res = await _service.EditAsync(new AttendanceEditJson
            {
                id = 99999
            });

            Assert.IsFalse(res.success);
        }


        // TODO: invalid studentId, missing student array, invalid presence

        #endregion
    }
}
