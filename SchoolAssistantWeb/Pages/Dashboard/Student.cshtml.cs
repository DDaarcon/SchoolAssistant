using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SchoolAssistant.DAL.Models.AppStructure;
using SchoolAssistant.Infrastructure.Models.MarksOverview;
using SchoolAssistant.Infrastructure.Models.ScheduleDisplay;
using SchoolAssistant.Infrastructure.Models.ScheduleShared;
using SchoolAssistant.Logic.Help;
using SchoolAssistant.Logic.ScheduleDisplay;

namespace SchoolAssistant.Web.Pages.Dashboard
{
    public class StudentModel : PageModel
    {
        private readonly UserManager<User> _userManager;
        private readonly IFetchSchedDisplayConfigService _fetchScheduleConfigSvc;

        private User _user = null!;

        public ScheduleConfigJson ScheduleConfig { get; set; } = null!;
        public ScheduleDayLessonsJson[] ScheduleEvents { get; set; } = null!;

        public MarksOverviewModel MarksOverview { get; set; } = new MarksOverviewModel();

        public StudentModel(
            UserManager<User> userManager,
            IFetchSchedDisplayConfigService fetchScheduleConfigSvc)
        {
            _userManager = userManager;
            _fetchScheduleConfigSvc = fetchScheduleConfigSvc;
        }

        public async Task OnGetAsync()
        {
            _user = await _userManager.GetUserAsync(User);

            ScheduleConfig = await _fetchScheduleConfigSvc.FetchForAsync(_user);

            ScheduleEvents = new ScheduleDayLessonsJson[]
            {
                new ScheduleDayLessonsJson()
                {
                    dayIndicator = DayOfWeek.Monday,
                    lessons = new LessonTimetableEntryJson[]
                    {
                        new ()
                        {
                            id = 1,
                            time = new TimeJson{ hour = 12, minutes = 30 },
                            lecturer = new IdNameJson{ name = "T. Milecki", id = 4 },
                            room = new IdNameJson{ name = "Sala nr 5", id = 4 },
                            subject = new IdNameJson{ name = "J�zyk polski", id = 4 }
                        }
                    }
                }
                //new ()
                //{
                    
                    //id = 1.ToString(),
                    //title = "J�zyk polski",
                    //start = new DateTime(2022, 5, 17, 10, 50, 0).GetMillisecondsJsUTC(),
                    //end = new DateTime(2022, 5, 17, 11, 35, 0).GetMillisecondsJsUTC(),
                    //@class = "1d",
                    //lecturer = "T. Milecki",
                    //room = "Sala nr 5",
                    //subject = "J�zyk polski"
                //},
                //new ()
                //{
                //    id = 2.ToString(),
                //    title = "J�zyk angielski",
                //    start = new DateTime(2022, 5, 18, 12, 50, 0).GetMillisecondsJsUTC(),
                //    end = new DateTime(2022, 5, 18, 13, 35, 0).GetMillisecondsJsUTC(),
                //    @class = "1d",
                //    lecturer = "T. Wielicki",
                //    room = "Sala nr 4",
                //    subject = "J�zyk angielski"
                //},
            };

            MarksOverview.Marks = new List<MarkForOverviewModel>()
            {
                new()
                {
                    Mark = "-5", Subject = "English", Issuer = "Tomasz Kowalczyk"
                },
                new()
                {
                    Mark = "1", Subject = "Polish", Issuer = "Tomasz Kowalczyk"
                },
                new()
                {
                    Mark = "+3", Subject = "Math", Issuer = "Tomasz Kowalczyk"
                },
                new()
                {
                    Mark = "-5", Subject = "Some long subject name to check long names", Issuer = "Tomasz Kowalczykowia�skowski"
                }
            };
        }
    }
}
