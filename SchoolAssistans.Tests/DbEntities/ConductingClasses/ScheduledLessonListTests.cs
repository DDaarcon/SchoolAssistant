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
using SchoolAssistant.Infrastructure.Models.ConductingClasses.ScheduledLessonsList;
using SchoolAssistant.Logic;
using SchoolAssistant.Logic.ConductingClasses.ScheduledLessonsList;
using SchoolAssistant.Logic.Help;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolAssistans.Tests.DbEntities.ConductingClasses
{
    public class ScheduledLessonListTests : BaseDbEntitiesTests
    {
        private IFetchScheduledLessonListEntriesService _service = null!;

        private IRepository<OrganizationalClass> _orgClassRepo = null!;
        private IRepository<Teacher> _teacherRepo = null!;
        private IRepositoryBySchoolYear<PeriodicLesson> _periLessonRepo = null!;
        private IRepository<Lesson> _lessonRepo = null!;

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
            _lessonRepo = new Repository<Lesson>(_Context, null);

            _service = new FetchScheduledLessonListEntriesService(_teacherRepo, _periLessonRepo, _configRepo);
        }


        private DateTime _Monday => DateTime.Now.AddDays(-(int)DateTime.Now.DayOfWeek + 1).AddHours(-DateTime.Now.Hour + 8);

        private int _DefDuration => 45;

        [Test]
        public void Monday_is_monday()
        {
            Assert.AreEqual(DayOfWeek.Monday, _Monday.DayOfWeek);
        }

        [Test]
        public async Task Should_fetch_all_upcoming_in_next_week()
        {
            using var timer = new TestTimer();

            var teacher = await GetTeacherWithMostScheduledLessonsAsync();

            var from = _Monday.AddDays(7);
            var to = _Monday.AddDays(7 + 5);

            var res = await _service.GetModelForTeacherAsync(teacher.Id, new FetchScheduledLessonsRequestModel
            {
                From = from,
                To = to,
                OnlyUpcoming = true
            });

            AssertItemsPresent(res);

            AssertEntriesMatchEntities(res.entries, teacher.Schedule, from, to, false, true);
        }

        [Test]
        public async Task Should_fetch_all_held_from_previous_week()
        {
            using var timer = new TestTimer();

            var teacher = await GetTeacherWithMostScheduledLessonsAsync();

            var from = _Monday.AddDays(-7);
            var to = _Monday.AddDays(-7 + 5);

            var res = await _service.GetModelForTeacherAsync(teacher.Id, new FetchScheduledLessonsRequestModel
            {
                From = from,
                To = to
            });

            AssertItemsPresent(res);

            AssertEntriesMatchEntities(res.entries, teacher.Schedule, from, to, true, false);

        }

        [Test]
        public async Task ShouldFetchLessons_WhenFromMondayLimitTo5()
        {
            using var timer = new TestTimer();

            var teacher = await GetTeacherWithMostScheduledLessonsAsync();

            var from = _Monday;

            var res = await _service.GetModelForTeacherAsync(teacher.Id, new FetchScheduledLessonsRequestModel
            {
                From = from,
                LimitTo = 5
            });

            AssertItemsPresent(res);

            Assert.IsTrue(res.entries.Length <= 5);

            AssertEntriesMatchEntities(res.entries, teacher.Schedule, from, from.AddDays(100), false, false);
        }

        [Test]
        public async Task ShouldFetchLessons_WhenToMondayLimitTo8()
        {
            using var timer = new TestTimer();

            var teacher = await GetTeacherWithMostScheduledLessonsAsync();

            var to = _Monday;

            var res = await _service.GetModelForTeacherAsync(teacher.Id, new FetchScheduledLessonsRequestModel
            {
                To = to,
                LimitTo = 8
            });

            AssertItemsPresent(res);

            Assert.IsTrue(res.entries.Length <= 8);

            AssertEntriesMatchEntities(res.entries, teacher.Schedule, to.AddDays(-100), to, false, false);
        }


        #region Fails

        [Test]
        public async Task ShouldReturnEmpty_WhenLimitDatesAreMissing()
        {
            using var timer = new TestTimer();

            var teacher = await GetTeacherWithMostScheduledLessonsAsync();

            var res = await _service.GetModelForTeacherAsync(teacher.Id, new FetchScheduledLessonsRequestJson
            {
                limitTo = 100,
                onlyUpcoming = true
            });

            Assert.IsNotNull(res);
            Assert.IsEmpty(res.entries);
        }

        [Test]
        public async Task ShouldReturnEmpty_WhenInvalidTeacherId()
        {
            using var timer = new TestTimer();

            var res = await _service.GetModelForTeacherAsync(999, new FetchScheduledLessonsRequestJson
            {
                fromTk = _Monday.GetTicksJs(),
                limitTo = 100,
                onlyUpcoming = true
            });

            Assert.IsNotNull(res);
            Assert.IsEmpty(res.entries);
        }


        #endregion

        private void AssertItemsPresent(ScheduledLessonListEntriesJson res)
        {
            Assert.IsNotNull(res);
            Assert.IsNotNull(res!.entries);
            Assert.IsNotEmpty(res.entries);
        }

        private async Task<Teacher> GetTeacherWithMostScheduledLessonsAsync()
        {
            return (await _teacherRepo.AsQueryable().Select(x => new
            {
                Teacher = x,
                LessonsCount = x.Schedule.Count
            }).OrderByDescending(x => x.LessonsCount).FirstAsync()).Teacher;
        }

        private void AssertEntriesMatchEntities(ScheduledLessonListEntryJson[] entries, IEnumerable<PeriodicLesson> entities, DateTime from, DateTime to, bool mustHaveClasses, bool mustNotHaveClasses)
        {
            foreach (var entry in entries)
            {
                var date = DatesHelper.FromTicksJs(entry.startTimeTk).ToLocalTime();
                var time = TimeOnly.FromDateTime(date);

                var entityTimes = entities.Select(x => (x.GetTime(), x.GetDayOfWeek()));
                var entityTimesLocal = entities.Select(x => (x.GetTimeLocal(), x.GetDayOfWeek()));

                if (!entities.Any(d =>
                {

                    var entityTime = d.GetTime();
                    var dayOfWeek = d.GetDayOfWeek();
                    if (entityTime != time || dayOfWeek != date.DayOfWeek)
                        return false;

                    bool baseCheck = entry.className == d.ParticipatingOrganizationalClass?.Name
                        && entry.subjectName == d.Subject.Name
                        && entry.duration == (d.CustomDuration ?? _DefDuration);

                    if (!baseCheck)
                        return false;

                    if (entry.heldClasses != null && mustNotHaveClasses)
                        return false;

                    if (entry.heldClasses != null)
                        return _lessonRepo.Exists(l =>
                            l.FromScheduleId == d.Id
                            && l.Date == DatesHelper.FromTicksJs(entry.startTimeTk).ToLocalTime()
                            && l.PresenceOfStudents.Count(x => x.Status == PresenceStatus.Present) == entry.heldClasses.amountOfPresentStudents
                            && l.Topic == entry.heldClasses.topic);

                    if (entry.heldClasses == null && mustHaveClasses)
                        return false;

                    return true;
                }))
                {
                    Assert.Fail($"fail entry at {DatesHelper.FromTicksJs(entry.startTimeTk)} {(entry.heldClasses is not null ? "and should have classes" : "and should not have classes")}");
                }
            };
        }
    }
}
