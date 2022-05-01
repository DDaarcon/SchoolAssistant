using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SchoolAssistant.Infrastructure.Models.ScheduleArranger;

namespace SchoolAssistant.Web.Pages.ScheduleArranger
{
    public class ScheduleArrangerModel : PageModel
    {
        public ScheduleArrangerConfigJson Config { get; set; } = null!;

        public void OnGet()
        {
            Config = new ScheduleArrangerConfigJson
            {
                defaultLessonDuration = 45,
                cellDuration = 5,
                cellHeight = 5,
                startHour = 6,
                endHour = 20
            };
        }

        public async Task<JsonResult> OnGetClassLessonsAsync(long classId)
        {
            var model = new ScheduleClassLessonsJson
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
            return new JsonResult(model);
        }
    }
}
