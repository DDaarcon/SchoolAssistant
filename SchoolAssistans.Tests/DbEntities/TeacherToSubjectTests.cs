using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using SchoolAssistant.DAL.Models.Staff;
using SchoolAssistant.DAL.Models.Subjects;
using SchoolAssistant.DAL.Repositories;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolAssistans.Tests.DbEntities
{
    public class TeacherToSubjectTests
    {

        private IRepository<Teacher> _teacherRepo = null!;
        private IRepository<Subject> _subjectRepo = null!;

        [OneTimeSetUp]
        public void Setup()
        {
            _teacherRepo = new Repository<Teacher>(TestDatabase.CreateContext(TestServices.Collection), null);
            _subjectRepo = new Repository<Subject>(TestDatabase.Context, null);

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
                Name = "Being a not pleasable woman"
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
        public async Task TeacherIsPresentAsync()
        {
            var teacher = await GetJoannaAsync();

            Assert.IsNotNull(teacher);
        }

        [Test]
        public async Task MainSubjectIsPresentAsync()
        {
            var teacher = await GetJoannaAsync();

            Assert.IsNotNull(teacher);
            Assert.IsTrue(teacher.MainSubjects is not null);
            Assert.IsTrue(teacher.MainSubjects!.Any(x => x.Name == "Modeling"));
        }

        [Test]
        public async Task AdditionalSubjectIsPresentAsync()
        {
            var teacher = await GetJoannaAsync();

            Assert.True(teacher.AdditionalSubjects is not null);
            Assert.True(teacher.AdditionalSubjects!.Any(x => x.Name == "Being a not pleasable woman"));
        }

        private Task<Teacher?> GetJoannaAsync()
        {
            return _teacherRepo.AsQueryable().FirstOrDefaultAsync(x => x.FirstName == "Joanna" && x.LastName == "Krupa");
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
        public async Task AddingReferenceFromExistingTeacherToExistinSubjectAsync()
        {
            var subject = new Subject
            {
                Name = "Some subject"
            };
            await _subjectRepo.AddAsync(subject);
            await _subjectRepo.SaveAsync();

            var teacher = await _teacherRepo.AsQueryable().FirstAsync();

            teacher.AddMainSubject(subject);

            _teacherRepo.Update(teacher);
            await _teacherRepo.SaveAsync();

            teacher = await _teacherRepo.AsQueryable().FirstAsync(x => x.Id == teacher.Id);

            Assert.IsNotNull(teacher);
            Assert.IsTrue(teacher.MainSubjects.Contains(subject));
        }

        [Test]
        public async Task AddingNewTeacherWithReferenceToExistingSubjectAsync()
        {

            var subject = new Subject
            {
                Name = "Some subject other subject"
            };
            await _subjectRepo.AddAsync(subject);
            await _subjectRepo.SaveAsync();

            var teacher = new Teacher
            {
                FirstName = "John",
                LastName = "Travolta"
            };

            teacher.AddMainSubject(subject);

            await _teacherRepo.AddAsync(teacher);
            await _teacherRepo.SaveAsync();

            teacher = await _teacherRepo.AsQueryable().FirstOrDefaultAsync(x => x.FirstName == "John" && x.LastName == "Travolta");

            Assert.IsNotNull(teacher);
            Assert.IsTrue(teacher.MainSubjects.Contains(subject));
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

            Assert.IsTrue(subjectNames.Any(x => x.Name == "Being a not pleasable woman"));
        }

    }
}