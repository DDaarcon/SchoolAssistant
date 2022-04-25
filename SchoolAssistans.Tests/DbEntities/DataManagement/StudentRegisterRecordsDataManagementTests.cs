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
    public class StudentRegisterRecordsDataManagementTests
    {
        private ISchoolYearService _schoolYearService;
        private IStudentRegisterRecordsDataManagementService _registerDataManagementService;
        private IRepository<Student> _studentRepo;
        private IRepository<StudentRegisterRecord> _studentRegRepo;
        private IRepository<OrganizationalClass> _orgClassRepo;


        [OneTimeSetUp]
        public void Setup()
        {
            TestDatabase.CreateContext(TestServices.Collection);

            _studentRepo = new Repository<Student>(TestDatabase.Context, null);
            _orgClassRepo = new Repository<OrganizationalClass>(TestDatabase.Context, null);
            _studentRegRepo = new Repository<StudentRegisterRecord>(TestDatabase.Context, null);

            _registerDataManagementService = new StudentRegisterRecordsDataManagementService(_studentRegRepo);

            var yearRepo = new Repository<SchoolYear>(TestDatabase.Context, null);
            _schoolYearService = new SchoolYearService(yearRepo);
        }

        [OneTimeTearDown]
        public void Teardown()
        {
            TestDatabase.DisposeContext();
        }


        [SetUp]
        public void SetupOne()
        {

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

            var res = await _registerDataManagementService.GetEntriesJsonAsync();

            Assert.IsNotNull(res);
            Assert.IsTrue(res.Length == 2);
            Assert.IsTrue(res.All(x => orgClass.Students.Select(x => x.InfoId).Contains(x.id)));
            Assert.IsTrue(res.All(x => orgClass.Students.Select(x => x.Info.GetFullName()).Contains(x.name)));
            Assert.IsTrue(res.All(x => x.className == orgClass.Name));
        }

        [Test]
        public async Task Should_fetch_modification_data_async()
        {
            var orgClass = await Add_3b_2_Students_Async();
            var record = orgClass.Students.First().Info;

            var res = await _registerDataManagementService.GetModificationDataJsonAsync(record.Id);

            Assert.IsNotNull(res);
            Assert.IsNotNull(res.data);
            Assert.IsTrue(res.data.id == record.Id);
            Assert.IsTrue(res.data.address == record.Address);
            Assert.IsTrue(res.data.firstName == record.FirstName);
            Assert.IsTrue(res.data.personalId == record.PersonalID);
            Assert.IsTrue(res.data.placeOfBirth == record.PlaceOfBirth);
            Assert.IsTrue(res.data.secondName == record.SecondName);
            var date = DateTime.Parse(res.data.dateOfBirth).Date;
            Assert.AreEqual(date, record.DateOfBirth);
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

            var res = await _registerDataManagementService.CreateOrUpdateAsync(model);

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

            var res = await _registerDataManagementService.CreateOrUpdateAsync(model);

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
    }
}
