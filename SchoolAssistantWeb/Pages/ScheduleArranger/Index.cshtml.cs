using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SchoolAssistant.Infrastructure.Models.ScheduleArranger;
using SchoolAssistant.Infrastructure.Models.ScheduleArranger.PageModelToReact;
using SchoolAssistant.Infrastructure.Models.Shared.Json;
using SchoolAssistant.Logic.General.PeriodicLessons;
using SchoolAssistant.Logic.ScheduleArranger;

namespace SchoolAssistant.Web.Pages.ScheduleArranger
{
    public class ScheduleArrangerModel : PageModel
    {
        private readonly IFetchSchedArrConfigService _fetchConfigService;
        private readonly IFetchSchedArrDataService _fetchDataService;

        private readonly IFetchClassLessonsForSchedArrService _fetchLessonsSvc;
        private readonly IAddLessonBySchedArrService _addLessonSvc;
        private readonly IEditLessonBySchedArrService _editLessonSvc;
        private readonly IRemovePeriodicLessonService _removeLessonSvc;

        private readonly IFetchOtherLessonsForSchedArrService _fetchOtherLessonsSvc;

        public ScheduleArrangerConfigJson Config { get; private set; } = null!;
        public ScheduleClassSelectorEntryJson[] Classes { get; private set; } = null!;
        public ScheduleSubjectEntryJson[] Subjects { get; private set; } = null!;
        public ScheduleTeacherEntryJson[] Teachers { get; private set; } = null!;
        public ScheduleRoomEntryJson[] Rooms { get; private set; } = null!;


        public ScheduleArrangerModel(
            IFetchSchedArrConfigService fetchConfigService,
            IFetchSchedArrDataService fetchDataService,
            IFetchClassLessonsForSchedArrService fetchLessonsService,
            IAddLessonBySchedArrService addLessonService,
            IFetchOtherLessonsForSchedArrService fetchOtherLessonsService,
            IEditLessonBySchedArrService editLessonSvc,
            IRemovePeriodicLessonService removeLessonSvc)
        {
            _fetchConfigService = fetchConfigService;
            _fetchDataService = fetchDataService;
            _fetchLessonsSvc = fetchLessonsService;
            _addLessonSvc = addLessonService;
            _fetchOtherLessonsSvc = fetchOtherLessonsService;
            _editLessonSvc = editLessonSvc;
            _removeLessonSvc = removeLessonSvc;
        }


        public async Task OnGetAsync(long? classId)
        {
            Config = await _fetchConfigService.FetchAsync().ConfigureAwait(false);
            Config.classId = classId;
            Classes = await _fetchDataService.FetchClassesForCurrentYearAsync().ConfigureAwait(false);
            Subjects = await _fetchDataService.FetchSubjectsAsync().ConfigureAwait(false);
            Teachers = await _fetchDataService.FetchTeachersAsync().ConfigureAwait(false);
            Rooms = await _fetchDataService.FetchRoomsAsync().ConfigureAwait(false);
        }

        public async Task<JsonResult> OnGetClassLessonsAsync(long classId)
        {
            var model = await _fetchLessonsSvc.ForAsync(classId).ConfigureAwait(false);
            return new JsonResult(model);
        }



        public async Task<JsonResult> OnPostLessonAsync([FromBody] AddLessonRequestJson model)
        {
            var result = await _addLessonSvc.AddToClassAsync(model).ConfigureAwait(false);
            return new JsonResult(result);
        }
        public async Task<JsonResult> OnPostLessonModificationAsync([FromBody] LessonEditModelJson model)
        {
            var result = await _editLessonSvc.EditAsync(model).ConfigureAwait(false);
            return new JsonResult(result);
        }

        public async Task<JsonResult> OnPostDeleteLessonAsync(long id)
        {
            var success = await _removeLessonSvc.ValidateIdAndRemoveAsync(id).ConfigureAwait(false);
            return new JsonResult(new ResponseJson
            {
                message = success ? null : "Nie znaleziono lekcji"
            });
        }


        public async Task<JsonResult> OnGetOtherLessonsAsync(long classId, long? teacherId, long? roomId)
        {
            var result = await _fetchOtherLessonsSvc.ForAsync(classId, teacherId, roomId).ConfigureAwait(false);
            return new JsonResult(result);
        }

    }
}
