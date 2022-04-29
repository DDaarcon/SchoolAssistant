using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
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
        private ISchoolYearRepository _schoolYearRepo = null!;
        private IStudentsDataManagementService _dataManagementService = null!;
        private IStudentRegisterRecordsDataManagementService _registerDataManagementService = null!;
        private IRepository<OrganizationalClass> _orgClassRepo = null!;
        private IRepositoryBySchoolYear<Student> _studentRepo = null!;
        private IRepository<StudentRegisterRecord> _studentRegRepo = null!;



        [OneTimeSetUp]
        public void Setup()
        {
            TestDatabase.CreateContext(TestServices.Collection);

            _schoolYearRepo = new SchoolYearRepository(TestDatabase.Context, null);

            _studentRepo = new RepositoryBySchoolYear<Student>(TestDatabase.Context, null, _schoolYearRepo);
            _orgClassRepo = new Repository<OrganizationalClass>(TestDatabase.Context, TestServices.GetService<IServiceScopeFactory>());
            _studentRegRepo = new Repository<StudentRegisterRecord>(TestDatabase.Context, null);



            _registerDataManagementService = new StudentRegisterRecordsDataManagementService(
                _schoolYearRepo,
                new ModifyStudentRegisterRecordFromJsonService(_studentRegRepo),
                _studentRegRepo);

            _dataManagementService = new StudentsDataManagementService(
                _orgClassRepo,
                _studentRepo,
                _registerDataManagementService,
                new ModifyStudentFromJsonService(_studentRepo, _studentRegRepo, _orgClassRepo));
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
            await TestDatabase.ClearDataAsync<SchoolYear>();

            TestDatabase.StopTrackingEntities();
        }

        private Task<SchoolYear> Year => _schoolYearRepo.GetOrCreateCurrentAsync();



        [Test]
        public async Task Should_fetch_enties()
        {
            await FakeData.Class_2a_Mechanics_15Students(
                await Year,
                _orgClassRepo);

            var orgClass = await FakeData.Class_3b_27Students(
                await Year,
                _orgClassRepo);

            var res = await _dataManagementService.GetEntriesJsonAsync(orgClass.Id);

            Assert.IsNotNull(res);
            Assert.IsTrue(res.Length == 27);
            Assert.IsTrue(res.All(x => orgClass.Students.Select(x => x.Id).Contains(x.id)));
            Assert.IsTrue(res.All(x => orgClass.Students.Select(x => x.NumberInJournal).Contains(x.numberInJournal)));
            Assert.IsTrue(res.All(x => orgClass.Students.Select(x => x.Info.GetFullName()).Contains(x.name)));
        }

        [Test]
        public async Task Should_fetch_modification_data()
        {
            var orgClass = await FakeData.Class_2a_Mechanics_15Students(
                await Year,
                _orgClassRepo);
            var student = orgClass.Students.First();

            var res = await _dataManagementService.GetModificationDataJsonAsync(student.Id);

            Assert.IsNotNull(res);
            Assert.IsNotNull(res!.data);
            Assert.IsTrue(res.data.id == student.Id);
            Assert.IsTrue(res.data.numberInJournal == student.NumberInJournal);
            Assert.AreEqual(res.data.registerRecordId, student.InfoId);
        }

        [Test]
        public async Task Should_create_student()
        {
            var orgClass = await FakeData.Class_2a_Mechanics_15Students(
                await Year,
                _orgClassRepo);

            var regRecord = await FakeData.StudentRegisterRecord(_studentRegRepo);

            var model = new StudentDetailsJson
            {
                organizationalClassId = orgClass.Id,
                numberInJournal = 20,
                registerRecordId = regRecord.Id
            };

            var res = await _dataManagementService.CreateOrUpdateAsync(model);

            Assert.IsNotNull(res);
            Assert.IsTrue(res.success);

            Assert.IsTrue(await _studentRepo.AsQueryable().AnyAsync(
                x => x.OrganizationalClassId == orgClass.Id
                && x.NumberInJournal == model.numberInJournal
                && x.InfoId == model.registerRecordId));
        }

        [Test]
        public async Task Should_change_number_in_journal()
        {
            var orgClass = await FakeData.Class_2a_Mechanics_15Students(
                await Year,
                _orgClassRepo);
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

            Assert.IsTrue(await _studentRepo.AsQueryable().AnyAsync(
                x => x.OrganizationalClassId == orgClass.Id
                && x.NumberInJournal == newNumber
                && x.Id == student.Id));
        }

        [Test]
        public async Task Should_change_class()
        {
            var orgClass1 = await FakeData.Class_2a_Mechanics_15Students(await Year, _orgClassRepo);
            var orgClass2 = await FakeData.Class_3b_27Students(await Year, _orgClassRepo);
            var student = orgClass1.Students.First();

            var model = new StudentDetailsJson
            {
                id = student.Id,
                numberInJournal = GetMaxNumberInJournal(orgClass2) + 1,
                organizationalClassId = orgClass2.Id,
                registerRecordId = student.Info.Id
            };

            var res = await _dataManagementService.CreateOrUpdateAsync(model);

            Assert.IsNotNull(res);
            Assert.IsTrue(res.success);

            Assert.IsTrue(await _studentRepo.AsQueryable().AnyAsync(
                x => x.Id == model.id
                && x.OrganizationalClassId == model.organizationalClassId));
        }

        [Test]
        public async Task Should_remove_existing_student_in_the_same_year()
        {
            var orgClass1 = await FakeData.Class_2a_Mechanics_15Students(await Year, _orgClassRepo);
            var orgClass2 = await FakeData.Class_3b_27Students(await Year, _orgClassRepo);
            var student = orgClass1.Students.First(x => x.NumberInJournal == 5);

            int number = GetMaxNumberInJournal(orgClass2) + 1;
            long recordId = student.Info.Id;

            var model = new StudentDetailsJson
            {
                numberInJournal = number,
                organizationalClassId = orgClass2.Id,
                registerRecordId = recordId
            };

            var res = await _dataManagementService.CreateOrUpdateAsync(model);

            Assert.IsNotNull(res);
            Assert.IsTrue(res.success);

            Assert.IsFalse(await _studentRepo.ExistsAsync(student.Id));

            Assert.IsTrue(await _studentRepo.ExistsAsync(
                x => x.InfoId == model.registerRecordId
                && x.NumberInJournal == model.numberInJournal
                && x.OrganizationalClassId == model.organizationalClassId));
        }

        [Test]
        public async Task Should_not_remove_existing_student_in_different_year()
        {
            var orgClass1 = await FakeData.Class_2a_Mechanics_15Students(await Year, _orgClassRepo);
            var student = orgClass1.Students.First(x => x.NumberInJournal == 5);

            var diffYear = await _schoolYearRepo.GetOrCreateAsync((await Year).Year - 1);
            var orgClass2 = await FakeData.Class_3b_27Students(diffYear, _orgClassRepo);

            int number = GetMaxNumberInJournal(orgClass2) + 1;
            long recordId = student.Info.Id;

            var model = new StudentDetailsJson
            {
                numberInJournal = 5,
                organizationalClassId = orgClass2.Id,
                registerRecordId = recordId
            };

            var res = await _dataManagementService.CreateOrUpdateAsync(model);

            Assert.IsNotNull(res);
            Assert.IsTrue(res.success);

            var record = await _studentRegRepo.GetByIdAsync(recordId);
            Assert.IsNotNull(record);
            Assert.AreEqual(record!.StudentInstances.Count, 2);
        }

        [Test]
        public async Task Should_move_numbers_in_journal_up()
        {
            var orgClass = await FakeData.Class_3b_27Students(await Year, _orgClassRepo);
            var names = orgClass.Students
                .OrderBy(x => x.NumberInJournal)
                .Select(x => x.Info.GetFullName()).ToList();

            var record = await FakeData.StudentRegisterRecord(_studentRegRepo);

            var model = new StudentDetailsJson
            {
                numberInJournal = 5,
                organizationalClassId = orgClass.Id,
                registerRecordId = record.Id
            };

            names.Insert(4, record.GetFullName());

            var res = await _dataManagementService.CreateOrUpdateAsync(model);

            Assert.IsNotNull(res);
            Assert.IsTrue(res.success);

            var newNames = orgClass.Students
                .OrderBy(x => x.NumberInJournal)
                .Select(x => x.Info.GetFullName()).ToArray();

            for (int i = 0; i < names.Count; i++)
                Assert.AreEqual(names[i], newNames[i]);
        }


        #region Fails

        [Test]
        public async Task Should_fail_invalid_id()
        {
            var orgClass = await FakeData.Class_2a_Mechanics_15Students(
                await Year,
                _orgClassRepo);
            var student = orgClass.Students.First();

            var model = new StudentDetailsJson
            {
                id = 9999,
                numberInJournal = student.NumberInJournal,
                organizationalClassId = orgClass.Id,
                registerRecordId = student.Info.Id
            };

            var res = await _dataManagementService.CreateOrUpdateAsync(model);

            Assert.IsNotNull(res);
            Assert.IsFalse(res.success);
        }

        [Test]
        public async Task Should_fail_invalid_class_id()
        {
            var orgClass = await FakeData.Class_2a_Mechanics_15Students(
                await Year,
                _orgClassRepo);
            var student = orgClass.Students.First();

            var model = new StudentDetailsJson
            {
                id = student.Id,
                numberInJournal = student.NumberInJournal,
                organizationalClassId = 9999,
                registerRecordId = student.Info.Id
            };

            var res = await _dataManagementService.CreateOrUpdateAsync(model);

            Assert.IsNotNull(res);
            Assert.IsFalse(res.success);
        }

        [Test]
        public async Task Should_fail_invalid_register_record_id()
        {
            var orgClass = await FakeData.Class_2a_Mechanics_15Students(
                await Year,
                _orgClassRepo);
            var student = orgClass.Students.First();

            var model = new StudentDetailsJson
            {
                id = student.Id,
                numberInJournal = student.NumberInJournal,
                organizationalClassId = orgClass.Id,
                registerRecordId = 9999
            };

            var res = await _dataManagementService.CreateOrUpdateAsync(model);

            Assert.IsNotNull(res);
            Assert.IsFalse(res.success);
        }

        [Test]
        public async Task Should_fail_missing_register_record_id()
        {
            var orgClass = await FakeData.Class_2a_Mechanics_15Students(
                await Year,
                _orgClassRepo);
            var student = orgClass.Students.First();

            var model = new StudentDetailsJson
            {
                id = student.Id,
                numberInJournal = student.NumberInJournal,
                organizationalClassId = orgClass.Id
            };

            var res = await _dataManagementService.CreateOrUpdateAsync(model);

            Assert.IsNotNull(res);
            Assert.IsFalse(res.success);
        }

        [Test]
        public async Task Should_fail_missing_class()
        {
            var orgClass = await FakeData.Class_2a_Mechanics_15Students(
                await Year,
                _orgClassRepo);
            var student = orgClass.Students.First();

            var model = new StudentDetailsJson
            {
                id = student.Id,
                numberInJournal = student.NumberInJournal,
                registerRecordId = student.Info.Id
            };

            var res = await _dataManagementService.CreateOrUpdateAsync(model);

            Assert.IsNotNull(res);
            Assert.IsFalse(res.success);
        }

        [Test]
        public async Task Should_fail_missing_numberInJournal()
        {
            var orgClass = await FakeData.Class_2a_Mechanics_15Students(
                await Year,
                _orgClassRepo);
            var student = orgClass.Students.First();

            var model = new StudentDetailsJson
            {
                id = student.Id,
                organizationalClassId = orgClass.Id,
                registerRecordId = student.Info.Id
            };

            var res = await _dataManagementService.CreateOrUpdateAsync(model);

            Assert.IsNotNull(res);
            Assert.IsFalse(res.success);
        }

        [Test]
        public async Task Should_fail_taken_register_id()
        {
            var orgClass = await FakeData.Class_2a_Mechanics_15Students(
                await Year,
                _orgClassRepo);
            var student = orgClass.Students.First();
            var student2 = orgClass.Students.Skip(1).First();

            var model = new StudentDetailsJson
            {
                id = student.Id,
                numberInJournal = student.NumberInJournal,
                organizationalClassId = orgClass.Id,
                registerRecordId = student2.Info.Id
            };

            var res = await _dataManagementService.CreateOrUpdateAsync(model);

            Assert.IsNotNull(res);
            Assert.IsFalse(res.success);
        }

        [Test]
        public async Task Should_fail_fetch_modify_data_invalid_id()
        {
            var res = await _dataManagementService.GetModificationDataJsonAsync(9999);

            Assert.IsNull(res);
        }

        [Test]
        public async Task Should_fail_fetch_entries_invaid_class_id()
        {
            var res = await _dataManagementService.GetEntriesJsonAsync(9999);

            Assert.IsNotNull(res);
            Assert.IsEmpty(res);
        }

        [Test]
        public async Task Should_fail_missing_model()
        {
            var res = await _dataManagementService.CreateOrUpdateAsync(null!);

            Assert.IsNotNull(res);
            Assert.IsFalse(res.success);
        }

        #endregion


        private int GetMaxNumberInJournal(OrganizationalClass orgClass)
            => orgClass.Students.Max(x => x.NumberInJournal);
    }
}
