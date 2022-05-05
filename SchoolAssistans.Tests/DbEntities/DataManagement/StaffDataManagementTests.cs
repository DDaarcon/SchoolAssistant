using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using SchoolAssistant.DAL.Models.Staff;
using SchoolAssistant.DAL.Models.Subjects;
using SchoolAssistant.DAL.Repositories;
using SchoolAssistant.Infrastructure.Models.DataManagement.Staff;
using SchoolAssistant.Logic.DataManagement.Staff;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolAssistans.Tests.DbEntities.DataManagement
{
    public class StaffDataManagementTests
    {
        private IStaffDataManagementService _staffDataManagementService = null!;
        private IRepository<Subject> _subjectRepo = null!;
        private IRepository<Teacher> _teacherRepo = null!;

        [OneTimeSetUp]
        public async Task Setup()
        {
            TestDatabase.CreateContext(TestServices.Collection);

            _teacherRepo = new Repository<Teacher>(TestDatabase.Context, null);
            _subjectRepo = new Repository<Subject>(TestDatabase.Context, null);

            var teacherModifySvc = new ModifyTeacherFromJsonService(_teacherRepo, _subjectRepo);
            var teacherDataService = new TeachersDataManagementService(teacherModifySvc, _teacherRepo);

            _staffDataManagementService = new StaffDataManagementService(teacherDataService);

            _subjectRepo.AddRange(new Subject[]
            {
                new ()
                {
                    Name = "Jezyk polski"
                },
                new Subject()
                {
                    Name = "Matematyka"
                },
                new Subject() {
                    Name = "Język angielski"
                },
                new Subject() {
                    Name = "Muzyka"
                }
            });

            _teacherRepo.Add(new Teacher
            {
                FirstName = "Jolanta",
                SecondName = "Marzena",
                LastName = "Kowalczyk"
            });

            await _subjectRepo.SaveAsync();
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            TestDatabase.DisposeContext();
        }

        [Test]
        public async Task Should_create_teacher()
        {
            var matematyka = _subjectRepo.AsQueryable().FirstOrDefault(x => x.Name == "Matematyka")!;
            var model = new StaffPersonDetailsJson
            {
                firstName = "Mariusz",
                lastName = "Ogariusz",
                groupId = nameof(Teacher),
                mainSubjectsIds = new long[]
                {
                    matematyka.Id
                }
            };

            var response = await _staffDataManagementService.CreateOrUpdateAsync(model);

            Assert.IsTrue(response.success);

            var teacher = _teacherRepo.AsQueryable().FirstOrDefault(x => x.FirstName == "Mariusz");

            Assert.IsNotNull(teacher);
            Assert.AreEqual(teacher!.LastName, "Ogariusz");
            Assert.AreEqual(teacher.SubjectOperations.MainIter.First().Name, matematyka.Name);
        }

        #region Fails

        [Test]
        public async Task Should_fail_creating_missing_firstname()
        {
            var model = new StaffPersonDetailsJson
            {
                lastName = "Ogariusz",
                groupId = nameof(Teacher),
            };

            var response = await _staffDataManagementService.CreateOrUpdateAsync(model);

            Assert.IsFalse(response.success);
        }

        [Test]
        public async Task Should_fail_creating_missing_lastname()
        {
            var model = new StaffPersonDetailsJson
            {
                firstName = "Ogariusz",
                groupId = nameof(Teacher),
            };

            var response = await _staffDataManagementService.CreateOrUpdateAsync(model);

            Assert.IsFalse(response.success);
        }

        [Test]
        public async Task Should_fail_creating_invalid_type()
        {
            var model = new StaffPersonDetailsJson
            {
                firstName = "balbalb",
                lastName = "yhydhyd",
                groupId = "JakasBledna"
            };

            var res = await _staffDataManagementService.CreateOrUpdateAsync(model);

            Assert.IsFalse(res.success);
        }

        [Test]
        public async Task Should_fail_updating_invalid_teacher_id()
        {
            var model = new StaffPersonDetailsJson
            {
                id = 999999,
                firstName = "balbalb",
                lastName = "yhydhyd",
                groupId = nameof(Teacher)
            };

            var res = await _staffDataManagementService.CreateOrUpdateAsync(model);

            Assert.IsFalse(res.success);
        }

        [Test]
        public async Task Should_fail_creating_invalid_subject_id()
        {
            var model = new StaffPersonDetailsJson
            {
                firstName = "balbalb",
                lastName = "yhydhyd",
                groupId = nameof(Teacher),
                mainSubjectsIds = new long[]
                {
                    999999
                }
            };

            var res = await _staffDataManagementService.CreateOrUpdateAsync(model);

            Assert.IsFalse(res.success);
        }

        [Test]
        public async Task Should_fail_fetching_invalid_id()
        {
            var res = await _staffDataManagementService.GetDetailsJsonAsync(nameof(Teacher), 999999);

            Assert.IsNull(res);
        }

        [Test]
        public async Task Should_fail_fetching_invalid_group()
        {
            var res = await _staffDataManagementService.GetDetailsJsonAsync("Incorrect", 1);

            Assert.IsNull(res);
        }


        [Test]
        public async Task Should_fail_same_main_and_additional_subject()
        {
            var subjects = await FakeData.Subjects(_subjectRepo, 2);

            var model = new StaffPersonDetailsJson
            {
                firstName = "balbalb",
                lastName = "yhydhyd",
                groupId = nameof(Teacher),
                mainSubjectsIds = new long[] { subjects.First().Id, subjects.Skip(1).First().Id },
                additionalSubjectsIds = new long[] { subjects.First().Id }
            };

            var res = await _staffDataManagementService.CreateOrUpdateAsync(model);

            Assert.IsFalse(res.success);
        }

        #endregion

        [Test]
        public async Task Should_modify_teachers_main_subjects_to_have_one_not_owned_before()
        {
            var teacher = await FakeData.Teacher(_teacherRepo);

            var mainIds = teacher.SubjectOperations.MainIter.Select(x => x.Id);
            var additIds = teacher.SubjectOperations.AdditionalIter.Select(x => x.Id);
            var subject = await _subjectRepo.AsQueryable().FirstAsync(x => !mainIds.Contains(x.Id) && !additIds.Contains(x.Id));

            var model = new StaffPersonDetailsJson
            {
                id = teacher.Id,
                firstName = teacher.FirstName,
                lastName = teacher.LastName,
                groupId = nameof(Teacher),
                mainSubjectsIds = new[]
                {
                    subject!.Id
                }
            };

            var response = await _staffDataManagementService.CreateOrUpdateAsync(model);

            Assert.IsTrue(response.success);

            teacher = await _teacherRepo.GetByIdAsync(teacher.Id);

            Assert.IsNotNull(teacher);
            Assert.IsTrue(teacher!.SubjectOperations.MainIter.Count() == 1);
            Assert.IsTrue(teacher.SubjectOperations.MainIter.Any(x => x.Name == subject.Name));
        }

        [Test]
        public async Task Should_modify_teachers_main_subjects_to_have_only_one_owned_before()
        {
            var teacher = await FakeData.Teacher(_teacherRepo);

            var mainIds = teacher.SubjectOperations.MainIter.Select(x => x.Id);
            var additIds = teacher.SubjectOperations.AdditionalIter.Select(x => x.Id);
            var subject = await _subjectRepo.AsQueryable().FirstAsync(x => mainIds.Contains(x.Id) && !additIds.Contains(x.Id));

            var model = new StaffPersonDetailsJson
            {
                id = teacher.Id,
                firstName = teacher.FirstName,
                lastName = teacher.LastName,
                groupId = nameof(Teacher),
                mainSubjectsIds = new[]
                {
                    subject!.Id
                }
            };

            var response = await _staffDataManagementService.CreateOrUpdateAsync(model);

            Assert.IsTrue(response.success);

            teacher = await _teacherRepo.GetByIdAsync(teacher.Id);

            Assert.IsNotNull(teacher);
            Assert.IsTrue(teacher!.SubjectOperations.MainIter.Count() == 1);
            Assert.IsTrue(teacher.SubjectOperations.MainIter.Any(x => x.Name == subject.Name));
        }

        [Test]
        public async Task Should_modify_teacher_firstname()
        {
            var teacher = new Teacher
            {
                FirstName = "Inrelevent",
                LastName = "Some last name"
            };
            _teacherRepo.Add(teacher);
            _teacherRepo.Save();

            var model = new StaffPersonDetailsJson
            {
                id = teacher.Id,
                firstName = "Ulek",
                lastName = teacher.LastName,
                groupId = nameof(Teacher)
            };

            var response = await _staffDataManagementService.CreateOrUpdateAsync(model);

            Assert.IsTrue(response.success);

            teacher = await _teacherRepo.GetByIdAsync(teacher.Id);

            Assert.IsNotNull(teacher);
            Assert.AreEqual(teacher!.FirstName, "Ulek");
        }

        [Test]
        public async Task Should_modify_teacher_lastname()
        {
            var teacher = new Teacher
            {
                FirstName = "Inrelevent",
                LastName = "Some last name"
            };
            _teacherRepo.Add(teacher);
            _teacherRepo.Save();

            var model = new StaffPersonDetailsJson
            {
                id = teacher.Id,
                firstName = teacher.FirstName,
                lastName = "Wulek",
                groupId = nameof(Teacher)
            };

            var response = await _staffDataManagementService.CreateOrUpdateAsync(model);

            Assert.IsTrue(response.success);

            teacher = await _teacherRepo.GetByIdAsync(teacher.Id);

            Assert.IsNotNull(teacher);
            Assert.AreEqual(teacher!.LastName, "Wulek");
        }

        [Test]
        public async Task Should_fetch_details()
        {
            var teacher = new Teacher
            {
                FirstName = "Alvro",
                LastName = "Suez"
            };
            var subject = new Subject
            {
                Name = "Rolling"
            };

            teacher.SubjectOperations.AddNewlyCreatedMain(subject);

            _teacherRepo.Add(teacher);
            await _teacherRepo.SaveAsync();

            var res = await _staffDataManagementService.GetDetailsJsonAsync(nameof(Teacher), teacher.Id);

            Assert.IsNotNull(res);
            Assert.AreEqual(res!.firstName, teacher.FirstName);
            Assert.AreEqual(res.lastName, teacher.LastName);
            Assert.AreEqual(res.mainSubjectsIds!.First(), teacher.SubjectOperations.MainIter.First().Id);
        }

        [Test]
        public async Task Should_fetch_group()
        {
            var teacher = new Teacher
            {
                FirstName = "Alvro",
                LastName = "Suez"
            };

            _teacherRepo.Add(teacher);
            await _teacherRepo.SaveAsync();

            var res = await _staffDataManagementService.GetGroupsOfEntriesJsonAsync();

            Assert.IsNotNull(res);

            var teachersGroup = res.FirstOrDefault(x => x.id == nameof(Teacher));

            Assert.IsNotNull(teachersGroup);
            Assert.IsTrue(teachersGroup!.entries.Any());
        }
    }
}
