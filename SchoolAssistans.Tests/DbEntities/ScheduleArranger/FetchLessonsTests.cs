using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using SchoolAssistant.DAL.Models.Lessons;
using SchoolAssistant.DAL.Models.SchoolYears;
using SchoolAssistant.DAL.Models.Staff;
using SchoolAssistant.DAL.Models.StudentsOrganization;
using SchoolAssistant.DAL.Repositories;
using SchoolAssistant.Logic;
using SchoolAssistant.Logic.ScheduleArranger;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolAssistans.Tests.DbEntities.ScheduleArranger
{
    public class FetchLessonsTests
    {
        private IFetchClassLessonsForSchedArrService _fetchLessonsService = null!;

        private ISchoolYearRepository _schoolYearRepository = null!;
        private IRepository<OrganizationalClass> _orgClassRepo = null!;
        private IRepository<Teacher> _teacherRepo = null!;
        private IRepository<PeriodicLesson> _lessonRepo = null!;

        [OneTimeSetUp]
        public void Setup()
        {
            TestDatabase.CreateContext(TestServices.Collection);
        }

        [OneTimeTearDown]
        public void Teardown()
        {
            TestDatabase.DisposeContext();
        }


        [SetUp]
        public async Task SetupOne()
        {
            TestDatabase.RequestContextFromServices(TestServices.Collection);

            await TestDatabase.ClearDataAsync<PeriodicLesson>();
            await TestDatabase.ClearDataAsync<OrganizationalClass>();

            SetupServices();

            await FakeData.Class_5f_0Students_RandomScheduleAddedSeparately(await _Year, _orgClassRepo, _teacherRepo, _lessonRepo);
            TestDatabase.RequestContextFromServices(TestServices.Collection);
            SetupServices();
        }

        private void SetupServices()
        {
            _schoolYearRepository = new SchoolYearRepository(TestDatabase.Context, null);
            _orgClassRepo = new Repository<OrganizationalClass>(TestDatabase.Context, null);
            _teacherRepo = new Repository<Teacher>(TestDatabase.Context, null);
            _lessonRepo = new Repository<PeriodicLesson>(TestDatabase.Context, null);

            _fetchLessonsService = new FetchClassLessonsForSchedArrService(_orgClassRepo);
        }

        private Task<SchoolYear> _Year => _schoolYearRepository.GetOrCreateCurrentAsync();



        [Test]
        public async Task Should_fetch_lessons()
        {
            var orgClass = await FakeData.Class_4f_0Students_RandomSchedule(await _Year, _orgClassRepo, _teacherRepo);

            var res = await _fetchLessonsService.ForAsync(orgClass.Id);

            Assert.IsNotNull(res);
            Assert.IsNotNull(res!.data);
            Assert.IsNotEmpty(res.data);

            var resLessonsWithDay = res.data.SelectMany(x => x.lessons.Select(y => new
            {
                day = x.dayIndicator,
                lesson = y
            }));

            foreach (var lesson in orgClass.Schedule)
            {
                var occurance = lesson.GetNextOccurance();

                Assert.IsTrue(resLessonsWithDay.Any(x =>
                    x.day == occurance!.Value.DayOfWeek
                    && x.lesson.customDuration == lesson.CustomDuration
                    && x.lesson.time.hour == occurance!.Value.Hour
                    && x.lesson.time.minutes == occurance!.Value.Minute
                    && x.lesson.lecturer.name == lesson.Lecturer.GetShortenedName()
                    && x.lesson.lecturer.id == lesson.LecturerId
                    && x.lesson.subject.name == lesson.Subject.Name
                    && x.lesson.subject.id == lesson.SubjectId
                    && x.lesson.room.name == lesson.Room.DisplayName
                    && x.lesson.room.id == lesson.RoomId));
            }
        }

        [Test]
        public async Task Should_fetch_lessons_added_separately()
        {
            var orgClass = await _orgClassRepo.AsQueryable().FirstOrDefaultAsync(x => x.Grade == 5 && x.Distinction == "f");
            Assert.IsNotNull(orgClass);

            var lessons = await _lessonRepo.AsListAsync();

            var res = await _fetchLessonsService.ForAsync(orgClass.Id);

            Assert.IsNotNull(res);
            Assert.IsNotNull(res!.data);
            Assert.IsNotEmpty(res.data);

            var resLessonsWithDay = res.data.SelectMany(x => x.lessons.Select(y => new
            {
                day = x.dayIndicator,
                lesson = y
            }));

            foreach (var lesson in orgClass.Schedule)
            {
                var occurance = lesson.GetNextOccurance();

                Assert.IsTrue(resLessonsWithDay.Any(x =>
                    x.day == occurance!.Value.DayOfWeek
                    && x.lesson.customDuration == lesson.CustomDuration
                    && x.lesson.time.hour == occurance!.Value.Hour
                    && x.lesson.time.minutes == occurance!.Value.Minute
                    && x.lesson.lecturer.name == lesson.Lecturer.GetShortenedName()
                    && x.lesson.lecturer.id == lesson.LecturerId
                    && x.lesson.subject.name == lesson.Subject.Name
                    && x.lesson.subject.id == lesson.SubjectId
                    && x.lesson.room.name == lesson.Room.DisplayName
                    && x.lesson.room.id == lesson.RoomId));
            }
        }


        #region Fails


        [Test]
        public async Task Should_fail_invalid_classId()
        {
            var res = await _fetchLessonsService.ForAsync(9999);

            Assert.IsNull(res);
        }


        #endregion
    }
}
