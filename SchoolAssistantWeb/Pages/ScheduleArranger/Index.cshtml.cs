using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SchoolAssistant.Infrastructure.Models.ScheduleArranger;
using SchoolAssistant.Logic.ScheduleArranger;

namespace SchoolAssistant.Web.Pages.ScheduleArranger
{
    public class ScheduleArrangerModel : PageModel
    {
        private readonly IFetchScheduleArrangerConfigService _fetchConfigService;
        private readonly IFetchLessonsForScheduleArrangerService _fetchLessonsSvc;
        private readonly IAddLessonByScheduleArrangerService _addLessonSvc;

        public ScheduleArrangerConfigJson Config { get; set; } = null!;


        public ScheduleArrangerModel(
            IFetchScheduleArrangerConfigService fetchConfigService,
            IFetchLessonsForScheduleArrangerService fetchLessonsService,
            IAddLessonByScheduleArrangerService addLessonService)
        {
            _fetchConfigService = fetchConfigService;
            _fetchLessonsSvc = fetchLessonsService;
            _addLessonSvc = addLessonService;
        }


        public async Task OnGetAsync()
        {
            Config = await _fetchConfigService.FetchAsync();
        }

        public async Task<JsonResult> OnGetClassLessonsAsync(long classId)
        {
            var model = await _fetchLessonsSvc.ForClassAsync(classId);
            return new JsonResult(model);
        }

        public async Task<JsonResult> OnPostLessonAsync([FromBody] AddLessonRequestJson model)
        {
            var result = await _addLessonSvc.AddToClass(model);
            return new JsonResult(model);
        }



        private ScheduleClassLessonsJson _lessonsMockupModel => new ScheduleClassLessonsJson
        {
            data = new ScheduleDayLessonsJson[]
                {
                    new ()
                    {
                        dayIndicator = DayOfWeek.Monday,
                        lessons = new PeriodicLessonTimetableEntryJson[]
                        {
                            new ()
                            {
                                time = new TimeJson
                                {
                                    hour = 9,
                                    minutes = 0
                                },
                                subject = new IdNameJson() { id = 1, name = "Matematyka" },
                                lecturer = new IdNameJson() { id = 1, name = "T. Nowakowski" },
                                room = new IdNameJson() { id = 1, name = "Sala nr 5" }
                            },
                            new ()
                            {
                                time = new TimeJson
                                {
                                    hour = 9,
                                    minutes = 50
                                },
                                subject = new IdNameJson() { id = 2, name = "Jêzyk polski" },
                                lecturer = new IdNameJson() { id = 2, name = "T. Kosakowski" },
                                room = new IdNameJson() { id = 1, name = "Sala nr 5" }
                            },
                        }
                    }
                }
        };
    }
}
