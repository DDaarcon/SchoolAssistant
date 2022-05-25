﻿using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using SchoolAssistant.DAL;
using SchoolAssistant.DAL.Enums;
using SchoolAssistant.DAL.Models.AppStructure;
using SchoolAssistant.DAL.Models.Staff;
using SchoolAssistant.DAL.Models.StudentsOrganization;
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
    public class AddUserTests : BaseDbEntitiesTests
    {
        private IAddUserService _addUserSvc = null!;

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
            await TestDatabase.ClearDataAsync<Subject>();
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
            var userManager = TestServices.GetService<UserManager<User>>();
            _orgClassRepo = new Repository<OrganizationalClass>(_Context, null);
            _userRepo = new UserRepository(_Context, null, userManager);
            _teacherRepo = new Repository<Teacher>(_Context, null);

            _addUserSvc = new AddUserService();
        }


        [Test]
        public async Task Should_add_student_user()
        {
            var student = _orgClass1.Students.First();

            var res = await _addUserSvc.AddAsync(new AddUserRequestJson
            {
                userName = "Some_ranDOm_usernaMe2412#",
                email = "some.email@domain.com",
                phoneNumber = "4890099090",
                relatedType = UserTypeForManagement.Student,
                relatedId = student.Id
            });

            AssertResponseSuccess(res);

            var user = await _userRepo.Manager.FindByNameAsync("Some_ranDOm_usernaMe2412#");
            Assert.IsNotNull(user);
            Assert.IsTrue(await _userRepo.Manager.CheckPasswordAsync(user, res.temporaryPassword));
            Assert.AreEqual(UserType.Student, user.Type);
            Assert.IsNotNull(user.Student);
            Assert.AreEqual(student.Id, user.Student!.Id);
            Assert.AreEqual("some.email@domain.com", user.Email);
            Assert.AreEqual("4890099090", user.PhoneNumber);
        }

        [Test]
        public async Task Should_add_teacher_user()
        {
            var teacher = _teachers.First();

            var res = await _addUserSvc.AddAsync(new AddUserRequestJson
            {
                userName = "teacherUser",
                email = "some.email@domain.com",
                relatedType = UserTypeForManagement.Teacher,
                relatedId = teacher.Id
            });

            AssertResponseSuccess(res);

            var user = await _userRepo.Manager.FindByNameAsync("teacherUser");
            Assert.IsNotNull(user);
            Assert.IsTrue(await _userRepo.Manager.CheckPasswordAsync(user, res.temporaryPassword));
            Assert.AreEqual(UserType.Teacher, user.Type);
            Assert.IsNotNull(user.Teacher);
            Assert.AreEqual(teacher.Id, user.Teacher!.Id);
            Assert.AreEqual("some.email@domain.com", user.Email);
            Assert.IsNull(user.PhoneNumber);
        }

        #region Fails

        [Test]
        public async Task Should_fail_missing_username()
        {
            var teacher = _teachers.First();

            var res = await _addUserSvc.AddAsync(new AddUserRequestJson
            {
                email = "some.email@domain.com",
                relatedType = UserTypeForManagement.Teacher,
                relatedId = teacher.Id
            });

            AssertResponseFail(res);
        }

        [Test]
        public async Task Should_fail_empty_username()
        {
            var teacher = _teachers.First();

            var res = await _addUserSvc.AddAsync(new AddUserRequestJson
            {
                userName = "",
                email = "some.email@domain.com",
                relatedType = UserTypeForManagement.Teacher,
                relatedId = teacher.Id
            });

            AssertResponseFail(res);
        }

        [Test]
        public async Task Should_fail_empty_email()
        {
            var teacher = _teachers.First();

            var res = await _addUserSvc.AddAsync(new AddUserRequestJson
            {
                userName = "some_user_name",
                email = "",
                relatedType = UserTypeForManagement.Teacher,
                relatedId = teacher.Id
            });

            AssertResponseFail(res);
        }

        [Test]
        public async Task Should_fail_missing_email()
        {
            var teacher = _teachers.First();

            var res = await _addUserSvc.AddAsync(new AddUserRequestJson
            {
                userName = "some_user_name",
                relatedType = UserTypeForManagement.Teacher,
                relatedId = teacher.Id
            });

            AssertResponseFail(res);
        }

        [Test]
        public async Task Should_fail_invalid_type()
        {
            var teacher = _teachers.First();

            var res = await _addUserSvc.AddAsync(new AddUserRequestJson
            {
                userName = "some_user_name",
                email = "some.email@domain.com",
                relatedType = (UserTypeForManagement)(-10),
                relatedId = teacher.Id
            });

            AssertResponseFail(res);
        }

        [Test]
        public async Task Should_fail_invalid_id()
        {
            var res = await _addUserSvc.AddAsync(new AddUserRequestJson
            {
                userName = "some_user_name",
                email = "some.email@domain.com",
                relatedType = UserTypeForManagement.Teacher,
                relatedId = 9999
            });

            AssertResponseFail(res);
        }

        [Test]
        public async Task Should_fail_spaces_in_username()
        {
            var teacher = _teachers.First();

            var res = await _addUserSvc.AddAsync(new AddUserRequestJson
            {
                userName = "some user name",
                email = "some.email@domain.com",
                relatedType = UserTypeForManagement.Teacher,
                relatedId = teacher.Id
            });

            AssertResponseFail(res);
        }

        [Test]
        public async Task Should_fail_teacher_has_user()
        {
            var teacher = _teacherUsers.First().Teacher;

            var res = await _addUserSvc.AddAsync(new AddUserRequestJson
            {
                userName = "some_user_name",
                email = "some.email@domain.com",
                relatedType = UserTypeForManagement.Teacher,
                relatedId = teacher!.Id
            });

            AssertResponseFail(res);
        }

        [Test]
        public async Task Should_fail_username_taken()
        {
            var teacher = _teachers.First();
            var user = _teacherUsers.First();

            var res = await _addUserSvc.AddAsync(new AddUserRequestJson
            {
                userName = user.UserName,
                email = "some.email@domain.com",
                relatedType = UserTypeForManagement.Teacher,
                relatedId = teacher.Id
            });

            AssertResponseFail(res);
        }

        [Test]
        public async Task Should_fail_email_taken()
        {
            var teacher = _teachers.First();
            var user = _teacherUsers.First();

            var res = await _addUserSvc.AddAsync(new AddUserRequestJson
            {
                userName = "some_user_name",
                email = user.Email,
                relatedType = UserTypeForManagement.Teacher,
                relatedId = teacher.Id
            });

            AssertResponseFail(res);
        }

        #endregion
    }
}
