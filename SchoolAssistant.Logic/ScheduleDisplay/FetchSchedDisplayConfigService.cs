using SchoolAssistant.DAL.Enums;
using SchoolAssistant.DAL.Models.AppStructure;
using SchoolAssistant.DAL.Repositories;
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
        readonly IAppConfigRepository _configRepo;

        public FetchSchedDisplayConfigService(
            IAppConfigRepository configRepo)
        {
            _configRepo = configRepo;
        }

        public async Task<ScheduleConfigJson> FetchForAsync(User forUser)
        {
            return new ScheduleConfigJson
            {
                defaultLessonDuration = await _configRepo.DefaultLessonDuration.GetAsync() ?? 45,
                startHour = await _configRepo.ScheduleStartHour.GetAsync() ?? 7,
                endHour = await _configRepo.ScheduleEndhour.GetAsync() ?? 18,
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
