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
using SchoolAssistant.Infrastructure.Models.ConductingClasses.ConductLesson;
using SchoolAssistant.Logic.ConductingClasses.ConductLesson;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolAssistans.Tests.DbEntities.ConductingClasses
{
    internal class EditLessonDetailsTests : BaseDbEntitiesTests
    {

        private IRepository<OrganizationalClass> _orgClassRepo = null!;
        private IRepository<Teacher> _teacherRepo = null!;
        private IRepositoryBySchoolYear<PeriodicLesson> _periLessonRepo = null!;
        private IRepositoryBySchoolYear<Lesson> _lessonRepo = null!;

        private IEditLessonDetailsService _service = null!;

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

            _service = new EditLessonDetailsService(_lessonRepo);
        }

        private DateTime _Monday => DateTime.Now.AddDays(-(int)DateTime.Now.DayOfWeek + 1).AddHours(-DateTime.Now.Hour + 8);

        private int _DefDuration => 45;


        [Test]
        public async Task ShouldChangeExistingLessonTopic()
        {
            using var timer = new TestTimer();

            var lesson = await _lessonRepo.AsQueryable().FirstOrDefaultAsync(x => !String.IsNullOrEmpty(x.Topic));
            if (lesson is null)
                Assert.Fail("lesson with topic should exist, badly prepared test data");

            var res = await _service.EditAsync(new LessonDetailsEditJson
            {
                id = lesson!.Id,
                topic = "some new topic, never happened before"
            });

            _lessonRepo.UseIndependentDbContext();
            Assert.IsTrue(await _lessonRepo.ExistsAsync(x => x.Topic == "some new topic, never happened before"), "not found lesson with changed topic");

            Assert.IsTrue(res.success, res.message);
        }

        [Test]
        public async Task ShouldAddTopicToExistingLesson_WHenLessonWithoutTopicExists()
        {
            using var timer = new TestTimer();

            var lesson = await _lessonRepo.AsQueryable().FirstOrDefaultAsync(x => String.IsNullOrEmpty(x.Topic));
            if (lesson is null)
                Assert.Pass("lesson without topic does not have to exist, ignore test case");

            var res = await _service.EditAsync(new LessonDetailsEditJson
            {
                id = lesson!.Id,
                topic = "some new topic, never happened before"
            });

            Assert.IsTrue(res.success, res.message);
        }


        #region Fails

        [Test]
        public async Task ShouldFail_WhenLessonIdIsinvalid()
        {
            using var timer = new TestTimer();
            var res = await _service.EditAsync(new LessonDetailsEditJson
            {
                id = 99999,
                topic = "some new topic, never happened before"
            });

            Assert.IsFalse(res.success);
        }


        [Test]
        public async Task ShouldFail_WhenTopicIsMissing()
        {
            using var timer = new TestTimer();

            var lesson = await _lessonRepo.AsQueryable().FirstOrDefaultAsync(x => !String.IsNullOrEmpty(x.Topic));
            if (lesson is null)
                Assert.Fail("lesson with topic should exist, badly prepared test data");

            var res = await _service.EditAsync(new LessonDetailsEditJson
            {
                id = 99999,
                topic = null
            });

            Assert.IsFalse(res.success);
        }


        [Test]
        public async Task ShouldFail_WhenTopicIsEmpty()
        {
            using var timer = new TestTimer();

            var lesson = await _lessonRepo.AsQueryable().FirstOrDefaultAsync(x => !String.IsNullOrEmpty(x.Topic));
            if (lesson is null)
                Assert.Fail("lesson with topic should exist, badly prepared test data");

            var res = await _service.EditAsync(new LessonDetailsEditJson
            {
                id = 99999,
                topic = ""
            });

            Assert.IsFalse(res.success);
        }



        #endregion
    }
}
