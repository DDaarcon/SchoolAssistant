using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using SchoolAssistant.DAL;
using SchoolAssistant.DAL.Attributes;
using SchoolAssistant.DAL.Enums;
using SchoolAssistant.DAL.Models.AppStructure;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolAssistans.Tests.DbEntities
{
    public class SeedRolesAndUserTests
    {
        private UserManager<User> _userManager = null!;
        private RoleManager<Role> _roleManager = null!;

        private DefaultDataSeeder _dataSeeder = null!;

        [OneTimeSetUp]
        public void Setup()
        {
            TestDatabase.CreateContext(TestServices.Collection);

            RegisterIdentity();

            _dataSeeder = new DefaultDataSeeder(_roleManager, _userManager);
        }

        private void RegisterIdentity()
        {
            TestServices.Collection.AddIdentity<User, Role>().AddEntityFrameworkStores<SADbContext>();
            TestServices.AddService<ILogger<UserManager<User>>, Logger<UserManager<User>>>();
            TestServices.AddService<ILogger<RoleManager<Role>>, Logger<RoleManager<Role>>>();

            _userManager = TestServices.GetService<UserManager<User>>();
            _roleManager = TestServices.GetService<RoleManager<Role>>();
        }

        [Test]
        public async Task SeedDefaultRolesAndUsers()
        {
            await _dataSeeder.SeedAllAsync();

            var rolesEnum = Enum.GetValues<UserType>();
            var roles = _roleManager.Roles.ToList();

            Assert.IsTrue(roles.Any());
            Assert.IsTrue(roles.Count() == rolesEnum.Length);

            for (int i = 0; i < rolesEnum.Length; i++)
            {
                var roleStored = roles.ElementAt(i);
                var roleGener = rolesEnum.ElementAt(i).GetUserTypeAttribute();

                Assert.IsNotNull(roleStored);
                Assert.IsNotNull(roleGener);
                Assert.IsTrue(roleStored.Name == roleGener!.RoleName);
            }
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            TestDatabase.DisposeContext();
        }
    }
}
