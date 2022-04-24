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

            _dataManagementService = new StudentsDataManagementService(_orgClassRepo);
            var yearRepo = new Repository<SchoolYear>(TestDatabase.Context, null);
            _schoolYearService = new SchoolYearService(yearRepo);
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
            Assert.IsNotNull(res.data.registerRecord);
            Assert.IsTrue(res.data.registerRecord.id == student.Info.Id);
            Assert.IsTrue(res.data.registerRecord.firstName == student.Info.FirstName);
            Assert.IsTrue(res.data.registerRecord.personalId == student.Info.PersonalID);
            Assert.IsTrue(res.data.registerRecord.address == student.Info.Address);
            var date = DateTime.Parse(res.data.registerRecord.dateOfBirth).Date;
            Assert.AreEqual(date, student.Info.DateOfBirth);
        }

        [Test]
        public async Task Should_create_register_record_async()
        {
            var model = new StudentRegisterRecordDetailsJson
            {
                firstName = "Mateusz",
                lastName = "Wojak",
                dateOfBirth = "08.10.2000",
                placeOfBirth = "Nowy sącz",
                personalId = "5678876545673",
                address = "Kraków ul. Stara 15"
            };

            var res = await _dataManagementService.CreateOrUpdateStudentRegisterRecordAsync(model);

            Assert.IsNotNull(res);
            Assert.IsTrue(res.success);

            var registerRec = await _studentRegRepo.AsQueryable().FirstOrDefaultAsync(x => x.FirstName == model.firstName && x.LastName == model.lastName);

            Assert.IsNotNull(registerRec);
            Assert.AreEqual(model.lastName, registerRec.LastName);
            Assert.AreEqual(model.placeOfBirth, registerRec.PlaceOfBirth);
            Assert.AreEqual(model.personalId, registerRec.PersonalID);
            Assert.AreEqual(model.address, registerRec.Address);
            var date = DateTime.Parse(model.dateOfBirth).Date;
            Assert.AreEqual(date, registerRec.DateOfBirth);
        }

        [Test]
        public async Task Should_update_register_record_async()
        {
            var orgClass = await Add_3b_2_Students_Async();
            var student = orgClass.Students.First();

            var model = new StudentRegisterRecordDetailsJson
            {
                id = student.Info.Id,
                firstName = "Mateusz",
                lastName = "Wojak",
                dateOfBirth = "08.10.2000",
                placeOfBirth = "Nowy sącz",
                personalId = "5678876545673",
                address = "Kraków ul. Stara 15"
            };

            var res = await _dataManagementService.CreateOrUpdateStudentRegisterRecordAsync(model);

            Assert.IsNotNull(res);
            Assert.IsTrue(res.success);

            var registerRec = await _studentRegRepo.AsQueryable().FirstOrDefaultAsync(x => x.FirstName == model.firstName && x.LastName == model.lastName);

            Assert.IsNotNull(registerRec);
            Assert.AreEqual(model.id, student.Info.Id);
            Assert.AreEqual(model.lastName, registerRec.LastName);
            Assert.AreEqual(model.placeOfBirth, registerRec.PlaceOfBirth);
            Assert.AreEqual(model.personalId, registerRec.PersonalID);
            Assert.AreEqual(model.address, registerRec.Address);
            var date = DateTime.Parse(model.dateOfBirth).Date;
            Assert.AreEqual(date, registerRec.DateOfBirth);
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

            var res = await _dataManagementService.CreateOrUpdateStudentAsync(model);

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

            var res = await _dataManagementService.CreateOrUpdateStudentAsync(model);

            Assert.IsNotNull(res);
            Assert.IsTrue(res.success);

            student = orgClass.Students.FirstOrDefault(x => x.NumberInJournal == newNumber);

            Assert.IsNotNull(student);
        }
    }
}
