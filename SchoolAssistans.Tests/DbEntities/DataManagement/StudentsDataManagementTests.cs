using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using SchoolAssistant.DAL.Models.SchoolYears;
using SchoolAssistant.DAL.Models.StudentsOrganization;
using SchoolAssistant.DAL.Models.StudentsParents;
using SchoolAssistant.DAL.Repositories;
using SchoolAssistant.Infrastructure.Models.DataManagement.Students;
using SchoolAssistant.Logic;
using SchoolAssistant.Logic.DataManagement.Students;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolAssistans.Tests.DbEntities.DataManagement
{
    public class StudentsDataManagementTests
    {
        private ISchoolYearService _schoolYearService;
        private IStudentsDataManagementService _dataManagementService;
        private IStudentRegisterRecordsDataManagementService _registerDataManagementService;
        private IRepository<OrganizationalClass> _orgClassRepo;
        private IRepository<Student> _studentRepo;
        private IRepository<StudentRegisterRecord> _studentRegRepo;



        [OneTimeSetUp]
        public void Setup()
        {
            TestDatabase.CreateContext(TestServices.Collection);

            _studentRepo = new Repository<Student>(TestDatabase.Context, null);
            _orgClassRepo = new Repository<OrganizationalClass>(TestDatabase.Context, null);
            _studentRegRepo = new Repository<StudentRegisterRecord>(TestDatabase.Context, null);

            _dataManagementService = new StudentsDataManagementService(_orgClassRepo, _studentRepo);

            var yearRepo = new Repository<SchoolYear>(TestDatabase.Context, null);
            _schoolYearService = new SchoolYearService(yearRepo);

            _registerDataManagementService = new StudentRegisterRecordsDataManagementService(
                _schoolYearService,
                new ModifyStudentRegisterRecordFromJsonService(_studentRegRepo),
                _studentRegRepo);
        }

        [OneTimeTearDown]
        public void Teardown()
        {
            TestDatabase.DisposeContext();
        }



        [SetUp]
        public async Task SetupOne()
        {
            await TestDatabase.ClearDataAsync<Student>();
            await TestDatabase.ClearDataAsync<StudentRegisterRecord>();
            await TestDatabase.ClearDataAsync<OrganizationalClass>();
        }

        private SchoolYear Year => _schoolYearService.GetOrCreateCurrent();

        private StudentRegisterRecord SampleStudentRegister1 => new StudentRegisterRecord
        {
            FirstName = "Jolek",
            SecondName = "Karol",
            LastName = "Wałek",
            DateOfBirth = new DateTime(2000, 1, 18),
            PlaceOfBirth = "Kraków",
            PersonalID = "234567876543",
            Address = "Warsaw ul.Pokątna 177",
            FirstParent = new ParentRegisterSubrecord
            {
                FirstName = "Grażyna",
                LastName = "Wałek",
                Address = "Warsaw ul.Pokątna 177",
                PhoneNumber = "4738237938",
                Email = "blablabla@ulululu.com"
            }
        };
        private Student SampleStudent1 => new Student
        {
            SchoolYearId = Year.Id,
            Info = SampleStudentRegister1,
            NumberInJournal = 1
        };

        private StudentRegisterRecord SampleStudentRegister2 => new StudentRegisterRecord
        {
            FirstName = "Wolek",
            SecondName = "Major",
            LastName = "Nowak",
            DateOfBirth = new DateTime(1999, 10, 18),
            PlaceOfBirth = "Częstochowa",
            PersonalID = "6134838139",
            Address = "Warsaw ul.Ciekawa 17",
            FirstParent = new ParentRegisterSubrecord
            {
                FirstName = "Grażyna",
                LastName = "Nowak",
                Address = "Warsaw ul.Ciekawa 17",
                PhoneNumber = "3647897128",
                Email = "blablabla@ulululu.com"
            },
            SecondParent = new ParentRegisterSubrecord
            {
                FirstName = "Mariusz",
                LastName = "Nowak",
                Address = "Warsaw ul.Ciekawa 17",
                PhoneNumber = "21371289",
                Email = "blablabla@ulululu.com"
            }
        };
        private Student SampleStudent2 => new Student
        {
            SchoolYearId = Year.Id,
            Info = SampleStudentRegister2,
            NumberInJournal = 12
        };


        private async Task<OrganizationalClass> Add_3b_2_Students_Async()
        {
            var orgClass = new OrganizationalClass
            {
                SchoolYearId = Year.Id,
                Grade = 3,
                Distinction = "b"
            };

            var std1 = SampleStudent1;
            var std2 = SampleStudent2;

            orgClass.Students.Add(std1);
            orgClass.Students.Add(std2);


            await _orgClassRepo.AddAsync(orgClass);
            await _orgClassRepo.SaveAsync();

            return orgClass;
        }

        private async Task<OrganizationalClass> Add_2g_Studentless_Async()
        {
            var orgClass = new OrganizationalClass
            {
                SchoolYearId = Year.Id,
                Grade = 2,
                Distinction = "g"
            };

            await _orgClassRepo.AddAsync(orgClass);
            await _orgClassRepo.SaveAsync();

            return orgClass;
        }


        [Test]
        public async Task Should_fetch_enties_async()
        {
            var orgClass = await Add_3b_2_Students_Async();

            var res = await _dataManagementService.GetEntriesJsonAsync(orgClass.Id);

            Assert.IsNotNull(res);
            Assert.IsTrue(res.Length == 2);
            Assert.IsTrue(res.All(x => orgClass.Students.Select(x => x.Id).Contains(x.id)));
            Assert.IsTrue(res.All(x => orgClass.Students.Select(x => x.NumberInJournal).Contains(x.numberInJournal)));
            Assert.IsTrue(res.Any(x => x.name == SampleStudentRegister1.GetFullName()));
            Assert.IsTrue(res.Any(x => x.name == SampleStudentRegister2.GetFullName()));
        }

        [Test]
        public async Task Should_fetch_modification_data_async()
        {
            var orgClass = await Add_3b_2_Students_Async();
            var student = orgClass.Students.First();

            var res = await _dataManagementService.GetModificationDataJsonAsync(student.Id);

            Assert.IsNotNull(res);
            Assert.IsNotNull(res.data);
            Assert.IsTrue(res.data.id == student.Id);
            Assert.IsTrue(res.data.numberInJournal == student.NumberInJournal);
            Assert.AreEqual(res.data.registerRecordId, student.InfoId);
        }

        [Test]
        public async Task Should_create_student_async()
        {
            var orgClass = await Add_2g_Studentless_Async();

            var regRecord = SampleStudentRegister1;
            await _studentRegRepo.AddAsync(regRecord);
            await _studentRegRepo.SaveAsync();

            var model = new StudentDetailsJson
            {
                organizationalClassId = orgClass.Id,
                numberInJournal = 20,
                registerRecordId = regRecord.Id
            };

            var res = await _dataManagementService.CreateOrUpdateAsync(model);

            Assert.IsNotNull(res);
            Assert.IsTrue(res.success);

            orgClass = await _orgClassRepo.GetByIdAsync(orgClass.Id);
            var student = orgClass.Students.FirstOrDefault();

            Assert.IsNotNull(student);
            Assert.AreEqual(model.registerRecordId, student.Info.Id);
            Assert.AreEqual(model.numberInJournal, student.NumberInJournal);
        }

        [Test]
        public async Task Should_change_number_in_journal_async()
        {
            var orgClass = await Add_3b_2_Students_Async();
            var student = orgClass.Students.First();

            int newNumber = student.NumberInJournal + 4;

            var model = new StudentDetailsJson
            {
                id = student.Id,
                numberInJournal = newNumber,
                organizationalClassId = orgClass.Id,
                registerRecordId = student.Info.Id
            };

            var res = await _dataManagementService.CreateOrUpdateAsync(model);

            Assert.IsNotNull(res);
            Assert.IsTrue(res.success);

            student = orgClass.Students.FirstOrDefault(x => x.NumberInJournal == newNumber);

            Assert.IsNotNull(student);
        }

        [Test]
        public async Task Should_change_class_async()
        {
            var orgClass1 = await Add_3b_2_Students_Async();
            var orgClass2 = await Add_2g_Studentless_Async();
            var student = orgClass1.Students.First();

            var model = new StudentDetailsJson
            {
                id = student.Id,
                organizationalClassId = orgClass2.Id,
                registerRecordId = student.Info.Id
            };

            var res = await _dataManagementService.CreateOrUpdateAsync(model);

            Assert.IsNotNull(res);
            Assert.IsTrue(res.success);

            student = orgClass2.Students.FirstOrDefault(x => x.Id == student.Id);

            Assert.IsNotNull(student);
        }


        #region Fails

        [Test]
        public async Task Should_fail_invalid_id_async()
        {
            var orgClass = await Add_3b_2_Students_Async();
            var student = orgClass.Students.First();

            var model = new StudentDetailsJson
            {
                id = 9999,
                organizationalClassId = orgClass.Id,
                registerRecordId = student.Info.Id
            };

            var res = await _dataManagementService.CreateOrUpdateAsync(model);

            Assert.IsNotNull(res);
            Assert.IsFalse(res.success);
        }

        [Test]
        public async Task Should_fail_invalid_class_id_async()
        {
            var orgClass = await Add_3b_2_Students_Async();
            var student = orgClass.Students.First();

            var model = new StudentDetailsJson
            {
                id = student.Id,
                organizationalClassId = 9999,
                registerRecordId = student.Info.Id
            };

            var res = await _dataManagementService.CreateOrUpdateAsync(model);

            Assert.IsNotNull(res);
            Assert.IsFalse(res.success);
        }

        [Test]
        public async Task Should_fail_invalid_register_record_id_async()
        {
            var orgClass = await Add_3b_2_Students_Async();
            var student = orgClass.Students.First();

            var model = new StudentDetailsJson
            {
                id = student.Id,
                organizationalClassId = orgClass.Id,
                registerRecordId = 9999
            };

            var res = await _dataManagementService.CreateOrUpdateAsync(model);

            Assert.IsNotNull(res);
            Assert.IsFalse(res.success);
        }

        [Test]
        public async Task Should_fail_missing_register_record_id_async()
        {
            var orgClass = await Add_3b_2_Students_Async();
            var student = orgClass.Students.First();

            var model = new StudentDetailsJson
            {
                id = student.Id,
                organizationalClassId = orgClass.Id
            };

            var res = await _dataManagementService.CreateOrUpdateAsync(model);

            Assert.IsNotNull(res);
            Assert.IsFalse(res.success);
        }

        [Test]
        public async Task Should_fail_missing_class_async()
        {
            var orgClass = await Add_3b_2_Students_Async();
            var student = orgClass.Students.First();

            var model = new StudentDetailsJson
            {
                id = student.Id,
                registerRecordId = student.Info.Id
            };

            var res = await _dataManagementService.CreateOrUpdateAsync(model);

            Assert.IsNotNull(res);
            Assert.IsFalse(res.success);
        }

        [Test]
        public async Task Should_fail_taken_register_id_async()
        {
            var orgClass = await Add_3b_2_Students_Async();
            var student = orgClass.Students.First();
            var student2 = orgClass.Students.Skip(1).First();

            var model = new StudentDetailsJson
            {
                id = student.Id,
                organizationalClassId = orgClass.Id,
                registerRecordId = student2.Info.Id
            };

            var res = await _dataManagementService.CreateOrUpdateAsync(model);

            Assert.IsNotNull(res);
            Assert.IsFalse(res.success);
        }

        [Test]
        public async Task Should_fail_fetch_modify_data_invalid_id_async()
        {
            var res = await _dataManagementService.GetModificationDataJsonAsync(9999);

            Assert.IsNull(res);
        }

        [Test]
        public async Task Should_fail_fetch_entries_invaid_class_id_async()
        {
            var res = await _dataManagementService.GetEntriesJsonAsync(9999);

            Assert.IsNull(res);
        }

        #endregion
    }
}
