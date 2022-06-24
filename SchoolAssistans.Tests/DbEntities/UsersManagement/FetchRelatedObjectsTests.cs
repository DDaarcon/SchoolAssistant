using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using SchoolAssistant.DAL;
using SchoolAssistant.DAL.Models.AppStructure;
using SchoolAssistant.DAL.Models.Staff;
using SchoolAssistant.DAL.Models.StudentsOrganization;
using SchoolAssistant.DAL.Models.StudentsParents;
using SchoolAssistant.DAL.Repositories;
using SchoolAssistant.Infrastructure.Enums.Users;
using SchoolAssistant.Infrastructure.Models.UsersManagement;
using SchoolAssistant.Logic.UsersManagement;
using SchoolAssistant.Logic.UsersManagement.FetchUserRelatedObjectsHelp;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolAssistans.Tests.DbEntities.UsersManagement
{
    public class FetchRelatedObjectsTests : BaseDbEntitiesTests
    {
        private FetchUserRelatedObjectsService _fetchSvc = null!;

        private IRepository<OrganizationalClass> _orgClassRepo = null!;
        private IUserRepository _userRepo = null!;
        private IRepository<Teacher> _teacherRepo = null!;


        private OrganizationalClass _orgClass1 = null!;
        private IList<User> _studentUsers = null!;
        private IEnumerable<Teacher> _teachers = null!;
        private IList<User> _teacherUsers = null!;


        [OneTimeSetUp]
        public void Setup()
        {
            TestServices.Collection.AddIdentity<User, Role>().AddEntityFrameworkStores<SADbContext>();
            TestServices.AddService<ILogger<UserManager<User>>, Logger<UserManager<User>>>();
            TestServices.AddService<ILogger<RoleManager<Role>>, Logger<RoleManager<Role>>>();
        }

        protected override async Task CleanDataAfterEveryTestAsync()
        {
            await TestDatabase.ClearDataAsync<User>();
            await TestDatabase.ClearDataAsync<Teacher>();
            await TestDatabase.ClearDataAsync<Student>();
            await TestDatabase.ClearDataAsync<StudentRegisterRecord>();
            await TestDatabase.ClearDataAsync<OrganizationalClass>();
        }

        protected override async Task SetupDataForEveryTestAsync()
        {
            _orgClass1 = await FakeData.Class_2a_Mechanics_15Students(await _Year, _orgClassRepo);
            _studentUsers = await FakeData.UsersWithRandomStudents(_userRepo, 5);

            _teachers = await FakeData._XRandom_Teachers(_teacherRepo, 15);
            _teacherUsers = await FakeData.UsersWithRandomTeachers(_userRepo, _teacherRepo, 15);
        }

        protected override void SetupServices()
        {
            var httpContextAccessor = TestServices.GetService<IHttpContextAccessor>();

            var userManager = TestServices.GetService<UserManager<User>>();
            _orgClassRepo = new Repository<OrganizationalClass>(_Context, null);
            _userRepo = new UserRepository(_Context, null, userManager, httpContextAccessor);
            _teacherRepo = new Repository<Teacher>(_Context, null);

            var studRegRecRepo = new Repository<StudentRegisterRecord>(_Context, null);
            var fetchStudents = new FetchStudentUserRelatedObjectsService(studRegRecRepo);

            var fetchTechers = new FetchTeacherUserRelatedObjectsService(_teacherRepo);
            _fetchSvc = new FetchUserRelatedObjectsService(fetchStudents, fetchTechers);
        }

        [Test]
        public async Task Should_fetch_all_students()
        {
            var res = await _fetchSvc.GetObjectsAsync(new FetchRelatedObjectsRequestJson
            {
                ofType = UserTypeForManagement.Student
            });

            var students = _orgClass1.Students.Select(x => x.Info);

            Assert.IsNotNull(res);
            Assert.AreEqual(students.Count(), res.Length);

            Assert.IsTrue(res.All(x => x is StudentUserRelatedObjectJson
                && students.Any(d =>
                    x.id == d.Id
                    && x.firstName == d.FirstName
                    && x.lastName == d.LastName
                    && x.email == d.Email
                    && ((StudentUserRelatedObjectJson)x).orgClass == _orgClass1.Name)));
        }

        [Test]
        public async Task Should_fetch_all_teachers()
        {
            var res = await _fetchSvc.GetObjectsAsync(new FetchRelatedObjectsRequestJson
            {
                ofType = UserTypeForManagement.Teacher
            });

            var teachers = _teachers;

            Assert.IsNotNull(res);
            Assert.AreEqual(teachers.Count(), res.Length);

            Assert.IsTrue(res.All(x => x is SimpleRelatedObjectJson
                && teachers.Any(d =>
                    x.id == d.Id
                    && x.firstName == d.FirstName
                    && x.lastName == d.LastName
                    && x.email == d.Email)));
        }
    }
}
