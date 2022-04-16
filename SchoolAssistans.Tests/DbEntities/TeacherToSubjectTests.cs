using NUnit.Framework;
using SchoolAssistant.DAL.Models.Staff;
using SchoolAssistant.DAL.Models.Subjects;
using SchoolAssistant.DAL.Repositories;
using System.Linq;

namespace SchoolAssistans.Tests.DbEntities
{
    public class TeacherToSubjectTests
    {

        private IRepository<Teacher> _teacherRepo = null!;

        [OneTimeSetUp]
        public void Setup()
        {
            _teacherRepo = new Repository<Teacher>(TestDatabase.CreateContext(TestServices.Collection), null);

            SetUpRecords();
        }

        private void SetUpRecords()
        {
            var teacher1 = new Teacher
            {
                FirstName = "Joanna",
                LastName = "Krupa"
            };
            teacher1.AddMainSubject(new Subject
            {
                Name = "Modeling"
            });
            teacher1.AddAdditionalSubject(new Subject
            {
                Name = "Being a bitch"
            });

            _teacherRepo.AddRange(new Teacher[]
            {
                teacher1
            });

            _teacherRepo.Save();
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            TestDatabase.DisposeContext();
        }

        [Test]
        public void TeacherIsPresent()
        {
            var teachers = _teacherRepo.AsList();

            Assert.True(teachers.Count == 1);
            Assert.True(teachers[0].FirstName == "Joanna");
            Assert.True(teachers[0].LastName == "Krupa");
        }

        [Test]
        public void MainSubjectIsPresent()
        {
            var teacher = _teacherRepo.AsList()[0];

            Assert.True(teacher.MainSubjects is not null);
            Assert.True(teacher.MainSubjects!.Count() == 1);
            Assert.True(teacher.MainSubjects.First().Name == "Modeling");
        }

        [Test]
        public void AdditionalSubjectIsPresent()
        {
            var teacher = _teacherRepo.AsList()[0];

            Assert.True(teacher.AdditionalSubjects is not null);
            Assert.True(teacher.AdditionalSubjects!.Count() == 1);
            Assert.True(teacher.AdditionalSubjects.First().Name == "Being a bitch");
        }

        [Test]
        public void RemovingMainSubject()
        {
            var teacher = _teacherRepo.AsList()[0];
            var newSubject = new Subject { Name = "Judging" };
            teacher.AddMainSubject(newSubject);

            _teacherRepo.Save();
            teacher = _teacherRepo.GetById(teacher.Id);

            Assert.True(teacher.MainSubjects is not null);
            Assert.True(teacher.MainSubjects!.Count() == 2);

            teacher.RemoveMainSubject(newSubject);
            _teacherRepo.Save();
            teacher = _teacherRepo.GetById(teacher.Id);

            Assert.True(teacher.MainSubjects.Count() == 1);
        }

        [Test]
        public void ReferencingMainSubjectsInQuery()
        {
            var subjectNames = _teacherRepo.AsQueryable()
                .SelectMany(x => x.MainSubjects)
                .ToList();

            Assert.IsTrue(subjectNames.Any(x => x.Name == "Modeling"));
        }

        [Test]
        public void ReferencingAdditionalSubjectsInQuery()
        {
            var subjectNames = _teacherRepo.AsQueryable()
                .SelectMany(x => x.AdditionalSubjects)
                .ToList();

            Assert.IsTrue(subjectNames.Any(x => x.Name == "Being a bitch"));
        }

    }
}