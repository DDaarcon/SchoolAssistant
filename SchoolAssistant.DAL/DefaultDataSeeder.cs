using Microsoft.AspNetCore.Identity;
using SchoolAssistant.DAL.Attributes;
using SchoolAssistant.DAL.Enums;
using SchoolAssistant.DAL.Models.AppStructure;

namespace SchoolAssistant.DAL
{
    public interface IDefaultDataSeeder
    {
        Task SeedAllAsync();
        Task SeedRolesAsync();
        Task SeedSystemAdminAsync();
    }

    [Injectable(ServiceLifetime.Transcient)]
    public class DefaultDataSeeder : IDefaultDataSeeder
    {
        private readonly RoleManager<Role> _roleManager;
        private readonly UserManager<User> _userManager;

        private readonly User _systemAdmin = new()
        {
            Email = "fake.email@mail.com",
            NormalizedEmail = "FAKE.EMAIL@MAIL.COM",
            UserName = "SuperAdmin",
            NormalizedUserName = "SUPERADMIN",
            EmailConfirmed = true,
            PhoneNumber = "+48111111111",
            PhoneNumberConfirmed = true,
            Type = UserType.SystemAdmin,
            SecurityStamp = Guid.NewGuid().ToString("D")
        };

        private readonly string _systemAdminPassword = "!SystemAdmin12";

        public DefaultDataSeeder(
            RoleManager<Role> roleManager,
            UserManager<User> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task SeedAllAsync()
        {
            await SeedRolesAsync();
            await SeedSystemAdminAsync();
        }

        public async Task SeedRolesAsync()
        {
            var descriptions = UserTypeHelper.GetUserTypeDescriptions();

            foreach (var description in descriptions)
            {
                var role = await _roleManager.FindByNameAsync(description.RoleName);

                if (role is null)
                {
                    role = new Role
                    {
                        Name = description.RoleName,
                        NormalizedName = description.RoleName?.ToUpper()
                    };
                    await _roleManager.CreateAsync(role);
                }
                else
                {

                    //await _roleManager.UpdateAsync(role);
                }
            }
        }

        public async Task SeedSystemAdminAsync()
        {
            var user = await _userManager.FindByNameAsync(_systemAdmin.UserName);

            if (user is null)
            {
                user = _systemAdmin;
                await _userManager.CreateAsync(user);
                await _userManager.AddPasswordAsync(user, _systemAdminPassword);

                await _userManager.AddToRoleAsync(user, UserType.SystemAdmin.GetUserTypeAttribute()!.RoleName);
            }
        }
    }
}
