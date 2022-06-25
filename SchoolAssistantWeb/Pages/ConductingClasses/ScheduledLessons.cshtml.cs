using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SchoolAssistant.DAL.Models.AppStructure;
using SchoolAssistant.DAL.Repositories;
using SchoolAssistant.Infrastructure.Models.ConductingClasses.ConductLesson;
using SchoolAssistant.Infrastructure.Models.ConductingClasses.ScheduledLessonsList;
using SchoolAssistant.Infrastructure.Models.Shared.Json;
using SchoolAssistant.Logic.ConductingClasses.ConductLesson;
using SchoolAssistant.Logic.ConductingClasses.ScheduledLessonsList;

namespace SchoolAssistant.Web.Pages.ConductingClasses
{
    public class ScheduledLessonsModel : PageModel
    {
        private readonly IUserRepository _userRepo;
        private readonly IFetchScheduledLessonListEntriesService _scheduledLessonsListSvc;
        private readonly IFetchScheduledLessonListConfigService _scheduledLessonsListConfigSvc;

        private readonly IStartLessonService _startLessonSvc;
        private readonly IEditLessonDetailsService _editDetailsSvc;
        private readonly IEditAttendanceService _editAttendanceSvc;
        private readonly IGiveMarkService _giveMarkSvc;

        private User _user = null!;
        public ScheduledLessonListEntriesJson ScheduledLessonListEntries { get; set; } = null!;
        public ScheduledLessonListConfigJson ScheduledLessonListConfig { get; set; } = null!;

        public ScheduledLessonsModel(
            IUserRepository userRepo,
            IFetchScheduledLessonListEntriesService scheduledLessonsListSvc,
            IFetchScheduledLessonListConfigService scheduledLessonsListConfigSvc,
            IStartLessonService startLessonSvc,
            IEditLessonDetailsService editDetailsSvc,
            IEditAttendanceService editAttendanceSvc,
            IGiveMarkService giveMarkSvc)
        {
            _userRepo = userRepo;
            _scheduledLessonsListSvc = scheduledLessonsListSvc;
            _scheduledLessonsListConfigSvc = scheduledLessonsListConfigSvc;
            _startLessonSvc = startLessonSvc;
            _editDetailsSvc = editDetailsSvc;
            _editAttendanceSvc = editAttendanceSvc;
            _giveMarkSvc = giveMarkSvc;
        }

        public async Task OnGetAsync()
        {
            await FetchUserAsync().ConfigureAwait(false);

            ScheduledLessonListEntries = (await _scheduledLessonsListSvc.GetModelForTeacherAsync(_user.TeacherId!.Value, new FetchScheduledLessonsRequestModel
            {
                From = DateTime.Now.AddDays(-1),
                LimitTo = 30
            }).ConfigureAwait(false))!;
            ScheduledLessonListConfig = await _scheduledLessonsListConfigSvc.GetDefaultConfigAsync().ConfigureAwait(false);
        }


        public async Task<JsonResult> OnGetEntriesAsync(FetchScheduledLessonsRequestJson model)
        {
            await FetchUserAsync().ConfigureAwait(false);

            var entries = await _scheduledLessonsListSvc.GetModelForTeacherAsync(_user.TeacherId!.Value, model).ConfigureAwait(false);
            return new JsonResult(entries);
        }

        public async Task<JsonResult> OnGetOpenPanelAsync(DateTime scheduledTimeUtc)
        {
            await FetchUserAsync().ConfigureAwait(false);

            var success = await _startLessonSvc.TryStartLessonAtAsync(scheduledTimeUtc.ToLocalTime(), _user.Teacher!).ConfigureAwait(false);

            return new JsonResult(new ResponseJson
            {
                message = success ? null : "Nie odnaleziono zajêæ"
            });
        }


        public async Task<JsonResult> OnPostLessonDetailsAsync([FromBody] LessonDetailsEditJson model)
        {
            var res = await _editDetailsSvc.EditAsync(model).ConfigureAwait(false);
            return new JsonResult(res);
        }

        public async Task<JsonResult> OnPostAttendanceAsync([FromBody] AttendanceEditJson model)
        {
            var res = await _editAttendanceSvc.EditAsync(model).ConfigureAwait(false);
            return new JsonResult(res);
        }

        public async Task<JsonResult> OnPostMarkAsync([FromBody] GiveMarkJson model)
        {
            var res = await _giveMarkSvc.GiveAsync(model).ConfigureAwait(false);
            return new JsonResult(res);
        }


        private async Task FetchUserAsync()
        {
            _user = await _userRepo.Manager.GetUserAsync(User).ConfigureAwait(false);

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
