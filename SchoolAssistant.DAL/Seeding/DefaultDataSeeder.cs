using AppConfigurationEFCore;
using Microsoft.AspNetCore.Identity;
using SchoolAssistant.DAL.Attributes;
using SchoolAssistant.DAL.Help.AppConfiguration;
using SchoolAssistant.DAL.Models.AppStructure;
using SchoolAssistant.DAL.Seeding.Help;

namespace SchoolAssistant.DAL.Seeding
{
    public interface IDefaultDataSeeder
    {
        Task SeedAppConfigAsync();
        Task SeedRolesAndUsersAsync();
        Task SeedRolesAsync();
        Task SeedUsersAsync();
    }

    [Injectable(ServiceLifetime.Scoped)]
    public class DefaultDataSeeder : IDefaultDataSeeder
    {
        private readonly RoleManager<Role> _roleManager;
        private readonly IUserSeedingService _userSeedingSvc;
        private readonly IAppConfiguration<AppConfigRecords> _configRepo;

        public DefaultDataSeeder(
            RoleManager<Role> roleManager,
            IAppConfiguration<AppConfigRecords> configRepo,
            IUserSeedingService userSeedingSvc)
        {
            _roleManager = roleManager;
            _configRepo = configRepo;
            _userSeedingSvc = userSeedingSvc;
        }

        public async Task SeedRolesAndUsersAsync()
        {
            await SeedRolesAsync().ConfigureAwait(false);
            await SeedUsersAsync().ConfigureAwait(false);
        }

        public async Task SeedRolesAsync()
        {
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


        public async Task SeedUsersAsync()
        {
            await _userSeedingSvc.SeedAllAsync().ConfigureAwait(false);
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
                await _configRepo.Records.HiddenDays.SetIfEmptyAsync(new DayOfWeek[] { DayOfWeek.Sunday, DayOfWeek.Saturday }).ConfigureAwait(false),
                await _configRepo.Records.LessonsListTopicLengthLimit.SetIfEmptyAsync(70).ConfigureAwait(false)
            };

            if (results.Any(x => x))
                await _configRepo.SaveAsync().ConfigureAwait(false);
        }
    }
}
