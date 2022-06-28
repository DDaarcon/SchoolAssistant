using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolAssistant.DAL.Repositories;
using SchoolAssistant.Infrastructure.Models.ConductingClasses.ConductLesson;
using SchoolAssistant.Infrastructure.Models.ConductingClasses.ScheduledLessonsList;
using SchoolAssistant.Infrastructure.Models.Shared.Json;
using SchoolAssistant.Logic.ConductingClasses.ConductLesson;
using SchoolAssistant.Logic.ConductingClasses.ScheduledLessonsList;

namespace SchoolAssistant.Web.Pages.ConductingClasses
{
    [Authorize(Roles = "Teacher")]
    public class ScheduledLessonsModel : MyPageModel
    {
        private readonly IFetchScheduledLessonListEntriesService _scheduledLessonsListSvc;
        private readonly IFetchScheduledLessonListConfigService _scheduledLessonsListConfigSvc;

        private readonly IStartLessonService _startLessonSvc;
        private readonly IEditLessonDetailsService _editDetailsSvc;
        private readonly IEditAttendanceService _editAttendanceSvc;
        private readonly IGiveMarkService _giveMarkSvc;
        private readonly IGiveGroupMarkService _giveGroupMarkSvc;

        public ScheduledLessonListEntriesJson ScheduledLessonListEntries { get; set; } = null!;
        public ScheduledLessonListConfigJson ScheduledLessonListConfig { get; set; } = null!;

        public ScheduledLessonsModel(
            IUserRepository userRepo,
            IFetchScheduledLessonListEntriesService scheduledLessonsListSvc,
            IFetchScheduledLessonListConfigService scheduledLessonsListConfigSvc,
            IStartLessonService startLessonSvc,
            IEditLessonDetailsService editDetailsSvc,
            IEditAttendanceService editAttendanceSvc,
            IGiveMarkService giveMarkSvc,
            IGiveGroupMarkService giveGroupMarkSvc) : base(userRepo)
        {
            _scheduledLessonsListSvc = scheduledLessonsListSvc;
            _scheduledLessonsListConfigSvc = scheduledLessonsListConfigSvc;
            _startLessonSvc = startLessonSvc;
            _editDetailsSvc = editDetailsSvc;
            _editAttendanceSvc = editAttendanceSvc;
            _giveMarkSvc = giveMarkSvc;
            _giveGroupMarkSvc = giveGroupMarkSvc;
        }

        public async Task OnGetAsync()
        {
            await FetchUserAsync().ConfigureAwait(false);

            // TODO: currently works only for teacher
            ScheduledLessonListEntries = (await _scheduledLessonsListSvc.GetModelForTeacherAsync(_User!.TeacherId!.Value, new FetchScheduledLessonsRequestModel
            {
                From = DateTime.Now.AddDays(-1),
                LimitTo = 30
            }).ConfigureAwait(false))!;
            ScheduledLessonListConfig = await _scheduledLessonsListConfigSvc.GetDefaultConfigAsync().ConfigureAwait(false);
        }



        public async Task<JsonResult> OnGetEntriesAsync(FetchScheduledLessonsRequestJson model)
        {
            await FetchUserAsync().ConfigureAwait(false);

            var entries = await _scheduledLessonsListSvc.GetModelForTeacherAsync(_User!.TeacherId!.Value, model).ConfigureAwait(false);
            return new JsonResult(entries);
        }

        public async Task<JsonResult> OnGetOpenPanelAsync(DateTime scheduledTimeUtc)
        {
            await FetchUserAsync().ConfigureAwait(false);

            var success = await _startLessonSvc.TryStartLessonAtAsync(scheduledTimeUtc.ToLocalTime(), _User!.Teacher!).ConfigureAwait(false);

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

        public async Task<JsonResult> OnPostGroupMarkAsync([FromBody] GiveGroupMarkJson model)
        {
            var res = await _giveGroupMarkSvc.GiveAsync(model).ConfigureAwait(false);
            return new JsonResult(res);
        }
    }
}
