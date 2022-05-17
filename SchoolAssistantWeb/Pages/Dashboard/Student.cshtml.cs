using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SchoolAssistant.DAL.Models.AppStructure;
using SchoolAssistant.Infrastructure.Enums.Schedule;
using SchoolAssistant.Infrastructure.Models.MarksOverview;
using SchoolAssistant.Infrastructure.Models.Schedule;
using SchoolAssistant.Logic.Help;

namespace SchoolAssistant.Web.Pages.Dashboard
{
    public class StudentModel : PageModel
    {
        private readonly UserManager<User> _userManager;

        private User _user = null!;

        public ScheduleConfigJson ScheduleConfig { get; set; } = null!;
        public ScheduleEventJson[] ScheduleEvents { get; set; } = null!;
        public MarksOverviewModel MarksOverview { get; set; } = new MarksOverviewModel();

        public StudentModel(
            UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task OnGetAsync()
        {
            _user = await _userManager.GetUserAsync(User);

            ScheduleConfig = new()
            {
                locale = "pl",
                hiddenDays = new DayOfWeek[] { DayOfWeek.Sunday, DayOfWeek.Saturday },
                @for = ScheduleViewerType.Student,
                startHour = 8,
                endHour = 20
            };

            ScheduleEvents = new ScheduleEventJson[]
            {
                new ()
                {
                    id = 1.ToString(),
                    title = "Jêzyk polski",
                    start = new DateTime(2022, 5, 17, 10, 50, 0).GetMillisecondsJS(),
                    end = new DateTime(2022, 5, 17, 11, 35, 0).GetMillisecondsJS(),
                    @class = "1d",
                    lecturer = "T. Milecki",
                    room = "Sala nr 5",
                    subject = "Jêzyk polski"
                },
                new ()
                {
                    id = 2.ToString(),
                    title = "Jêzyk angielski",
                    start = new DateTime(2022, 5, 18, 12, 50, 0).GetMillisecondsJS(),
                    end = new DateTime(2022, 5, 18, 13, 35, 0).GetMillisecondsJS(),
                    @class = "1d",
                    lecturer = "T. Wielicki",
                    room = "Sala nr 4",
                    subject = "Jêzyk angielski"
                },
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
                    Mark = "-5", Subject = "Some long subject name to check long names", Issuer = "Tomasz Kowalczykowiañskowski"
                }
            };
        }
    }
}
