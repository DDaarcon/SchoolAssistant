using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using SchoolAssistant.DAL;
using SchoolAssistant.DAL.Models.AppStructure;
using SchoolAssistant.DAL.Models.Staff;
using SchoolAssistant.DAL.Models.StudentsParents;
using SchoolAssistant.DAL.Models.Subjects;
using SchoolAssistant.DAL.Repositories;
using SchoolAssistant.Infrastructure.Enums.Users;
using SchoolAssistant.Infrastructure.Models.UsersManagement;
using SchoolAssistant.Logic.UsersManagement;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolAssistans.Tests.DbEntities.UsersManagement
{
    public class FetchUserListEntriesTests : BaseDbEntitiesTests
    {
        private UserManager<User> _userManager = null!;
        private IUserRepository _userRepo = null!;
        private IFetchUserListEntriesService _fetchService = null!;

        private IRepository<StudentRegisterRecord> _studentRecordRepo = null!;
        private IRepository<Subject> _subjectRepo = null!;
        private IRepository<Teacher> _teacherRepo = null!;

        private IEnumerable<User> _studentUsers = null!;
        private IEnumerable<User> _teacherUsers = null!;

        [OneTimeSetUp]
        public void Setup()
        {
            TestServices.Collection.AddIdentity<User, Role>().AddEntityFrameworkStores<SADbContext>();
            TestServices.AddService<ILogger<UserManager<User>>, Logger<UserManager<User>>>();
            TestServices.AddService<ILogger<RoleManager<Role>>, Logger<RoleManager<Role>>>();
        }

        protected override async Task CleanDataAfterEveryTestAsync()
        {
            await TestDatabase.ClearDataAsync<Subject>();
            await TestDatabase.ClearDataAsync<User>();
        }

        protected override async Task SetupDataForEveryTestAsync()
        {
            _studentUsers = await FakeData.UsersWithRandomStudents(_userRepo, 30);
            _teacherUsers = await FakeData.UsersWithRandomTeachers(_userRepo, _teacherRepo, 30);
        }

        protected override void SetupServices()
        {
            _userManager = TestServices.GetService<UserManager<User>>();
            _userRepo = new UserRepository(_Context, null, _userManager);
            _studentRecordRepo = new Repository<StudentRegisterRecord>(_Context, null);
            _subjectRepo = new Repository<Subject>(_Context, null);
            _teacherRepo = new Repository<Teacher>(_Context, null);
            _fetchService = new FetchUserListEntriesService(_userRepo);
        }

        [Test]
        public async Task Should_fetch_all_student_users()
        {
            var res = await _fetchService.FetchAsync(new FetchUsersListRequestJson
            {
                ofType = UserTypeForManagement.Student
            });

            Assert.IsNotNull(res);
            Assert.AreEqual(_studentUsers.Count(), res!.Length);

            Assert.IsTrue(res.All(x => _studentUsers.Any(u =>
                u.UserName == x.userName
                && u.Student?.FirstName == x.firstName
                && u.Student?.LastName == x.lastName
                && u.Email == x.email)));

            Assert.IsTrue(res.All(x => x is StudentUsersListEntryJson));
        }

        [Test]
        public async Task Should_fetch_all_teacher_users()
        {
            var res = await _fetchService.FetchAsync(new FetchUsersListRequestJson
            {
                ofType = UserTypeForManagement.Teacher
            });

            Assert.IsNotNull(res);
            Assert.AreEqual(_teacherUsers.Count(), res!.Length);

            Assert.IsTrue(res.All(x => _teacherUsers.Any(u =>
                u.UserName == x.userName
                && u.Teacher?.FirstName == x.firstName
                && u.Teacher?.LastName == x.lastName
                && u.Email == x.email)));
        }

        [Test]
        public async Task Should_fetch_next_10_student_users()
        {
            var res = await _fetchService.FetchAsync(new FetchUsersListRequestJson
            {
                skip = 10,
                take = 10,
                ofType = UserTypeForManagement.Student
            });

            var test = _studentUsers
                .OrderBy(x => x.Student!.LastName)
                .ThenBy(x => x.Student!.FirstName)
                .Skip(10)
                .Take(10);

            Assert.IsNotNull(res);
            Assert.AreEqual(test.Count(), res!.Length);

            Assert.IsTrue(res.All(x => test.Any(u =>
                u.UserName == x.userName
                && u.Student?.FirstName == x.firstName
                && u.Student?.LastName == x.lastName
                && u.Email == x.email)));
        }

        [Test]
        public async Task Should_fetch_next_10_teacher_users()
        {
            var res = await _fetchService.FetchAsync(new FetchUsersListRequestJson
            {
                skip = 10,
                take = 10,
                ofType = UserTypeForManagement.Teacher
            });

            var test = _teacherUsers
                .OrderBy(x => x.Teacher!.LastName)
                .ThenBy(x => x.Teacher!.FirstName)
                .Skip(10)
                .Take(10);

            Assert.IsNotNull(res);
            Assert.AreEqual(test.Count(), res!.Length);

            Assert.IsTrue(res.All(x => test.Any(u =>
                u.UserName == x.userName
                && u.Teacher?.FirstName == x.firstName
                && u.Teacher?.LastName == x.lastName
                && u.Email == x.email)));
        }

        [Test]
        public async Task Should_fetch_first_10_student_users()
        {
            var res = await _fetchService.FetchAsync(new FetchUsersListRequestJson
            {
                take = 10,
                ofType = UserTypeForManagement.Student
            });

            var test = _studentUsers
                .OrderBy(x => x.Student!.LastName)
                .ThenBy(x => x.Student!.FirstName)
                .Take(10);

            Assert.IsNotNull(res);
            Assert.AreEqual(test.Count(), res!.Length);

            Assert.IsTrue(res.All(x => test.Any(u =>
                u.UserName == x.userName
                && u.Student?.FirstName == x.firstName
                && u.Student?.LastName == x.lastName
                && u.Email == x.email)));
        }

        [Test]
        public async Task Should_fetch_first_10_teacher_users()
        {
            var res = await _fetchService.FetchAsync(new FetchUsersListRequestJson
            {
                take = 10,
                ofType = UserTypeForManagement.Teacher
            });

            var test = _teacherUsers
                .OrderBy(x => x.Teacher!.LastName)
                .ThenBy(x => x.Teacher!.FirstName)
                .Take(10);

            Assert.IsNotNull(res);
            Assert.AreEqual(test.Count(), res!.Length);

            Assert.IsTrue(res.All(x => test.Any(u =>
                u.UserName == x.userName
                && u.Teacher?.FirstName == x.firstName
                && u.Teacher?.LastName == x.lastName
                && u.Email == x.email)));
        }

        [Test]
        public async Task Should_ignore_negative_numbers()
        {
            var res = await _fetchService.FetchAsync(new FetchUsersListRequestJson
            {
                skip = -5,
                take = -10,
                ofType = UserTypeForManagement.Teacher
            });

            Assert.IsNotNull(res);
            Assert.AreEqual(_teacherUsers.Count(), res!.Length);

            Assert.IsTrue(res.All(x => _teacherUsers.Any(u =>
                u.UserName == x.userName
                && u.Teacher?.FirstName == x.firstName
                && u.Teacher?.LastName == x.lastName
                && u.Email == x.email)));
        }

        [Test]
        public async Task Should_return_empty_for_invalid_type()
        {
            var res = await _fetchService.FetchAsync(new FetchUsersListRequestJson
            {
                ofType = (UserTypeForManagement)999
            });

            Assert.IsNotNull(res);
            Assert.AreEqual(0, res!.Length);
        }

        [Test]
        public async Task Should_return_empty_for_missing_model()
        {
            var res = await _fetchService.FetchAsync(null!);

            Assert.IsNotNull(res);
            Assert.AreEqual(0, res!.Length);
        }
    }
}
