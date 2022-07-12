using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolAssistant.DAL.Enums;
using SchoolAssistant.DAL.Repositories;

namespace SchoolAssistant.Web.Pages
{
    [Authorize]
    public class IndexModel : MyPageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(
            IUserRepository userRepo,
            ILogger<IndexModel> logger) : base(userRepo)
        {
            _logger = logger;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            await FetchUserAsync().ConfigureAwait(false);

            SetVersionInViewData();

            return _User?.Type switch
            {
                UserType.Student => RedirectToPage("Dashboard/Student"),
                UserType.Teacher => RedirectToPage("Dashboard/Teacher"),
                UserType.Administration => throw new NotImplementedException(),
                UserType.Headmaster => throw new NotImplementedException(),
                UserType.SystemAdmin => Page(),
                _ => RedirectToPage("/Account/Logout", new { area = "Identity" }),
            };
        }
    }
}