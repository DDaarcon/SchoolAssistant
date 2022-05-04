using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SchoolAssistant.Infrastructure.Models.ScheduleArranger;
using SchoolAssistant.Infrastructure.Models.ScheduleArranger.PageModelToReact;
using SchoolAssistant.Logic.ScheduleArranger;

namespace SchoolAssistant.Web.Pages.ScheduleArranger
{
    public class ScheduleArrangerModel : PageModel
    {
        private readonly IFetchScheduleArrangerConfigService _fetchConfigService;
        private readonly IFetchScheduleArrangerDataService _fetchDataService;

        private readonly IFetchLessonsForScheduleArrangerService _fetchLessonsSvc;
        private readonly IAddLessonByScheduleArrangerService _addLessonSvc;

        public ScheduleArrangerConfigJson Config { get; private set; } = null!;
        public ScheduleClassSelectorEntryJson[] Classes { get; private set; } = null!;
        public ScheduleSubjectEntryJson[] Subjects { get; private set; } = null!;
        public ScheduleTeacherEntryJson[] Teachers { get; private set; } = null!;
        public ScheduleRoomEntryJson[] Rooms { get; private set; } = null!;


        public ScheduleArrangerModel(
            IFetchScheduleArrangerConfigService fetchConfigService,
            IFetchScheduleArrangerDataService fetchDataService,
            IFetchLessonsForScheduleArrangerService fetchLessonsService,
            IAddLessonByScheduleArrangerService addLessonService)
        {
            _fetchConfigService = fetchConfigService;
            _fetchDataService = fetchDataService;
            _fetchLessonsSvc = fetchLessonsService;
            _addLessonSvc = addLessonService;
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
            var model = await _fetchLessonsSvc.ForClassAsync(classId);
            return new JsonResult(model);
        }

        public async Task<JsonResult> OnPostLessonAsync([FromBody] AddLessonRequestJson model)
        {
            var result = await _addLessonSvc.AddToClassAsync(model);
            return new JsonResult(result);
        }
    }
}
