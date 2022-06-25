using Microsoft.AspNetCore.Mvc.RazorPages;
using SchoolAssistant.DAL.Enums;
using SchoolAssistant.DAL.Models.AppStructure;
using SchoolAssistant.DAL.Repositories;
using SchoolAssistant.Infrastructure.Models.ConductingClasses.ScheduledLessonsList;
using SchoolAssistant.Infrastructure.Models.ScheduleDisplay;
using SchoolAssistant.Infrastructure.Models.ScheduleShared;
using SchoolAssistant.Logic;
using SchoolAssistant.Logic.ConductingClasses.ScheduledLessonsList;
using SchoolAssistant.Logic.ScheduleDisplay;

namespace SchoolAssistant.Web.Pages.Dashboard
{
    public class TeacherModel : PageModel
    {
        private readonly IUserRepository _userRepo;
        private readonly IFetchSchedDisplayConfigService _fetchScheduleConfigSvc;
        private readonly ITeacherScheduleService _scheduleSvc;

        private readonly IFetchScheduledLessonListEntriesService _scheduledLessonsListSvc;
        private readonly IFetchScheduledLessonListConfigService _scheduledLessonsListConfigSvc;

        private User _user = null!;

        public ScheduleConfigJson ScheduleConfig { get; set; } = null!;
        public ScheduleDayLessonsJson<LessonJson>[] ScheduleLessons { get; set; } = null!;

        public ScheduledLessonListEntriesJson ScheduledLessonListEntries { get; set; } = null!;
        public ScheduledLessonListConfigJson ScheduledLessonListConfig { get; set; } = null!;

        public TeacherModel(
            IUserRepository userRepo,
            IFetchSchedDisplayConfigService fetchScheduleConfigSvc,
            ITeacherScheduleService scheduleSvc,
            IFetchScheduledLessonListEntriesService scheduledLessonsListSvc,
            IFetchScheduledLessonListConfigService scheduledLessonsListConfigSvc)
        {
            _userRepo = userRepo;
            _fetchScheduleConfigSvc = fetchScheduleConfigSvc;
            _scheduleSvc = scheduleSvc;
            _scheduledLessonsListSvc = scheduledLessonsListSvc;
            _scheduledLessonsListConfigSvc = scheduledLessonsListConfigSvc;
        }

        public async Task OnGetAsync()
        {
            await FetchAndValidateUserAsync().ConfigureAwait(false);

            ScheduleConfig = await _fetchScheduleConfigSvc.FetchForAsync(_user).ConfigureAwait(false);
            ScheduleLessons = (await _scheduleSvc.GetModelForCurrentYearAsync(_user.TeacherId!.Value).ConfigureAwait(false))!;

            ScheduledLessonListEntries = (await _scheduledLessonsListSvc.GetModelForTeacherAsync(_user.TeacherId!.Value, new FetchScheduledLessonsRequestModel
            {
                From = DateTime.Now,
                LimitTo = 6
            }).ConfigureAwait(false))!;
            ScheduledLessonListConfig = await _scheduledLessonsListConfigSvc.GetDefaultConfigAsync().ConfigureAwait(false);
        }


        private async Task<bool> FetchAndValidateUserAsync()
        {
            _user = (await _userRepo.GetCurrentAsync().ConfigureAwait(false))!;

            return _user.IsOfType(UserType.Teacher);
        }
    }
}
