using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SchoolAssistant.DAL.Models.AppStructure;
using SchoolAssistant.Infrastructure.Models.MarksOverview;
using SchoolAssistant.Infrastructure.Models.Schedule;

namespace SchoolAssistant.Web.Pages.Dashboard
{
    public class StudentModel : PageModel
    {
        private readonly UserManager<User> _userManager;

        private User _user = null!;

        public ScheduleModel Schedule { get; set; } = new ScheduleModel();
        public MarksOverviewModel MarksOverview { get; set; } = new MarksOverviewModel();

        public StudentModel(
            UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task OnGetAsync()
        {
            _user = await _userManager.GetUserAsync(User);

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

            var page = Page();
        }
    }
}
