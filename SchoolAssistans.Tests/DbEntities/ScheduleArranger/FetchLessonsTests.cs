using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cronos;
using NUnit.Framework;
using SchoolAssistant.DAL.Models.Lessons;
using SchoolAssistant.DAL.Models.SchoolYears;
using SchoolAssistant.DAL.Models.Staff;
using SchoolAssistant.DAL.Models.StudentsOrganization;
using SchoolAssistant.DAL.Repositories;
using SchoolAssistant.Logic;
using SchoolAssistant.Logic.ScheduleArranger;

namespace SchoolAssistans.Tests.DbEntities.ScheduleArranger
{
    public class FetchLessonsTests
    {
        private IFetchLessonsService _fetchLessonsService = null!;

        private ISchoolYearRepository _schoolYearRepository = null!;
        private IRepository<OrganizationalClass> _orgClassRepo = null!;
        private IRepository<Teacher> _teacherRepo = null!;

        [OneTimeSetUp]
        public async Task Setup()
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

        }

        private void SetupServices()
        {
            _schoolYearRepository = new SchoolYearRepository(TestDatabase.Context, null);
            _orgClassRepo = new Repository<OrganizationalClass>(TestDatabase.Context, null);
            _teacherRepo = new Repository<Teacher>(TestDatabase.Context, null);

            _fetchLessonsService = new FetchLessonsService();
        }

        private Task<SchoolYear> _Year => _schoolYearRepository.GetOrCreateCurrentAsync();



        [Test]
        public async Task Should_fetch_lessons()
        {
            var orgClass = await FakeData.Class_4f_0Students_RandomSchedule(await _Year, _orgClassRepo, _teacherRepo);

            var res = await _fetchLessonsService.ForClassAsync(orgClass.Id);

            Assert.IsNotNull(res);
            Assert.IsNotNull(res.data);

            var resLessonsWithDay = res.data.SelectMany(x => x.lessons.Select(y => new
            {
                day = x.dayIndicator,
                lesson = y
            }));

            foreach (var lesson in orgClass.Schedule)
            {
                var cron = CronExpression.Parse(lesson.CronPeriodicity);
                var occurance = cron.GetNextOccurrence(DateTime.Now);

                Assert.IsTrue(resLessonsWithDay.Any(x =>
                    x.day == occurance!.Value.DayOfWeek
                    && x.lesson.time.hour == occurance!.Value.Hour
                    && x.lesson.time.minutes == occurance!.Value.Minute
                    && x.lesson.lecturer.name == lesson.Lecturer.GetFullName()
                    && x.lesson.lecturer.id == lesson.LecturerId
                    && x.lesson.subject.name == lesson.Subject.Name
                    && x.lesson.subject.id == lesson.SubjectId
                    && x.lesson.room.name == lesson.Room.Name
                    && x.lesson.room.id == lesson.RoomId));
            }
        }


        #region Fails


        [Test]
        public async Task Should_fail_invalid_classId()
        {
            var res = await _fetchLessonsService.ForClassAsync(9999);

            Assert.IsNull(res);
        }


        #endregion
    }
}
