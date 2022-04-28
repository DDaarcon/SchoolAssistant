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
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolAssistans.Tests.DbEntities.DataManagement
{
    public class StudentRegisterRecordsDataManagementTests
    {
        private ISchoolYearService _schoolYearService = null!;
        private IStudentRegisterRecordsDataManagementService _registerDataManagementService = null!;
        private IRepository<Student> _studentRepo = null!;
        private IRepository<StudentRegisterRecord> _studentRegRepo = null!;
        private IRepository<OrganizationalClass> _orgClassRepo = null!;


        [OneTimeSetUp]
        public void Setup()
        {
            TestDatabase.CreateContext(TestServices.Collection);

            _studentRepo = new Repository<Student>(TestDatabase.Context, null);
            _orgClassRepo = new Repository<OrganizationalClass>(TestDatabase.Context, null);
            _studentRegRepo = new Repository<StudentRegisterRecord>(TestDatabase.Context, null);


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
            await TestDatabase.ClearDataAsync<OrganizationalClass>();
            await TestDatabase.ClearDataAsync<StudentRegisterRecord>();
        }


        private SchoolYear Year => _schoolYearService.GetOrCreateCurrent();

        private StudentRegisterRecordDetailsJson SampleValidStudentRegisterDetailsJson => new StudentRegisterRecordDetailsJson
        {
            firstName = "Mateusz",
            lastName = "Wojak",
            dateOfBirth = "08.10.2000",
            placeOfBirth = "Nowy sącz",
            personalId = "5678876545673",
            address = "Kraków ul. Stara 15",
            firstParent = new ParentRegisterSubrecordDetailsJson
            {
                firstName = "Grażyna",
                lastName = "Nowak",
                address = "Warsaw ul.Ciekawa 17",
                phoneNumber = "3647897128",
                email = "blablabla@ulululu.com"
            },
            secondParent = new ParentRegisterSubrecordDetailsJson
            {
                firstName = "Mariusz",
                secondName = "Rolek",
                lastName = "Nowak",
                address = "Warsaw ul.Ciekawa 17",
                phoneNumber = "21371289",
                email = "blablabla@ulululu.com"
            }
        };



        [Test]
        public async Task Should_fetch_enties_1()
        {
            await FakeData.Class_3b_27Students(
                Year,
                _orgClassRepo);
            await FakeData.Class_2a_Mechanics_15Students(
                Year,
                _orgClassRepo);

            for (int i = 0; i < 5; i++)
                await FakeData.StudentRegisterRecord(_studentRegRepo);

            var res = await _registerDataManagementService.GetEntriesJsonAsync();

            Assert.IsNotNull(res);
            Assert.IsTrue(res.Length == 27 + 15 + 5);
        }

        [Test]
        public async Task Should_fetch_enties_correct_order()
        {
            var orgClass = await FakeData.Class_2a_Mechanics_15Students(
                Year,
                _orgClassRepo);

            var std1 = await FakeData.StudentRegisterRecord(_studentRegRepo,
                "Aaron", "Afuk");
            var std2 = await FakeData.StudentRegisterRecord(_studentRegRepo,
                lastName: "Benx");
            var std3 = await FakeData.StudentRegisterRecord(_studentRegRepo,
                lastName: "Zertan");

            var res = await _registerDataManagementService.GetEntriesJsonAsync();

            Assert.IsNotNull(res);
            Assert.IsTrue(res.Length == 15 + 3);

            var expected = new List<StudentRegisterRecord>
            {
                std1, std2, std3
            }.Concat(orgClass.Students.Select(x => x.Info).OrderBy(x => x.LastName)).ToList();

            for (int i = 0; i < 15 + 3; i++)
            {
                Assert.AreEqual(expected[i].GetFullName(), res[i].name);
            }
        }

        [Test]
        public async Task Should_fetch_modification_data()
        {
            var orgClass = await FakeData.Class_2a_Mechanics_15Students(
                Year,
                _orgClassRepo);
            var record = orgClass.Students.First().Info;

            var res = await _registerDataManagementService.GetModificationDataJsonAsync(record.Id);

            Assert.IsNotNull(res);
            Assert.IsNotNull(res!.data);
            Assert.IsTrue(res.data.id == record.Id);

            Validate(record, res.data);
        }

        [Test]
        public async Task Should_create_register_record()
        {
            var model = new StudentRegisterRecordDetailsJson
            {
                firstName = "Mateusz",
                lastName = "Wojak",
                dateOfBirth = "08.10.2000",
                placeOfBirth = "Nowy sącz",
                personalId = "5678876545673",
                address = "Kraków ul. Stara 15",
                firstParent = new ParentRegisterSubrecordDetailsJson
                {
                    firstName = "Jolanta",
                    secondName = "Renata",
                    lastName = "Majczak",
                    address = "Kraków ul. Stara 15",
                    phoneNumber = "134987324",
                    email = "jujuj@grgr.com",
                }
            };

            var res = await _registerDataManagementService.CreateOrUpdateAsync(model);

            Assert.IsNotNull(res);
            Assert.IsTrue(res.success);

            var registerRec = await _studentRegRepo.AsQueryable().FirstOrDefaultAsync(x => x.FirstName == model.firstName && x.LastName == model.lastName);

            Validate(registerRec!, model);
        }

        [Test]
        public async Task Should_update_register_record_and_add_second_parent()
        {
            var orgClass = await FakeData.Class_2a_Mechanics_15Students(
                Year,
                _orgClassRepo);
            var student = orgClass.Students.Where(x => x.Info.SecondParent is not null).First();

            var model = new StudentRegisterRecordDetailsJson
            {
                id = student.Info.Id,
                firstName = "Mateusz",
                lastName = "Wojak",
                dateOfBirth = "08.10.2000",
                placeOfBirth = "Nowy sącz",
                personalId = "5678876545673",
                address = "Kraków ul. Stara 15",
                firstParent = new ParentRegisterSubrecordDetailsJson
                {
                    firstName = "Grażyna",
                    lastName = "Nowak",
                    address = "Warsaw ul.Ciekawa 17",
                    phoneNumber = "3647897128",
                    email = "blablabla@ulululu.com"
                },
                secondParent = new ParentRegisterSubrecordDetailsJson
                {
                    firstName = "Mariusz",
                    secondName = "Rolek",
                    lastName = "Nowak",
                    address = "Warsaw ul.Ciekawa 17",
                    phoneNumber = "21371289",
                    email = "blablabla@ulululu.com"
                }
            };

            var res = await _registerDataManagementService.CreateOrUpdateAsync(model);

            Assert.IsNotNull(res);
            Assert.IsTrue(res.success);

            var registerRec = await _studentRegRepo.AsQueryable().FirstOrDefaultAsync(x => x.FirstName == model.firstName && x.LastName == model.lastName);

            Validate(registerRec!, model);
        }


        private void Validate(StudentRegisterRecord entity, StudentRegisterRecordDetailsJson json)
        {
            Assert.IsNotNull(json);
            Assert.IsNotNull(entity);

            Assert.AreEqual(json.lastName, entity.LastName);
            Assert.AreEqual(json.placeOfBirth, entity.PlaceOfBirth);
            Assert.AreEqual(json.personalId, entity.PersonalID);
            Assert.AreEqual(json.address, entity.Address);
            var date = DateOnly.Parse(json.dateOfBirth);
            Assert.AreEqual(date, entity.DateOfBirth);

            var parent = entity.FirstParent;
            Assert.IsNotNull(parent);
            Assert.AreEqual(json.firstParent.firstName, parent.FirstName);
            Assert.AreEqual(json.firstParent.secondName, parent.SecondName);
            Assert.AreEqual(json.firstParent.lastName, parent.LastName);
            Assert.AreEqual(json.firstParent.phoneNumber, parent.PhoneNumber);
            Assert.AreEqual(json.firstParent.address, parent.Address);

            var secondParent = entity.SecondParent;
            if (secondParent is null)
            {
                Assert.IsNull(json.secondParent);
                return;
            }
            Assert.IsNotNull(json.secondParent);
            Assert.AreEqual(json.secondParent!.firstName, secondParent.FirstName);
            Assert.AreEqual(json.secondParent.secondName, secondParent.SecondName);
            Assert.AreEqual(json.secondParent.lastName, secondParent.LastName);
            Assert.AreEqual(json.secondParent.phoneNumber, secondParent.PhoneNumber);
            Assert.AreEqual(json.secondParent.address, secondParent.Address);
        }


        #region Fails

        [Test]
        public async Task Should_fail_missing_fist_name()
        {
            var model = SampleValidStudentRegisterDetailsJson;
            model.firstName = "";

            var res = await _registerDataManagementService.CreateOrUpdateAsync(model);

            Assert.IsNotNull(res);
            Assert.IsFalse(res.success);
        }

        [Test]
        public async Task Should_fail_missing_last_name()
        {
            var model = SampleValidStudentRegisterDetailsJson;
            model.lastName = "";

            var res = await _registerDataManagementService.CreateOrUpdateAsync(model);

            Assert.IsNotNull(res);
            Assert.IsFalse(res.success);
        }

        [Test]
        public async Task Should_fail_missing_address()
        {
            var model = SampleValidStudentRegisterDetailsJson;
            model.address = "";

            var res = await _registerDataManagementService.CreateOrUpdateAsync(model);

            Assert.IsNotNull(res);
            Assert.IsFalse(res.success);
        }

        [Test]
        public async Task Should_fail_missing_date_of_birth()
        {
            var model = SampleValidStudentRegisterDetailsJson;
            model.dateOfBirth = "";

            var res = await _registerDataManagementService.CreateOrUpdateAsync(model);

            Assert.IsNotNull(res);
            Assert.IsFalse(res.success);
        }

        [Test]
        public async Task Should_fail_invalid_date_of_birth()
        {
            var model = SampleValidStudentRegisterDetailsJson;
            model.dateOfBirth = "not date";

            var res = await _registerDataManagementService.CreateOrUpdateAsync(model);

            Assert.IsNotNull(res);
            Assert.IsFalse(res.success);
        }

        [Test]
        public async Task Should_fail_missing_personal_id()
        {
            var model = SampleValidStudentRegisterDetailsJson;
            model.personalId = "";

            var res = await _registerDataManagementService.CreateOrUpdateAsync(model);

            Assert.IsNotNull(res);
            Assert.IsFalse(res.success);
        }

        [Test]
        public async Task Should_fail_missing_place_of_birth()
        {
            var model = SampleValidStudentRegisterDetailsJson;
            model.placeOfBirth = "";

            var res = await _registerDataManagementService.CreateOrUpdateAsync(model);

            Assert.IsNotNull(res);
            Assert.IsFalse(res.success);
        }

        [Test]
        public async Task Should_fail_missing_fist_parent()
        {
            var model = SampleValidStudentRegisterDetailsJson;
            model.firstParent = null!;

            var res = await _registerDataManagementService.CreateOrUpdateAsync(model);

            Assert.IsNotNull(res);
            Assert.IsFalse(res.success);
        }

        [Test]
        public async Task Should_fail_missing_fist_name_of_parent()
        {
            var model = SampleValidStudentRegisterDetailsJson;
            model.firstParent.firstName = "";

            var res = await _registerDataManagementService.CreateOrUpdateAsync(model);

            Assert.IsNotNull(res);
            Assert.IsFalse(res.success);
        }

        [Test]
        public async Task Should_fail_missing_last_name_of_parent()
        {
            var model = SampleValidStudentRegisterDetailsJson;
            model.firstParent.lastName = "";

            var res = await _registerDataManagementService.CreateOrUpdateAsync(model);

            Assert.IsNotNull(res);
            Assert.IsFalse(res.success);
        }

        [Test]
        public async Task Should_fail_missing_phone_number_of_parent()
        {
            var model = SampleValidStudentRegisterDetailsJson;
            model.firstParent.phoneNumber = "";

            var res = await _registerDataManagementService.CreateOrUpdateAsync(model);

            Assert.IsNotNull(res);
            Assert.IsFalse(res.success);
        }

        [Test]
        public async Task Should_fail_missing_address_of_parent()
        {
            var model = SampleValidStudentRegisterDetailsJson;
            model.firstParent.address = "";

            var res = await _registerDataManagementService.CreateOrUpdateAsync(model);

            Assert.IsNotNull(res);
            Assert.IsFalse(res.success);
        }

        [Test]
        public async Task Should_fail_missing_email_of_parent()
        {
            var model = SampleValidStudentRegisterDetailsJson;
            model.firstParent.email = "";

            var res = await _registerDataManagementService.CreateOrUpdateAsync(model);

            Assert.IsNotNull(res);
            Assert.IsFalse(res.success);
        }

        [Test]
        public async Task Should_fail_invalid_email_of_parent()
        {
            var model = SampleValidStudentRegisterDetailsJson;
            model.firstParent.email = "not valid email";

            var res = await _registerDataManagementService.CreateOrUpdateAsync(model);

            Assert.IsNotNull(res);
            Assert.IsFalse(res.success);
        }

        [Test]
        public async Task Should_fail_missing_model()
        {
            var res = await _registerDataManagementService.CreateOrUpdateAsync(null!);

            Assert.IsNotNull(res);
            Assert.IsFalse(res.success);
        }

        [Test]
        public async Task Should_fail_existing_personal_id()
        {
            var student = await FakeData.StudentRegisterRecord(_studentRegRepo);

            var model = SampleValidStudentRegisterDetailsJson;
            model.personalId = student.PersonalID;

            var res = await _registerDataManagementService.CreateOrUpdateAsync(model);

            Assert.IsNotNull(res);
            Assert.IsFalse(res.success);
        }

        #endregion
    }
}
