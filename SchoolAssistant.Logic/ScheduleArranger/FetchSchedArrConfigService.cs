using AppConfigurationEFCore;
using SchoolAssistant.DAL.Help.AppConfiguration;
using SchoolAssistant.Infrastructure.Models.ScheduleArranger.PageModelToReact;

namespace SchoolAssistant.Logic.ScheduleArranger
{
    public interface IFetchSchedArrConfigService
    {
        Task<ScheduleArrangerConfigJson> FetchAsync();
    }

    [Injectable]
    public class FetchSchedArrConfigService : IFetchSchedArrConfigService
    {
        private readonly IAppConfiguration<AppConfigRecords> _configRepo;

        public FetchSchedArrConfigService(
            IAppConfiguration<AppConfigRecords> configRepo)
        {
            _configRepo = configRepo;
        }

        public async Task<ScheduleArrangerConfigJson> FetchAsync()
        {
            return new ScheduleArrangerConfigJson
            {
                defaultLessonDuration = await _configRepo.Records.DefaultLessonDuration.GetAsync().ConfigureAwait(false) ?? 45,
                cellDuration = await _configRepo.Records.ScheduleArrangerCellDuration.GetAsync().ConfigureAwait(false) ?? 5,
                cellHeight = await _configRepo.Records.ScheduleArrangerCellHeight.GetAsync().ConfigureAwait(false) ?? 5,
                startHour = await _configRepo.Records.ScheduleStartHour.GetAsync().ConfigureAwait(false) ?? 7,
                endHour = await _configRepo.Records.ScheduleEndhour.GetAsync().ConfigureAwait(false) ?? 18,
                hiddenDays = (await _configRepo.Records.HiddenDays.GetAsync().ConfigureAwait(false) ?? Enumerable.Empty<DayOfWeek>()).ToArray()
            };
        }
    }
}
