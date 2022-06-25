﻿using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using SchoolAssistant.DAL.Models.Attendance;
using SchoolAssistant.DAL.Models.Lessons;
using SchoolAssistant.DAL.Models.Marks;
using SchoolAssistant.DAL.Models.Rooms;
using SchoolAssistant.DAL.Models.Staff;
using SchoolAssistant.DAL.Models.StudentsOrganization;
using SchoolAssistant.DAL.Models.StudentsParents;
using SchoolAssistant.DAL.Models.Subjects;
using SchoolAssistant.DAL.Repositories;
using SchoolAssistant.Infrastructure.Enums.Marks;
using SchoolAssistant.Infrastructure.Models.ConductingClasses.ConductLesson;
using SchoolAssistant.Logic.ConductingClasses.ConductLesson;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolAssistans.Tests.DbEntities.ConductingClasses
{
    internal class GiveMarkTests : BaseDbEntitiesTests
    {
        private IRepository<OrganizationalClass> _orgClassRepo = null!;
        private IRepository<Teacher> _teacherRepo = null!;
        private IRepositoryBySchoolYear<PeriodicLesson> _periLessonRepo = null!;
        private IRepositoryBySchoolYear<Lesson> _lessonRepo = null!;
        private IRepositoryBySchoolYear<Mark> _markRepo = null!;

        private IGiveMarkService _service = null!;

        private OrganizationalClass _orgClass1 = null!;
        private OrganizationalClass _orgClass2 = null!;

        protected override async Task CleanDataAfterEveryTestAsync()
        {
            await TestDatabase.ClearDataAsync<Presence>();
            await TestDatabase.ClearDataAsync<Mark>();
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

            var extraClass = await FakeData.Class_3b_27Students(await _Year, _orgClassRepo);

            for (int i = 0; i < extraClass.Students.Count; i++)
            {
                var student = extraClass.Students.ElementAt(i);

                extraClass.Students.Remove(student);
                if (i < extraClass.Students.Count / 2)
                {
                    _orgClass1.Students.Add(student);
                }
                else
                {
                    _orgClass2.Students.Add(student);
                }
            }

            await _orgClassRepo.SaveAsync();

            _orgClassRepo.UseIndependentDbContext();

            _orgClass1 = await _orgClassRepo.GetByIdAsync(_orgClass1.Id)!;
            _orgClass2 = await _orgClassRepo.GetByIdAsync(_orgClass2.Id)!;
        }

        protected override void SetupServices()
        {
            _orgClassRepo = new Repository<OrganizationalClass>(_Context, null);
            _teacherRepo = new Repository<Teacher>(_Context, null);
            _periLessonRepo = new RepositoryBySchoolYear<PeriodicLesson>(_Context, null, _schoolYearRepo);
            _lessonRepo = new RepositoryBySchoolYear<Lesson>(_Context, null, _schoolYearRepo);
            _markRepo = new RepositoryBySchoolYear<Mark>(_Context, null, _schoolYearRepo);
            var studentRepo = new RepositoryBySchoolYear<Student>(_Context, null, _schoolYearRepo);

            _service = new GiveMarkService(_lessonRepo, studentRepo, _markRepo);
        }

        private int _DefDuration => 45;



        [Test]
        public void ___ShouldCreateClassesWithScheduleAndStudents()
        {
            Assert.IsNotNull(_orgClass1);
            Assert.IsNotNull(_orgClass2);
            Assert.IsTrue(_orgClass1.Students.Any(), "no students in orgClass1");
            Assert.IsTrue(_orgClass1.Schedule.Any(), "no schedule in orgClass1");
            Assert.IsTrue(_orgClass2.Students.Any(), "no students in orgClass2");
            Assert.IsTrue(_orgClass2.Schedule.Any(), "no students in orgClass2");
        }




        [Test]
        public async Task ShouldGiveMarkToStudent()
        {
            using var timer = new TestTimer();

            var lesson = await _lessonRepo.AsQueryableByYear.ByCurrent().FirstOrDefaultAsync();
            if (lesson is null)
                Assert.Fail("lesson with topic should exist, badly prepared test data");

            var studentId = lesson.FromSchedule.ParticipatingOrganizationalClass.Students.First().Id;

            var res = await _service.GiveAsync(new GiveMarkJson
            {
                lessonId = lesson!.Id,
                description = "Descriptive description",
                mark = new MarkJson
                {
                    value = (MarkValue)3
                },
                studentId = studentId
            });

            _lessonRepo.UseIndependentDbContext();
            Assert.IsTrue(await _markRepo.ExistsAsync(x => x.StudentId == studentId
                && x.Collection == null
                && x.Description == "Descriptive description"
                && x.IssuerId == lesson.FromSchedule.LecturerId
                && x.Main == MarkValue.D
                && x.Prefix == null
                && x.SchoolYearId == lesson.SchoolYearId));

            Assert.IsTrue(res.success, res.message);
        }


        [Test]
        public async Task ShouldGiveMarkToStudentWithPrefix()
        {
            using var timer = new TestTimer();

            var lesson = await _lessonRepo.AsQueryableByYear.ByCurrent().FirstOrDefaultAsync();
            if (lesson is null)
                Assert.Fail("lesson with topic should exist, badly prepared test data");

            var studentId = lesson.FromSchedule.ParticipatingOrganizationalClass.Students.First().Id;

            var res = await _service.GiveAsync(new GiveMarkJson
            {
                lessonId = lesson!.Id,
                description = "Descriptive description",
                mark = new MarkJson
                {
                    value = (MarkValue)3,
                    prefix = "-"
                },
                studentId = studentId
            });

            _lessonRepo.UseIndependentDbContext();
            Assert.IsTrue(await _markRepo.ExistsAsync(x => x.StudentId == studentId
                && x.Collection == null
                && x.Description == "Descriptive description"
                && x.IssuerId == lesson.FromSchedule.LecturerId
                && x.Main == MarkValue.D
                && x.Prefix == MarkPrefix.Minus
                && x.SchoolYearId == lesson.SchoolYearId));

            Assert.IsTrue(res.success, res.message);
        }


        [Test]
        public async Task ShouldGiveMarkToStudentWithWeight()
        {
            using var timer = new TestTimer();

            var lesson = await _lessonRepo.AsQueryableByYear.ByCurrent().FirstOrDefaultAsync();
            if (lesson is null)
                Assert.Fail("lesson with topic should exist, badly prepared test data");


            var studentId = lesson.FromSchedule.ParticipatingOrganizationalClass.Students.First().Id;

            var res = await _service.GiveAsync(new GiveMarkJson
            {
                lessonId = lesson!.Id,
                description = "Descriptive description",
                mark = new MarkJson
                {
                    value = (MarkValue)3,
                    prefix = "-"
                },
                studentId = studentId,
                weight = 10
            });

            _lessonRepo.UseIndependentDbContext();
            Assert.IsTrue(await _markRepo.ExistsAsync(x => x.StudentId == studentId
                && x.Collection == null
                && x.Description == "Descriptive description"
                && x.IssuerId == lesson.FromSchedule.LecturerId
                && x.Main == MarkValue.D
                && x.Prefix == MarkPrefix.Minus
                && x.Weight == 10
                && x.SchoolYearId == lesson.SchoolYearId));

            Assert.IsTrue(res.success, res.message);
        }




        #region Fails

        [Test]
        public async Task ShouldFail_WhenLessonIdIsinvalid()
        {
            using var timer = new TestTimer();

            var lesson = await _lessonRepo.AsQueryableByYear.ByCurrent().FirstOrDefaultAsync();
            if (lesson is null)
                Assert.Fail("lesson with topic should exist, badly prepared test data");

            var studentId = lesson.FromSchedule.ParticipatingOrganizationalClass.Students.First().Id;

            var res = await _service.GiveAsync(new GiveMarkJson
            {
                lessonId = 99999,
                description = "Descriptive description",
                mark = new MarkJson
                {
                    value = (MarkValue)3,
                    prefix = "-"
                },
                studentId = studentId,
                weight = 10
            });

            Assert.IsFalse(res.success);
        }

        [Test]
        public async Task ShouldFail_WhenDescriptionIsMissingEmpty()
        {
            using var timer = new TestTimer();

            var lesson = await _lessonRepo.AsQueryableByYear.ByCurrent().FirstOrDefaultAsync();
            if (lesson is null)
                Assert.Fail("lesson with topic should exist, badly prepared test data");

            var studentId = lesson.FromSchedule.ParticipatingOrganizationalClass.Students.First().Id;

            var res = await _service.GiveAsync(new GiveMarkJson
            {
                lessonId = lesson!.Id,
                description = "",
                mark = new MarkJson
                {
                    value = (MarkValue)3,
                    prefix = "-"
                },
                studentId = studentId,
                weight = 10
            });

            Assert.IsFalse(res.success);
        }

        [Test]
        public async Task ShouldFail_WhenMarkIsMissing()
        {
            using var timer = new TestTimer();

            var lesson = await _lessonRepo.AsQueryableByYear.ByCurrent().FirstOrDefaultAsync();
            if (lesson is null)
                Assert.Fail("lesson with topic should exist, badly prepared test data");

            var studentId = lesson.FromSchedule.ParticipatingOrganizationalClass.Students.First().Id;

            var res = await _service.GiveAsync(new GiveMarkJson
            {
                lessonId = lesson!.Id,
                description = "Descriptive description",
                studentId = studentId,
                weight = 10
            });

            Assert.IsFalse(res.success);
        }

        [Test]
        public async Task ShouldFail_WhenMarkValueIsMissing()
        {
            using var timer = new TestTimer();

            var lesson = await _lessonRepo.AsQueryableByYear.ByCurrent().FirstOrDefaultAsync();
            if (lesson is null)
                Assert.Fail("lesson with topic should exist, badly prepared test data");

            var studentId = lesson.FromSchedule.ParticipatingOrganizationalClass.Students.First().Id;

            var res = await _service.GiveAsync(new GiveMarkJson
            {
                lessonId = lesson!.Id,
                description = "Descriptive description",
                mark = new MarkJson
                {
                    prefix = "-"
                },
                studentId = studentId,
                weight = 10
            });

            Assert.IsFalse(res.success);
        }

        [Test]
        public async Task ShouldFail_WhenStudentIdIsInvalid()
        {
            using var timer = new TestTimer();

            var lesson = await _lessonRepo.AsQueryableByYear.ByCurrent().FirstOrDefaultAsync();
            if (lesson is null)
                Assert.Fail("lesson with topic should exist, badly prepared test data");

            var studentId = lesson.FromSchedule.ParticipatingOrganizationalClass.Students.First().Id;

            var res = await _service.GiveAsync(new GiveMarkJson
            {
                lessonId = lesson!.Id,
                description = "Descriptive description",
                mark = new MarkJson
                {
                    value = (MarkValue)3,
                    prefix = "-"
                },
                studentId = 99999,
                weight = 10
            });

            Assert.IsFalse(res.success);
        }

        [Test]
        public async Task ShouldFail_WhenMarkValueIsInvalid()
        {
            using var timer = new TestTimer();

            var lesson = await _lessonRepo.AsQueryableByYear.ByCurrent().FirstOrDefaultAsync();
            if (lesson is null)
                Assert.Fail("lesson with topic should exist, badly prepared test data");

            var studentId = lesson.FromSchedule.ParticipatingOrganizationalClass.Students.First().Id;

            var res = await _service.GiveAsync(new GiveMarkJson
            {
                lessonId = lesson!.Id,
                description = "Descriptive description",
                mark = new MarkJson
                {
                    value = (MarkValue)20,
                    prefix = "-"
                },
                studentId = studentId,
                weight = 10
            });

            Assert.IsFalse(res.success);
        }

        #endregion
    }
}
