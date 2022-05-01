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
                                hour = 9,
                                minutes = 0,
                                subjectName = "Matematyka",
                                lecturerName = "Tomasz Nowakowski",
                                roomName = "Sala nr 5"
                            },
                            new ()
                            {
                                hour = 9,
                                minutes = 50,
                                subjectName = "Jêzyk polski",
                                lecturerName = "Tomasz Kosakowski",
                                roomName = "Sala nr 5"
                            },
                        }
                    }
                }
            };
            return new JsonResult(model);
        }
    }
}
