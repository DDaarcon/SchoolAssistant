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
using SchoolAssistant.Logic.ConductingClasses;
using SchoolAssistant.Logic.ConductingClasses.ScheduledLessonsList;
using SchoolAssistant.Logic.Help;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

            _orgClass1 = await FakeData.Class_4f_0Students_RandomSchedule(await _Year, _orgClassRepo, _teacherRepo);
            _orgClass2 = await FakeData.Class_5f_0Students_RandomScheduleAddedSeparately(await _Year, _orgClassRepo, _teacherRepo, _periLessonRepo);

            var periLessons = await _periLessonRepo.AsListAsync();
            var schoolYearId = (await _Year).Id;

            foreach (var periLesson in periLessons)
            {
                var pastLessonDates = periLesson.GetOccurrences(DateTime.Now.AddMonths(-12), DateTime.Now);

                var lessons = pastLessonDates.Select(x => new Lesson
                {
                    Date = x,
                    FromScheduleId = periLesson.Id,
                    SchoolYearId = schoolYearId,
                    Topic = "Some topic",
                    PresenceOfStudents = periLesson.ParticipatingOrganizationalClass!.Students.Select(x => new Presence
                    {
                        SchoolYearId = schoolYearId,
                        StudentId = x.Id,
                        Status = PresenceStatus.Present
                    }).ToList()
                }).ToList();

                periLesson.TakenLessons = lessons;
                _periLessonRepo.Update(periLesson);
            }

            await _periLessonRepo.SaveAsync();

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
            Assert.IsTrue(entries.All(x => {
                var entitiesTimes = entities.Select(x => x.GetOccurrences(from, to).ToList()).ToList();
                var date = DatesHelper.FromTicksJs(x.startTimeTk);

                return entities.Any(d =>
                {
                    var times = d.GetOccurrences(from, to);
                    if (!times.Any(y =>
                    {
                        return y.GetTicksJs() == x.startTimeTk;
                    }))
                        return false;

                    bool baseCheck = x.className == d.ParticipatingOrganizationalClass?.Name
                        && x.subjectName == d.Subject.Name
                        && x.duration == (d.CustomDuration ?? _DefDuration);

                    if (!baseCheck)
                        return false;

                    if (x.heldClasses != null && mustNotHaveClasses)
                        return false;

                    if (x.heldClasses != null)
                        return _lessonRepo.Exists(l =>
                            l.FromScheduleId == d.Id
                            && l.Date == DatesHelper.FromTicksJs(x.startTimeTk)
                            && l.PresenceOfStudents.Count(x => x.Status == PresenceStatus.Present) == x.heldClasses.amountOfPresentStudents
                            && l.Topic == x.heldClasses.topic);

                    if (x.heldClasses == null && mustHaveClasses)
                        return false;

                    return true;
                });
            }));
        }
    }
}
