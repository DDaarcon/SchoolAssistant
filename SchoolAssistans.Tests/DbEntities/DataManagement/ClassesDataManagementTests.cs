using NUnit.Framework;
using SchoolAssistant.DAL.Models.SchoolYears;
using SchoolAssistant.DAL.Models.StudentsOrganization;
using SchoolAssistant.DAL.Repositories;
using SchoolAssistant.Infrastructure.Models.DataManagement.Classes;
using SchoolAssistant.Logic.DataManagement.Classes;
using System.Threading.Tasks;

namespace SchoolAssistans.Tests.DbEntities.DataManagement
{
    public class ClassesDataManagementTests
    {
        private ISchoolYearService _schoolYearService;
        private IClassDataManagementService _classDataManagementService;
        private IRepository<OrganizationalClass> _organizationalClassRepository;

        [OneTimeSetUp]
        public void Setup()
        {
            TestDatabase.CreateContext(TestServices.Collection);

            _organizationalClassRepository = new Repository<OrganizationalClass>(TestDatabase.Context, null);
            var schoolYearRepo = new Repository<SchoolYear>(TestDatabase.Context, null);
            _schoolYearService = new SchoolYearService(schoolYearRepo);

            var modifyClassesJsonSvc = new ModifyClassFromJsonService(_organizationalClassRepository, _schoolYearService);

            _classDataManagementService = new ClassDataManagementService(modifyClassesJsonSvc, _organizationalClassRepository);
        }

        [OneTimeTearDown]
        public void Teardown()
        {
            TestDatabase.DisposeContext();
        }

        [SetUp]
        public async Task SetupOneAsync()
        {
            await TestDatabase.ClearDataAsync<OrganizationalClass>();
        }


        private ClassDetailsJson SampleClassDetails => new ClassDetailsJson
        {
            grade = 1,
            distinction = "e",
            specialization = "Technik informatyk"
        };

        private async Task<OrganizationalClass> AddToDB_3_e_TechnikMechatronik_Async()
        {
            var year = await _schoolYearService.GetOrCreateCurrentAsync();
            var orgClass = new OrganizationalClass
            {
                SchoolYearId = year.Id,
                Grade = 3,
                Distinction = "e",
                Specialization = "Technik mechatronik",

            };
            await _organizationalClassRepository.AddAsync(orgClass);
            await _organizationalClassRepository.SaveAsync();

            return orgClass;
        }
        private async Task<OrganizationalClass> AddToDB_4_a_TechnikMechatronik_Async()
        {
            var year = await _schoolYearService.GetOrCreateCurrentAsync();
            var orgClass = new OrganizationalClass
            {
                SchoolYearId = year.Id,
                Grade = 4,
                Distinction = "a",
                Specialization = "Technik mechatronik",

            };
            await _organizationalClassRepository.AddAsync(orgClass);
            await _organizationalClassRepository.SaveAsync();

            return orgClass;
        }




        [Test]
        public async Task Should_create_org_class_async()
        {
            var model = SampleClassDetails;

            var res = await _classDataManagementService.CreateOrUpdateAsync(model);

            Assert.IsTrue(res.success);
        }

        [Test]
        public async Task Should_update_org_class_async()
        {
            var orgCl = await AddToDB_3_e_TechnikMechatronik_Async();
            var model = SampleClassDetails;
            model.id = orgCl.Id;
            model.grade = 5;
            model.distinction = "mi";
            model.specialization = "Mechanik";

            var res = await _classDataManagementService.CreateOrUpdateAsync(model);

            Assert.IsTrue(res.success);
            Assert.AreEqual(orgCl.Grade, 5);
            Assert.AreEqual(orgCl.Distinction, "mi");
            Assert.AreEqual(orgCl.Specialization, "Mechanik");
        }

        [Test]
        public async Task Should_fetch_entities_async()
        {
            var orgCl1 = await AddToDB_3_e_TechnikMechatronik_Async();
            var orgCl2 = await AddToDB_4_a_TechnikMechatronik_Async();

            var res = await _classDataManagementService.GetEntriesJsonAsync();

            Assert.AreEqual(res.Length, 2);
            var first = res[0];
            Assert.AreEqual(first.name, orgCl1.Name);
            Assert.AreEqual(first.specialization, orgCl1.Specialization);
            Assert.AreEqual(first.amountOfStudents, orgCl1.Students.Count);
            var second = res[1];
            Assert.AreEqual(second.name, orgCl2.Name);
            Assert.AreEqual(second.specialization, orgCl2.Specialization);
            Assert.AreEqual(second.amountOfStudents, orgCl2.Students.Count);
        }

        [Test]
        public async Task Should__fetch_empty_entities_array_async()
        {
            var res = await _classDataManagementService.GetEntriesJsonAsync();

            Assert.AreEqual(res.Length, 0);
        }

        [Test]
        public async Task Should_fetch_modification_data_async()
        {
            var orgCl = await AddToDB_3_e_TechnikMechatronik_Async();

            var res = await _classDataManagementService.GetModificationDataJsonAsync(orgCl.Id);

            Assert.IsNotNull(res);
            Assert.IsNotNull(res.data);
            Assert.AreEqual(res.data.id, orgCl.Id);
            Assert.AreEqual(res.data.grade, orgCl.Grade);
            Assert.AreEqual(res.data.distinction, orgCl.Distinction);
            Assert.AreEqual(res.data.specialization, orgCl.Specialization);
        }



        #region Fails

        [Test]
        public async Task Should_fail_creating_grade_below_0_async()
        {
            var model = SampleClassDetails;
            model.grade = -1;

            var res = await _classDataManagementService.CreateOrUpdateAsync(model);

            Assert.IsFalse(res.success);
        }

        [Test]
        public async Task Should_fail_creating_identical_class_exists_async()
        {
            var orgCl = await AddToDB_3_e_TechnikMechatronik_Async();

            var model = SampleClassDetails;
            model.grade = orgCl.Grade;
            model.distinction = orgCl.Distinction;

            var res = await _classDataManagementService.CreateOrUpdateAsync(model);

            Assert.IsFalse(res.success);
        }

        [Test]
        public async Task Should_fail_updating_invalid_id_async()
        {
            var model = SampleClassDetails;
            model.id = 99999;

            var res = await _classDataManagementService.CreateOrUpdateAsync(model);

            Assert.IsFalse(res.success);
        }

        [Test]
        public async Task Should_fail_updating_identical_class_exists_async()
        {
            var orgCl1 = await AddToDB_3_e_TechnikMechatronik_Async();
            var orgCl = await AddToDB_4_a_TechnikMechatronik_Async();
            var model = SampleClassDetails;
            model.id = orgCl.Id;
            model.grade = orgCl1.Grade;
            model.distinction = orgCl1.Distinction;

            var res = await _classDataManagementService.CreateOrUpdateAsync(model);

            Assert.IsFalse(res.success);
        }

        [Test]
        public async Task Should_fail_missing_model_async()
        {
            var res = await _classDataManagementService.CreateOrUpdateAsync(null!);

            Assert.IsFalse(res.success);
        }

        [Test]
        public async Task Should_fail_fetch_modification_data_invalid_id_async()
        {
            var res = await _classDataManagementService.GetModificationDataJsonAsync(999);

            Assert.IsNull(res);
        }

        #endregion
    }
}
