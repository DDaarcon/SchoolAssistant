using Microsoft.AspNetCore.Identity;
using SchoolAssistant.DAL.Attributes;
using SchoolAssistant.DAL.Enums;
using SchoolAssistant.DAL.Models.AppStructure;
using SchoolAssistant.DAL.Repositories;

namespace SchoolAssistant.DAL
{
    public interface IDefaultDataSeeder
    {
        Task SeedAppConfigAsync();
        Task SeedRolesAndAdminAsync();
        Task SeedRolesAsync();
        Task SeedSystemAdminAsync();
    }

    [Injectable(ServiceLifetime.Scoped)]
    public class DefaultDataSeeder : IDefaultDataSeeder
    {
        private readonly RoleManager<Role> _roleManager;
        private readonly UserManager<User> _userManager;
        private readonly IAppConfigRepository _configRepo;

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
            UserManager<User> userManager,
            IAppConfigRepository configRepo)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _configRepo = configRepo;
        }

        public async Task SeedRolesAndAdminAsync()
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

                    description.CopyPermissionsTo(role);
                    await _roleManager.CreateAsync(role);
                }
                else
                {
                    description.CopyPermissionsTo(role);
                    await _roleManager.UpdateAsync(role);
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


        public async Task SeedAppConfigAsync()
        {
            var results = new bool[]
            {
                await _configRepo.ScheduleArrangerCellHeight.SetIfEmptyAsync(5),
                await _configRepo.ScheduleArrangerCellDuration.SetIfEmptyAsync(5),
                await _configRepo.DefaultLessonDuration.SetIfEmptyAsync(45),
                await _configRepo.ScheduleStartHour.SetIfEmptyAsync(7),
                await _configRepo.ScheduleEndhour.SetIfEmptyAsync(18),
                await _configRepo.DefaultRoomName.SetIfEmptyAsync("Sala")
            };

            if (results.Any(x => x))
                await _configRepo.SaveAsync();
        }
    }
}
