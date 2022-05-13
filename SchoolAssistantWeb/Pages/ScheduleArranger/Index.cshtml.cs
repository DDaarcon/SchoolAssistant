using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SchoolAssistant.Infrastructure.Models.ScheduleArranger;
using SchoolAssistant.Infrastructure.Models.ScheduleArranger.PageModelToReact;
using SchoolAssistant.Logic.ScheduleArranger;

namespace SchoolAssistant.Web.Pages.ScheduleArranger
{
    public class ScheduleArrangerModel : PageModel
    {
        private readonly IFetchSchedArrConfigService _fetchConfigService;
        private readonly IFetchSchedArrDataService _fetchDataService;

        private readonly IFetchClassLessonsForSchedArrService _fetchLessonsSvc;
        private readonly IAddLessonBySchedArrService _addLessonSvc;

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
            IFetchOtherLessonsForSchedArrService fetchOtherLessonsService)
        {
            _fetchConfigService = fetchConfigService;
            _fetchDataService = fetchDataService;
            _fetchLessonsSvc = fetchLessonsService;
            _addLessonSvc = addLessonService;
            _fetchOtherLessonsSvc = fetchOtherLessonsService;
        }


        public async Task OnGetAsync(long? classId)
        {
            Config = await _fetchConfigService.FetchAsync();
            Config.classId = classId;
            Classes = await _fetchDataService.FetchClassesForCurrentYearAsync();
            Subjects = await _fetchDataService.FetchSubjectsAsync();
            Teachers = await _fetchDataService.FetchTeachersAsync();
            Rooms = await _fetchDataService.FetchRoomsAsync();
        }

        public async Task<JsonResult> OnGetClassLessonsAsync(long classId)
        {
            var model = await _fetchLessonsSvc.ForAsync(classId);
            return new JsonResult(model);
        }

        public async Task<JsonResult> OnPostLessonAsync([FromBody] AddLessonRequestJson model)
        {
            var result = await _addLessonSvc.AddToClassAsync(model);
            return new JsonResult(result);
        }

        public async Task<JsonResult> OnGetOtherLessonsAsync(long classId, long? teacherId, long? roomId)
        {
            var result = await _fetchOtherLessonsSvc.ForAsync(classId, teacherId, roomId);
            return new JsonResult(result);
        }
    }
}
