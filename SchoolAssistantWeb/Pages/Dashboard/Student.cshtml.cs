using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SchoolAssistant.DAL.Models.AppStructure;
using SchoolAssistant.DAL.Models.StudentsParents;
using SchoolAssistant.Infrastructure.Models.MarksOverview;
using SchoolAssistant.Infrastructure.Models.ScheduleDisplay;
using SchoolAssistant.Infrastructure.Models.ScheduleShared;
using SchoolAssistant.Logic.ScheduleDisplay;

namespace SchoolAssistant.Web.Pages.Dashboard
{
    public class StudentModel : PageModel
    {
        private readonly UserManager<User> _userManager;
        private readonly IFetchSchedDisplayConfigService _fetchScheduleConfigSvc;
        private readonly IStudentScheduleService _scheduleSvc;


        private User _user = null!;
        private Student _student = null!;

        public ScheduleConfigJson ScheduleConfig { get; set; } = null!;
        public ScheduleDayLessonsJson[] ScheduleLessons { get; set; } = null!;

        public MarksOverviewModel MarksOverview { get; set; } = new MarksOverviewModel();

        public StudentModel(
            UserManager<User> userManager,
            IFetchSchedDisplayConfigService fetchScheduleConfigSvc,
            IStudentScheduleService scheduleSvc)
        {
            _userManager = userManager;
            _fetchScheduleConfigSvc = fetchScheduleConfigSvc;
            _scheduleSvc = scheduleSvc;
        }

        // TODO: verify if user
        // TODO: check authorization on every controller

        public async Task OnGetAsync()
        {
            await FetchUserAndStudentForCurrentYearAsync();

            ScheduleConfig = await _fetchScheduleConfigSvc.FetchForAsync(_user);

            ScheduleLessons = _scheduleSvc.GetModel(_student)!;


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

        private async Task FetchUserAndStudentForCurrentYearAsync()
        {
            _user = await _userManager.GetUserAsync(User);

            if (_user is null)
            {
                // TODO: redirect to error page
                throw new NotImplementedException();
            }

            var student = _user!.Student?.StudentInstances.FirstOrDefault(x => x.SchoolYear.Current);

            if (student is null)
            {
                // TODO: redirect to error page
                throw new NotImplementedException();
            }
            _student = student;
        }
    }
}
