using AppConfigurationEFCore;
using Microsoft.AspNetCore.Identity;
using SchoolAssistant.DAL.Attributes;
using SchoolAssistant.DAL.Enums;
using SchoolAssistant.DAL.Help.AppConfiguration;
using SchoolAssistant.DAL.Models.AppStructure;

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
        private readonly IAppConfiguration<AppConfigRecords> _configRepo;

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
            IAppConfiguration<AppConfigRecords> configRepo)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _configRepo = configRepo;
        }

        public async Task SeedRolesAndAdminAsync()
        {
            await SeedRolesAsync().ConfigureAwait(false);
            await SeedSystemAdminAsync().ConfigureAwait(false);
        }

        public async Task SeedRolesAsync()
        {
            // TODO: verify if needed
            var descriptions = UserTypeHelper.GetUserTypeDescriptions();

            foreach (var description in descriptions)
            {
                var role = await _roleManager.FindByNameAsync(description.RoleName).ConfigureAwait(false);

                if (role is null)
                {
                    role = new Role
                    {
                        Name = description.RoleName,
                        NormalizedName = description.RoleName?.ToUpper()
                    };

                    description.CopyPermissionsTo(role);
                    await _roleManager.CreateAsync(role).ConfigureAwait(false);
                }
                else
                {
                    description.CopyPermissionsTo(role);
                    await _roleManager.UpdateAsync(role).ConfigureAwait(false);
                }
            }
        }

        public async Task SeedSystemAdminAsync()
        {
            var user = await _userManager.FindByNameAsync(_systemAdmin.UserName).ConfigureAwait(false);

            if (user is null)
            {
                user = _systemAdmin;
                await _userManager.CreateAsync(user).ConfigureAwait(false);
                await _userManager.AddPasswordAsync(user, _systemAdminPassword).ConfigureAwait(false);

                await _userManager.AddToRoleAsync(user, UserType.SystemAdmin.GetUserTypeAttribute()!.RoleName).ConfigureAwait(false);
            }
        }


        public async Task SeedAppConfigAsync()
        {
            var results = new bool[]
            {
                await _configRepo.Records.ScheduleArrangerCellHeight.SetIfEmptyAsync(5).ConfigureAwait(false),
                await _configRepo.Records.ScheduleArrangerCellDuration.SetIfEmptyAsync(5).ConfigureAwait(false),
                await _configRepo.Records.DefaultLessonDuration.SetIfEmptyAsync(45).ConfigureAwait(false),
                await _configRepo.Records.ScheduleStartHour.SetIfEmptyAsync(7).ConfigureAwait(false),
                await _configRepo.Records.ScheduleEndhour.SetIfEmptyAsync(18).ConfigureAwait(false),
                await _configRepo.Records.DefaultRoomName.SetIfEmptyAsync("Sala").ConfigureAwait(false),
                await _configRepo.Records.MinutesBeforeLessonConsideredClose.SetIfEmptyAsync(5).ConfigureAwait(false),
                await _configRepo.Records.HiddenDays.SetIfEmptyAsync(new DayOfWeek[] { DayOfWeek.Sunday, DayOfWeek.Saturday }).ConfigureAwait(false)
            };

            if (results.Any(x => x))
                await _configRepo.SaveAsync().ConfigureAwait(false);
        }
    }
}
