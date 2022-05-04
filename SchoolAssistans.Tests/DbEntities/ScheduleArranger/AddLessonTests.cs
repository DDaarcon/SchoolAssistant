using NUnit.Framework;
using SchoolAssistant.DAL.Models.Lessons;
using SchoolAssistant.DAL.Models.Rooms;
using SchoolAssistant.DAL.Models.Staff;
using SchoolAssistant.DAL.Models.StudentsOrganization;
using SchoolAssistant.DAL.Models.Subjects;
using SchoolAssistant.DAL.Repositories;
using SchoolAssistant.Infrastructure.Models.ScheduleArranger;
using SchoolAssistant.Logic;
using SchoolAssistant.Logic.ScheduleArranger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolAssistans.Tests.DbEntities.ScheduleArranger
{
    public class AddLessonTests : BaseDbEntitiesTests
    {
        private IAddLessonByScheduleArrangerService _addLessonService = null!;

        private IAppConfigRepository _configRepo = null!;
        private IRepository<OrganizationalClass> _orgClassRepo = null!;
        private IRepository<Teacher> _teacherRepo = null!;
        private IRepository<Room> _roomRepo = null!;


        private OrganizationalClass _classWithNoSchedule = null!;
        private IEnumerable<Teacher> _teachers = null!;
        private IList<Room> _rooms = null!;

        private Teacher _Teacher => _teachers.First();
        private Room _Room => _rooms.First();

        protected override async Task CleanDataAfterEveryTestAsync()
        {
            await TestDatabase.ClearDataAsync<PeriodicLesson>();
            await TestDatabase.ClearDataAsync<OrganizationalClass>();
        }

        protected override async Task SetupDataForEveryTestAsync()
        {
            _classWithNoSchedule = await FakeData.Class_1e_0Students(await _Year, _orgClassRepo);
            _teachers = await FakeData._5Random_Teachers(_teacherRepo);
            _rooms = new List<Room>();
            for (int i = 0; i < 10; i++)
                _rooms.Add(await FakeData.Room(_roomRepo));
        }

        protected override void SetupServices()
        {
            _configRepo = new AppConfigRepository(_Context, null);
            _orgClassRepo = new Repository<OrganizationalClass>(_Context, null);
            _teacherRepo = new Repository<Teacher>(_Context, null);
            _roomRepo = new Repository<Room>(_Context, null);

            _addLessonService = new AddLessonByScheduleArrangerService(
                _configRepo,
                _orgClassRepo,
                new Repository<Subject>(_Context, null),
                _teacherRepo,
                _roomRepo,
                new RepositoryBySchoolYear<PeriodicLesson>(_Context, null, _schoolYearRepo));
        }



        [Test]
        public async Task Should_add_lesson()
        {
            var subject = _Teacher.SubjectOperations.MainIter.First();
            var model = new AddLessonRequestJson
            {
                classId = _classWithNoSchedule.Id,
                lecturerId = _Teacher.Id,
                subjectId = subject.Id,
                roomId = _Room.Id,
                time = new TimeJson
                {
                    hour = 10,
                    minutes = 20
                },
                day = DayOfWeek.Tuesday
            };

            var res = await _addLessonService.AddToClass(model);

            AssertResponseSuccess(res);

            Assert.AreEqual(res!.lesson!.time.hour, model.time.hour);
            Assert.AreEqual(res!.lesson!.time.minutes, model.time.minutes);
            Assert.IsNull(res!.lesson!.customDuration);
            Assert.AreEqual(res!.lesson!.lecturer.name, _Teacher.GetShortenedName());
            Assert.AreEqual(res!.lesson!.subject.name, subject.Name);
            Assert.AreEqual(res!.lesson!.room.name, _Room.DisplayName);
            Assert.AreEqual(res!.lesson!.lecturer.id, model.lecturerId);
            Assert.AreEqual(res!.lesson!.subject.id, model.subjectId);
            Assert.AreEqual(res!.lesson!.room.id, model.roomId);
        }

        [Test]
        public async Task Should_add_lesson_custom_duration()
        {
            var subject = _Teacher.SubjectOperations.MainIter.First();
            var model = new AddLessonRequestJson
            {
                classId = _classWithNoSchedule.Id,
                customDuration = 90,
                lecturerId = _Teacher.Id,
                subjectId = subject.Id,
                roomId = _Room.Id,
                time = new TimeJson
                {
                    hour = 10,
                    minutes = 20
                },
                day = DayOfWeek.Tuesday
            };

            var res = await _addLessonService.AddToClass(model);

            AssertResponseSuccess(res);

            Assert.AreEqual(res!.lesson!.time.hour, model.time.hour);
            Assert.AreEqual(res!.lesson!.time.minutes, model.time.minutes);
            Assert.AreEqual(res!.lesson!.customDuration, 90);
            Assert.AreEqual(res!.lesson!.lecturer.name, _Teacher.GetShortenedName());
            Assert.AreEqual(res!.lesson!.subject.name, subject.Name);
            Assert.AreEqual(res!.lesson!.room.name, _Room.DisplayName);
            Assert.AreEqual(res!.lesson!.lecturer.id, model.lecturerId);
            Assert.AreEqual(res!.lesson!.subject.id, model.subjectId);
            Assert.AreEqual(res!.lesson!.room.id, model.roomId);
        }


        #region Fails

        [Test]
        public async Task Should_fail_invalid_hour_to_early()
        {
            await _configRepo.ScheduleStartHour.SetAndSaveAsync(11);

            var subject = _Teacher.SubjectOperations.MainIter.First();
            var model = new AddLessonRequestJson
            {
                classId = _classWithNoSchedule.Id,
                lecturerId = _Teacher.Id,
                subjectId = subject.Id,
                roomId = _Room.Id,
                time = new TimeJson
                {
                    hour = 10,
                    minutes = 20
                },
                day = DayOfWeek.Tuesday
            };

            var res = await _addLessonService.AddToClass(model);

            AssertResponseFail(res);
        }
        [Test]
        public async Task Should_fail_invalid_hour_to_late()
        {
            await _configRepo.ScheduleEndhour.SetAndSaveAsync(18);

            var subject = _Teacher.SubjectOperations.MainIter.First();
            var model = new AddLessonRequestJson
            {
                classId = _classWithNoSchedule.Id,
                lecturerId = _Teacher.Id,
                subjectId = subject.Id,
                roomId = _Room.Id,
                time = new TimeJson
                {
                    hour = 17,
                    minutes = 20
                },
                day = DayOfWeek.Tuesday
            };

            var res = await _addLessonService.AddToClass(model);

            AssertResponseFail(res);
        }
        [Test]
        public async Task Should_fail_invalid_hour()
        {
            var subject = _Teacher.SubjectOperations.MainIter.First();
            var model = new AddLessonRequestJson
            {
                classId = _classWithNoSchedule.Id,
                lecturerId = _Teacher.Id,
                subjectId = subject.Id,
                roomId = _Room.Id,
                time = new TimeJson
                {
                    hour = -1,
                    minutes = 20
                },
                day = DayOfWeek.Tuesday
            };

            var res = await _addLessonService.AddToClass(model);

            AssertResponseFail(res);
        }
        [Test]
        public async Task Should_fail_invalid_minutes()
        {
            var subject = _Teacher.SubjectOperations.MainIter.First();
            var model = new AddLessonRequestJson
            {
                classId = _classWithNoSchedule.Id,
                lecturerId = _Teacher.Id,
                subjectId = subject.Id,
                roomId = _Room.Id,
                time = new TimeJson
                {
                    hour = 10,
                    minutes = 61
                },
                day = DayOfWeek.Tuesday
            };

            var res = await _addLessonService.AddToClass(model);

            AssertResponseFail(res);
        }
        [Test]
        public async Task Should_fail_invalid_classId()
        {
            var subject = _Teacher.SubjectOperations.MainIter.First();
            var model = new AddLessonRequestJson
            {
                classId = 9999,
                lecturerId = _Teacher.Id,
                subjectId = subject.Id,
                roomId = _Room.Id,
                time = new TimeJson
                {
                    hour = 10,
                    minutes = 20
                },
                day = DayOfWeek.Tuesday
            };

            var res = await _addLessonService.AddToClass(model);

            AssertResponseFail(res);
        }
        [Test]
        public async Task Should_fail_invalid_lecturer()
        {
            var subject = _Teacher.SubjectOperations.MainIter.First();
            var model = new AddLessonRequestJson
            {
                classId = _classWithNoSchedule.Id,
                lecturerId = 9999,
                subjectId = subject.Id,
                roomId = _Room.Id,
                time = new TimeJson
                {
                    hour = 10,
                    minutes = 20
                },
                day = DayOfWeek.Tuesday
            };

            var res = await _addLessonService.AddToClass(model);

            AssertResponseFail(res);
        }
        [Test]
        public async Task Should_fail_invalid_subject()
        {
            var subject = _Teacher.SubjectOperations.MainIter.First();
            var model = new AddLessonRequestJson
            {
                classId = _classWithNoSchedule.Id,
                lecturerId = _Teacher.Id,
                subjectId = 9999,
                roomId = _Room.Id,
                time = new TimeJson
                {
                    hour = 10,
                    minutes = 20
                },
                day = DayOfWeek.Tuesday
            };

            var res = await _addLessonService.AddToClass(model);

            AssertResponseFail(res);
        }
        [Test]
        public async Task Should_fail_invalid_room()
        {
            var subject = _Teacher.SubjectOperations.MainIter.First();
            var model = new AddLessonRequestJson
            {
                classId = _classWithNoSchedule.Id,
                lecturerId = _Teacher.Id,
                subjectId = subject.Id,
                roomId = 9999,
                time = new TimeJson
                {
                    hour = 10,
                    minutes = 20
                },
                day = DayOfWeek.Tuesday
            };

            var res = await _addLessonService.AddToClass(model);

            AssertResponseFail(res);
        }
        [Test]
        public async Task Should_fail_missing_time()
        {
            var subject = _Teacher.SubjectOperations.MainIter.First();
            var model = new AddLessonRequestJson
            {
                classId = _classWithNoSchedule.Id,
                lecturerId = _Teacher.Id,
                subjectId = subject.Id,
                roomId = _Room.Id,
                time = null!,
                day = DayOfWeek.Tuesday
            };

            var res = await _addLessonService.AddToClass(model);

            AssertResponseFail(res);
        }
        [Test]
        public async Task Should_fail_invalid_day()
        {
            var subject = _Teacher.SubjectOperations.MainIter.First();
            var model = new AddLessonRequestJson
            {
                classId = _classWithNoSchedule.Id,
                lecturerId = _Teacher.Id,
                subjectId = subject.Id,
                roomId = _Room.Id,
                time = new TimeJson
                {
                    hour = 10,
                    minutes = 20
                },
                day = (DayOfWeek)10
            };

            var res = await _addLessonService.AddToClass(model);

            AssertResponseFail(res);
        }
        [Test]
        public async Task Should_fail_overlapping_with_other()
        {
            var orgClass = await FakeData.Class_4f_0Students_RandomSchedule(await _Year, _orgClassRepo, _teacherRepo);
            var lessonTime = orgClass.Schedule.First(x => x.GetDayOfWeek() == DayOfWeek.Monday).GetTime()!.Value;
            var lessonTimeInMin = Math.Max(lessonTime.Hour * 60 + lessonTime.Minute + 20, 0);

            var subject = _Teacher.SubjectOperations.MainIter.First();
            var model = new AddLessonRequestJson
            {
                classId = orgClass.Id,
                lecturerId = _Teacher.Id,
                subjectId = subject.Id,
                roomId = _Room.Id,
                time = new TimeJson
                {
                    hour = lessonTimeInMin / 60,
                    minutes = lessonTimeInMin % 60
                },
                day = DayOfWeek.Monday
            };

            var res = await _addLessonService.AddToClass(model);

            AssertResponseFail(res);
        }
        [Test]
        public async Task Should_fail_overlapping_with_other_2()
        {
            var orgClass = await FakeData.Class_4f_0Students_RandomSchedule(await _Year, _orgClassRepo, _teacherRepo);
            var lessonTime = orgClass.Schedule.First(x => x.GetDayOfWeek() == DayOfWeek.Monday).GetTime()!.Value;
            var lessonTimeInMin = lessonTime.Hour * 60 + lessonTime.Minute - 20;

            var subject = _Teacher.SubjectOperations.MainIter.First();
            var model = new AddLessonRequestJson
            {
                classId = orgClass.Id,
                lecturerId = _Teacher.Id,
                subjectId = subject.Id,
                roomId = _Room.Id,
                time = new TimeJson
                {
                    hour = lessonTimeInMin / 60,
                    minutes = lessonTimeInMin % 60
                },
                day = DayOfWeek.Monday
            };

            var res = await _addLessonService.AddToClass(model);

            AssertResponseFail(res);
        }


        #endregion
    }
}
