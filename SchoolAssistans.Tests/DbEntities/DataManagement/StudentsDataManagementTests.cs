﻿using Microsoft.EntityFrameworkCore;
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
        private ISchoolYearService _schoolYearService = null!;
        private IStudentsDataManagementService _dataManagementService = null!;
        private IStudentRegisterRecordsDataManagementService _registerDataManagementService = null!;
        private IRepository<OrganizationalClass> _orgClassRepo = null!;
        private IRepository<Student> _studentRepo = null!;
        private IRepository<StudentRegisterRecord> _studentRegRepo = null!;



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

            _dataManagementService = new StudentsDataManagementService(
                _orgClassRepo,
                _studentRepo,
                _registerDataManagementService);
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



        [Test]
        public async Task Should_fetch_enties()
        {
            await FakeData.Class_2a_Mechanics_15Students(
                Year,
                _orgClassRepo);

            var orgClass = await FakeData.Class_3b_27Students(
                Year,
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
                Year,
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
                Year,
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

            orgClass = await _orgClassRepo.GetByIdAsync(orgClass.Id);
            var student = orgClass!.Students.FirstOrDefault();

            Assert.IsNotNull(student);
            Assert.AreEqual(model.registerRecordId, student!.Info.Id);
            Assert.AreEqual(model.numberInJournal, student.NumberInJournal);
        }

        [Test]
        public async Task Should_change_number_in_journal()
        {
            var orgClass = await FakeData.Class_2a_Mechanics_15Students(
                Year,
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

            student = orgClass.Students.FirstOrDefault(x => x.NumberInJournal == newNumber);

            Assert.IsNotNull(student);
        }

        [Test]
        public async Task Should_change_class()
        {
            var orgClass1 = await FakeData.Class_2a_Mechanics_15Students(
                Year,
                _orgClassRepo);
            var orgClass2 = await FakeData.Class_3b_27Students(
                Year,
                _orgClassRepo);
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
        public async Task Should_fail_invalid_id()
        {
            var orgClass = await FakeData.Class_2a_Mechanics_15Students(
                Year,
                _orgClassRepo);
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
        public async Task Should_fail_invalid_class_id()
        {
            var orgClass = await FakeData.Class_2a_Mechanics_15Students(
                Year,
                _orgClassRepo);
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
        public async Task Should_fail_invalid_register_record_id()
        {
            var orgClass = await FakeData.Class_2a_Mechanics_15Students(
                Year,
                _orgClassRepo);
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
        public async Task Should_fail_missing_register_record_id()
        {
            var orgClass = await FakeData.Class_2a_Mechanics_15Students(
                Year,
                _orgClassRepo);
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
        public async Task Should_fail_missing_class()
        {
            var orgClass = await FakeData.Class_2a_Mechanics_15Students(
                Year,
                _orgClassRepo);
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
        public async Task Should_fail_taken_register_id()
        {
            var orgClass = await FakeData.Class_2a_Mechanics_15Students(
                Year,
                _orgClassRepo);
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

        public async Task Should_fail_missing_model()
        {
            var res = await _dataManagementService.CreateOrUpdateAsync(null!);

            Assert.IsNotNull(res);
            Assert.IsFalse(res.success);
        }

        #endregion
    }
}