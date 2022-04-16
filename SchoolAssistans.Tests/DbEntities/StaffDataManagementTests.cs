using NUnit.Framework;
using SchoolAssistant.DAL.Models.Staff;
using SchoolAssistant.DAL.Models.Subjects;
using SchoolAssistant.DAL.Repositories;
using SchoolAssistant.Infrastructure.Models.DataManagement.Staff;
using SchoolAssistant.Logic.DataManagement.Staff;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolAssistans.Tests.DbEntities
{
    public class StaffDataManagementTests
    {
        private IStaffDataManagementService _staffDataManagementService;
        private IRepository<Subject> _subjectRepo;
        private IRepository<Teacher> _teacherRepo;

        [OneTimeSetUp]
        public async Task Setup()
        {
            TestDatabase.CreateContext(TestServices.Collection);

            _teacherRepo = new Repository<Teacher>(TestDatabase.Context, null);
            _subjectRepo = new Repository<Subject>(TestDatabase.Context, null);

            var teacherModifySvc = new ModifyTeacherFromJsonService(_teacherRepo, _subjectRepo);
            var teacherDataService = new TeachersDataManagementService(teacherModifySvc, _teacherRepo);

            _staffDataManagementService = new StaffDataManagementService(teacherDataService);

            await _subjectRepo.AddRangeAsync(new Subject[]
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

            await _teacherRepo.AddAsync(new Teacher
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
            Assert.AreEqual(teacher.LastName, "Ogariusz");
            Assert.AreEqual(teacher.MainSubjects.First().Name, matematyka.Name);
        }

        [Test]
        public async Task Should_return_error_Blad_nieprawidlowa_kategoria()
        {
            var model = new StaffPersonDetailsJson
            {
                firstName = "balbalb",
                lastName = "yhydhyd",
                groupId = "JakasBledna"
            };

            var res = await _staffDataManagementService.CreateOrUpdateAsync(model);

            Assert.AreEqual(res.message, "Błąd! Nieprawidłowa kategoria personelu");
        }

        [Test]
        public async Task Should_modify_teachers_main_subjects()
        {
            var teacher = new Teacher
            {
                FirstName = "Inrelevent",
                LastName = "Some last name"
            };

            teacher.AddMainSubject(_subjectRepo.AsQueryable().FirstOrDefault(x => x.Name == "Jezyk polski"));

            _teacherRepo.Add(teacher);

            _teacherRepo.Save();

            Assert.IsTrue(teacher.MainSubjects.First().Name == "Jezyk polski");

            var matem = _subjectRepo.AsQueryable().FirstOrDefault(x => x.Name == "Matematyka");

            var model = new StaffPersonDetailsJson
            {
                id = teacher.Id,
                firstName = teacher.FirstName,
                lastName = teacher.LastName,
                groupId = nameof(Teacher),
                mainSubjectsIds = new[]
                {
                    matem.Id
                }
            };

            var response = await _staffDataManagementService.CreateOrUpdateAsync(model);

            Assert.IsTrue(response.success);

            teacher = await _teacherRepo.GetByIdAsync(teacher.Id);

            Assert.IsNotNull(teacher);
            Assert.IsTrue(teacher.MainSubjects.Count() == 1);
            Assert.IsTrue(teacher.MainSubjects.First().Name == "Matematyka");
        }
    }
}
