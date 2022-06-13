using Microsoft.AspNetCore.Mvc.RazorPages;
using SchoolAssistant.DAL.Models.AppStructure;
using SchoolAssistant.DAL.Repositories;
using SchoolAssistant.Infrastructure.Models.ConductingClasses.ScheduledLessonsList;
using SchoolAssistant.Infrastructure.Models.ScheduleDisplay;
using SchoolAssistant.Infrastructure.Models.ScheduleShared;
using SchoolAssistant.Logic.ConductingClasses;
using SchoolAssistant.Logic.ScheduleDisplay;

namespace SchoolAssistant.Web.Pages.Dashboard
{
    public class TeacherModel : PageModel
    {
        private readonly IUserRepository _userRepo;
        private readonly IFetchSchedDisplayConfigService _fetchScheduleConfigSvc;
        private readonly ITeacherScheduleService _scheduleSvc;

        private readonly IScheduledLessonListService _scheduledLessonsListSvc;

        private User _user = null!;

        public ScheduleConfigJson ScheduleConfig { get; set; } = null!;
        public ScheduleDayLessonsJson<LessonJson>[] ScheduleLessons { get; set; } = null!;

        public ScheduledLessonListJson ScheduledLessonListModel { get; set; } = null!;
        public ScheduledLessonListEntriesJson ScheduledLessonListEntries { get; set; } = null!;
        public ScheduledLessonListConfigJson ScheduledLessonListConfig { get; set; } = null!;

        public TeacherModel(
            IUserRepository userRepo,
            IFetchSchedDisplayConfigService fetchScheduleConfigSvc,
            ITeacherScheduleService scheduleSvc,
            IScheduledLessonListService scheduledLessonsListSvc)
        {
            _userRepo = userRepo;
            _fetchScheduleConfigSvc = fetchScheduleConfigSvc;
            _scheduleSvc = scheduleSvc;
            _scheduledLessonsListSvc = scheduledLessonsListSvc;
        }

        public async Task OnGetAsync()
        {
            await FetchUserAsync();

            ScheduleConfig = await _fetchScheduleConfigSvc.FetchForAsync(_user);
            ScheduleLessons = (await _scheduleSvc.GetModelForCurrentYearAsync(_user.TeacherId!.Value))!;

            ScheduledLessonListModel = (await _scheduledLessonsListSvc.GetModelForTeacherAsync(_user.TeacherId!.Value, new FetchScheduledLessonsRequestModel
            {
                From = DateTime.Now,
                LimitTo = 6
            }))!;

            ScheduledLessonListEntries = new ScheduledLessonListEntriesJson
            {
                entries = ScheduledLessonListModel.entries,
                incomingAtTk = ScheduledLessonListModel.incomingAtTk
            };
            ScheduledLessonListConfig = new ScheduledLessonListConfigJson
            {
                minutesBeforeLessonIsSoon = ScheduledLessonListModel.minutesBeforeLessonIsSoon
            };
        }

        private async Task FetchUserAsync()
        {
            _user = await _userRepo.Manager.GetUserAsync(User);

            if (_user is null)
            {
                // TODO: redirect to error page
                throw new NotImplementedException();
            }
            if (_user.Teacher is null)
            {
                // TODO: redirect to error page
                throw new NotImplementedException();
            }
        }
    }
}
