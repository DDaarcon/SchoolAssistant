using AppConfigurationEFCore;
using SchoolAssistant.DAL.Enums;
using SchoolAssistant.DAL.Help.AppConfiguration;
using SchoolAssistant.DAL.Models.AppStructure;
using SchoolAssistant.Infrastructure.Enums.Schedule;
using SchoolAssistant.Infrastructure.Models.ScheduleDisplay;

namespace SchoolAssistant.Logic.ScheduleDisplay
{
    public interface IFetchSchedDisplayConfigService
    {
        Task<ScheduleConfigJson> FetchForAsync(User forUser);
    }

    [Injectable]
    public class FetchSchedDisplayConfigService : IFetchSchedDisplayConfigService
    {
        readonly IAppConfiguration<AppConfigRecords> _configRepo;

        public FetchSchedDisplayConfigService(
            IAppConfiguration<AppConfigRecords> configRepo)
        {
            _configRepo = configRepo;
        }

        public async Task<ScheduleConfigJson> FetchForAsync(User forUser)
        {
            return new ScheduleConfigJson
            {
                defaultLessonDuration = await _configRepo.Records.DefaultLessonDuration.GetAsync().ConfigureAwait(false) ?? 45,
                startHour = await _configRepo.Records.ScheduleStartHour.GetAsync().ConfigureAwait(false) ?? 7,
                endHour = await _configRepo.Records.ScheduleEndhour.GetAsync().ConfigureAwait(false) ?? 18,
                hiddenDays = (await _configRepo.Records.HiddenDays.GetAsync().ConfigureAwait(false) ?? Enumerable.Empty<DayOfWeek>()).ToArray(),
                @for = GetScheduleViewerTypeFromUser(forUser)
            };
        }

        private ScheduleViewerType GetScheduleViewerTypeFromUser(User user)
        {
            // TODO: temporary solution
            return user.Type switch
            {
                UserType.Student => ScheduleViewerType.Student,
                UserType.Teacher => ScheduleViewerType.Teacher,
                UserType.SystemAdmin => ScheduleViewerType.Student,
                _ => throw new Exception("Schedule for this type of user is not yet implemented")
            };
        }
    }
}
