using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SchoolAssistant.DAL.Models.AppStructure;
using SchoolAssistant.Infrastructure.Models.Schedule;

namespace SchoolAssistant.Web.Pages.Dashboard
{
    public class StudentModel : PageModel
    {
        private readonly UserManager<User> _userManager;

        private User _user = null!;

        public ScheduleModel Schedule { get; set; } = new ScheduleModel();

        public StudentModel(
            UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task OnGetAsync()
        {
            _user = await _userManager.GetUserAsync(User);

            var page = Page();
        }
    }
}
