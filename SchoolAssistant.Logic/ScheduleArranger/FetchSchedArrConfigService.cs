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
                defaultLessonDuration = await _configRepo.Records.DefaultLessonDuration.GetAsync() ?? 45,
                cellDuration = await _configRepo.Records.ScheduleArrangerCellDuration.GetAsync() ?? 5,
                cellHeight = await _configRepo.Records.ScheduleArrangerCellHeight.GetAsync() ?? 5,
                startHour = await _configRepo.Records.ScheduleStartHour.GetAsync() ?? 7,
                endHour = await _configRepo.Records.ScheduleEndhour.GetAsync() ?? 18
            };
        }
    }
}
