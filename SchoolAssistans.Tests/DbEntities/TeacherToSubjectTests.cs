using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using SchoolAssistant.DAL.Models.LinkingTables;
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
        }


        [OneTimeTearDown]
        public void TearDown()
        {
            TestDatabase.DisposeContext();
        }

        [SetUp]
        public async Task SetupOne()
        {
            await TestDatabase.ClearDataAsync<TeacherToMainSubject>();
            await TestDatabase.ClearDataAsync<TeacherToAdditionalSubject>();
            await TestDatabase.ClearDataAsync<Subject>();
            await TestDatabase.ClearDataAsync<Teacher>();

            SetUpRecords();
        }
        private void SetUpRecords()
        {
            var teacher1 = new Teacher
            {
                FirstName = "Joanna",
                LastName = "Krupa"
            };
            teacher1.SubjectOperations.AddNewlyCreatedMain(new Subject
            {
                Name = "Modeling"
            });
            teacher1.SubjectOperations.AddNewlyCreatedAdditional(new Subject
            {
                Name = "Being a not pleasable woman"
            });

            _teacherRepo.AddRange(new Teacher[]
            {
                teacher1
            });

            _teacherRepo.Save();
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
            Assert.IsTrue(teacher.MainSubjects!.Any(x => x.Subject.Name == "Modeling"));
        }

        [Test]
        public async Task AdditionalSubjectIsPresentAsync()
        {
            var teacher = await GetJoannaAsync();

            Assert.True(teacher.AdditionalSubjects is not null);
            Assert.True(teacher.AdditionalSubjects!.Any(x => x.Subject.Name == "Being a not pleasable woman"));
        }

        private Task<Teacher?> GetJoannaAsync()
        {
            return _teacherRepo.AsQueryable().FirstOrDefaultAsync(x => x.FirstName == "Joanna" && x.LastName == "Krupa");
        }

        [Test]
        public async Task RemovingAdditionalSubjectAsync()
        {
            var teacher = new Teacher
            {
                FirstName = "Michael",
                LastName = "Jackson"
            };

            await _teacherRepo.AddAsync(teacher);
            await _teacherRepo.SaveAsync();

            var newSubject = new Subject { Name = "Judging" };
            teacher.SubjectOperations.AddNewlyCreatedAdditional(newSubject);

            await _teacherRepo.SaveAsync();
            teacher = await _teacherRepo.GetByIdAsync(teacher.Id);

            Assert.IsTrue(teacher.AdditionalSubjects is not null);
            Assert.IsTrue(teacher.AdditionalSubjects!.Count() == 1);

            teacher.SubjectOperations.RemoveAdditional(newSubject);
            await _subjectRepo.SaveAsync();
            teacher = await _teacherRepo.GetByIdAsync(teacher.Id);

            Assert.IsTrue(teacher.AdditionalSubjects.Count() == 0);
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

            teacher.SubjectOperations.AddMain(subject);

            _teacherRepo.Update(teacher);
            await _teacherRepo.SaveAsync();

            teacher = await _teacherRepo.AsQueryable().FirstAsync(x => x.Id == teacher.Id);

            Assert.IsNotNull(teacher);
            Assert.IsTrue(teacher.MainSubjects.Any(x => x.SubjectId == subject.Id));
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

            teacher.SubjectOperations.AddMain(subject);

            await _teacherRepo.AddAsync(teacher);
            await _teacherRepo.SaveAsync();

            var teacher2 = await _teacherRepo.AsQueryable().FirstOrDefaultAsync(x => x.FirstName == "John" && x.LastName == "Travolta");

            Assert.IsNotNull(teacher2);
            Assert.IsTrue(teacher2.MainSubjects.Any(x => x.SubjectId == subject.Id));
        }

        [Test]
        public void ReferencingMainSubjectsInQuery()
        {
            var subjectNames = _teacherRepo.AsQueryable()
                .SelectMany(x => x.MainSubjects)
                .Select(x => x.Subject)
                .ToList();

            Assert.IsTrue(subjectNames.Any(x => x.Name == "Modeling"));
        }

        [Test]
        public void ReferencingAdditionalSubjectsInQuery()
        {
            var subjectNames = _teacherRepo.AsQueryable()
                .SelectMany(x => x.AdditionalSubjects)
                .Select(x => x.Subject)
                .ToList();

            Assert.IsTrue(subjectNames.Any(x => x.Name == "Being a not pleasable woman"));
        }

    }
}