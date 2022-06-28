using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolAssistant.DAL.Enums;
using SchoolAssistant.DAL.Repositories;
using SchoolAssistant.Infrastructure.Models.ConductingClasses.ScheduledLessonsList;
using SchoolAssistant.Infrastructure.Models.ScheduleDisplay;
using SchoolAssistant.Infrastructure.Models.ScheduleShared;
using SchoolAssistant.Logic.ConductingClasses.ScheduledLessonsList;
using SchoolAssistant.Logic.ScheduleDisplay;

namespace SchoolAssistant.Web.Pages.Dashboard
{
    [Authorize(Roles = "Teacher")]
    public class TeacherModel : MyPageModel
    {
        private readonly IFetchSchedDisplayConfigService _fetchScheduleConfigSvc;
        private readonly ITeacherScheduleService _scheduleSvc;

        private readonly IFetchScheduledLessonListEntriesService _scheduledLessonsListSvc;
        private readonly IFetchScheduledLessonListConfigService _scheduledLessonsListConfigSvc;

        public ScheduleConfigJson ScheduleConfig { get; set; } = null!;
        public ScheduleDayLessonsJson<LessonJson>[] ScheduleLessons { get; set; } = null!;

        public ScheduledLessonListEntriesJson ScheduledLessonListEntries { get; set; } = null!;
        public ScheduledLessonListConfigJson ScheduledLessonListConfig { get; set; } = null!;

        public TeacherModel(
            IUserRepository userRepo,
            IFetchSchedDisplayConfigService fetchScheduleConfigSvc,
            ITeacherScheduleService scheduleSvc,
            IFetchScheduledLessonListEntriesService scheduledLessonsListSvc,
            IFetchScheduledLessonListConfigService scheduledLessonsListConfigSvc) : base(userRepo)
        {
            _fetchScheduleConfigSvc = fetchScheduleConfigSvc;
            _scheduleSvc = scheduleSvc;
            _scheduledLessonsListSvc = scheduledLessonsListSvc;
            _scheduledLessonsListConfigSvc = scheduledLessonsListConfigSvc;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            if (!await FetchAndValidateIfUserOfTypeAsync(UserType.Teacher).ConfigureAwait(false))
                return RedirectToStart;

            SetVersionInViewData();

            ScheduleConfig = await _fetchScheduleConfigSvc.FetchForAsync(_User!).ConfigureAwait(false);
            ScheduleLessons = (await _scheduleSvc.GetModelForCurrentYearAsync(_User.TeacherId!.Value).ConfigureAwait(false))!;

            ScheduledLessonListEntries = (await _scheduledLessonsListSvc.GetModelForTeacherAsync(_User.TeacherId!.Value, new FetchScheduledLessonsRequestModel
            {
                From = DateTime.Now,
                LimitTo = 6
            }).ConfigureAwait(false))!;
            ScheduledLessonListConfig = await _scheduledLessonsListConfigSvc.GetDefaultConfigAsync().ConfigureAwait(false);

            return Page();
        }
    }
}
