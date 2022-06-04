﻿using NUnit.Framework;
using SchoolAssistant.DAL.Enums;
using SchoolAssistant.DAL.Models.Attendance;
using SchoolAssistant.DAL.Models.Lessons;
using SchoolAssistant.DAL.Models.Rooms;
using SchoolAssistant.DAL.Models.Staff;
using SchoolAssistant.DAL.Models.StudentsOrganization;
using SchoolAssistant.DAL.Models.StudentsParents;
using SchoolAssistant.DAL.Models.Subjects;
using SchoolAssistant.DAL.Repositories;
using SchoolAssistant.Infrastructure.Models.ConductingClasses.ScheduledLessonsList;
using SchoolAssistant.Logic;
using SchoolAssistant.Logic.ConductingClasses;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolAssistans.Tests.DbEntities.ConductingClasses
{
    public class ScheduledLessonListTests : BaseDbEntitiesTests
    {
        private IScheduledLessonListService _service = null!;

        private IRepository<OrganizationalClass> _orgClassRepo = null!;
        private IRepository<Teacher> _teacherRepo = null!;
        private IRepository<PeriodicLesson> _periLessonRepo = null!;
        private IRepository<Lesson> _lessonRepo = null!;
        private IAppConfigRepository _configRepo = null!;

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
            await _configRepo.DefaultLessonDuration.SetAndSaveAsync(_DefDuration);

            _orgClass1 = await FakeData.Class_4f_0Students_RandomSchedule(await _Year, _orgClassRepo, _teacherRepo);
            _orgClass2 = await FakeData.Class_5f_0Students_RandomScheduleAddedSeparately(await _Year, _orgClassRepo, _teacherRepo, _periLessonRepo);

            var periLessons = _periLessonRepo.AsQueryable().Take(5);
            var schoolYearId = (await _Year).Id;

            foreach (var periLesson in periLessons)
            {
                var pastLessonDates = periLesson.GetCronExpression().GetOccurrences(DateTime.MinValue, DateTime.Now);

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
                });


            }
        }

        protected override void SetupServices()
        {
            _orgClassRepo = new Repository<OrganizationalClass>(_Context, null);
            _teacherRepo = new Repository<Teacher>(_Context, null);
            _periLessonRepo = new Repository<PeriodicLesson>(_Context, null);
            _lessonRepo = new Repository<Lesson>(_Context, null);
            _configRepo = new AppConfigRepository(_Context, null);

            _service = new ScheduledLessonListService();
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
            var teacher = GetTeacherWithMostScheduledLessons();

            var from = _Monday.AddDays(7);
            var to = _Monday.AddDays(7 + 5);

            var res = await _service.GetModelForTeacherAsync(teacher.Id, new FetchScheduledLessonListModel
            {
                From = from,
                To = to,
                OnlyUpcoming = true
            });

            AssertItemsPresent(res);

            Assert.IsTrue(res.Items.All(x => teacher.Schedule.Any(d =>
                x.ClassName == d.ParticipatingOrganizationalClass?.Name
                && x.SubjectName == d.Subject.Name
                && x.Duration == (d.CustomDuration ?? _DefDuration)
                && x.StartTime == d.GetCronExpression().GetOccurrences(from, to).First()
                && x.HeldClasses == null)));
        }

        [Test]
        public async Task Should_fetch_all_held_from_previous_week()
        {
            var teacher = GetTeacherWithMostScheduledLessons();

            var from = _Monday.AddDays(-7);
            var to = _Monday.AddDays(-7 + 5);

            var res = await _service.GetModelForTeacherAsync(teacher.Id, new FetchScheduledLessonListModel
            {
                From = from,
                To = to
            });

            AssertItemsPresent(res);

            Assert.IsTrue(res.Items.All(x => teacher.Schedule.Any(d =>
                x.ClassName == d.ParticipatingOrganizationalClass?.Name
                && x.SubjectName == d.Subject.Name
                && x.Duration == (d.CustomDuration ?? _DefDuration)
                && x.StartTime == d.GetCronExpression().GetOccurrences(from, to).First()
                && x.HeldClasses != null
                && _lessonRepo.Exists(l =>
                    l.FromScheduleId == d.Id
                    && l.Date == x.StartTime
                    && l.PresenceOfStudents.Count(x => x.Status == PresenceStatus.Present) == x.HeldClasses.AmountOfPresentStudents
                    && l.Topic == x.HeldClasses.Topic))));
        }

        private void AssertItemsPresent(ScheduledLessonListModel res)
        {
            Assert.IsNotNull(res);
            Assert.IsNotNull(res!.Items);
            Assert.IsNotEmpty(res.Items);
        }

        private Teacher GetTeacherWithMostScheduledLessons()
        {
            return _teacherRepo.AsQueryable().Select(x => new
            {
                Teacher = x,
                LessonsCount = x.Schedule.Count
            }).MaxBy(x => x.LessonsCount)!.Teacher;
        }
    }
}