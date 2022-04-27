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

        private StudentRegisterRecord SampleStudentRegister3 => new StudentRegisterRecord
        {
            FirstName = "Fiufiu",
            LastName = "Krychowiak",
            DateOfBirth = new DateTime(1999, 10, 18),
            PlaceOfBirth = "Londyn",
            PersonalID = "6134838139",
            Address = "Warsaw ul.Ciekawa 150",
            FirstParent = new ParentRegisterSubrecord
            {
                FirstName = "Milena",
                LastName = "Krychowiak",
                Address = "Warsaw ul.Ciekawa 150",
                PhoneNumber = "458739123",
                Email = "trututu@lalala.com"
            },
        };


        private async Task<OrganizationalClass> Add_3b_2_Students()
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

        private async Task<OrganizationalClass> Add_2g_Studentless()
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
            var orgClass = await Add_3b_2_Students();

            var res = await _registerDataManagementService.GetEntriesJsonAsync();

            Assert.IsNotNull(res);
            Assert.IsTrue(res.Length == 2);
            Assert.IsTrue(res.All(x => orgClass.Students.Select(x => x.InfoId).Contains(x.id)));
            Assert.IsTrue(res.All(x => orgClass.Students.Select(x => x.Info.GetFullName()).Contains(x.name)));
            Assert.IsTrue(res.All(x => x.className == orgClass.Name));
        }

        [Test]
        public async Task Should_fetch_enties_correct_order()
        {
            var orgClass = await Add_3b_2_Students();
            await _studentRegRepo.AddAsync(SampleStudentRegister3);
            await _studentRegRepo.SaveAsync();

            var res = await _registerDataManagementService.GetEntriesJsonAsync();

            Assert.IsNotNull(res);
            Assert.IsTrue(res.Length == 3);

            var expected = new List<StudentRegisterRecord>
            {
                SampleStudentRegister3,
                SampleStudentRegister2,
                SampleStudentRegister1
            };

            for (int i = 0; i < 3; i++)
            {
                Assert.AreEqual(expected[i].GetFullName(), res[i].name);
            }
        }

        [Test]
        public async Task Should_fetch_modification_data()
        {
            var orgClass = await Add_3b_2_Students();
            var record = orgClass.Students.First().Info;

            var res = await _registerDataManagementService.GetModificationDataJsonAsync(record.Id);

            Assert.IsNotNull(res);
            Assert.IsNotNull(res.data);
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

            Validate(registerRec, model);
        }

        [Test]
        public async Task Should_update_register_record_and_add_second_parent()
        {
            var orgClass = await Add_3b_2_Students();
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

            Validate(registerRec, model);
        }


        private void Validate(StudentRegisterRecord entity, StudentRegisterRecordDetailsJson json)
        {
            Assert.IsNotNull(json);
            Assert.IsNotNull(entity);

            Assert.AreEqual(json.lastName, entity.LastName);
            Assert.AreEqual(json.placeOfBirth, entity.PlaceOfBirth);
            Assert.AreEqual(json.personalId, entity.PersonalID);
            Assert.AreEqual(json.address, entity.Address);
            var date = DateTime.Parse(json.dateOfBirth).Date;
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
            Assert.AreEqual(json.secondParent.firstName, secondParent.FirstName);
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
            await _studentRegRepo.AddAsync(SampleStudentRegister1);
            await _studentRegRepo.SaveAsync();

            var model = SampleValidStudentRegisterDetailsJson;
            model.personalId = SampleStudentRegister1.PersonalID;

            var res = await _registerDataManagementService.CreateOrUpdateAsync(model);

            Assert.IsNotNull(res);
            Assert.IsFalse(res.success);
        }

        #endregion
    }
}
