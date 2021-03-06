using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SchoolAssistant.DAL.Enums;
using SchoolAssistant.DAL.Models.AppStructure;
using SchoolAssistant.DAL.Repositories;
using SchoolAssistant.Logic;
using System.Reflection;

namespace SchoolAssistant.Web
{
    public abstract class MyPageModel : PageModel
    {
        protected readonly IUserRepository _userRepo;

        protected MyPageModel(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }

        protected RedirectToPageResult RedirectToStart => RedirectToPage("/Index");


        protected User? _User { get; set; }

        protected async Task FetchUserAsync()
        {
            _User = (await _userRepo.GetCurrentUserCachedAsync().ConfigureAwait(false))!;
        }

        protected async Task<bool> FetchAndValidateIfUserOfTypeAsync(UserType type)
        {
            await FetchUserAsync().ConfigureAwait(false);

            return _User.IsOfType(type);
        }

        protected void SetVersionInViewData()
        {
            string? ver = Assembly.GetEntryAssembly()?.GetName().Version?.ToString();
            ViewData["ProjectVersion"] = ver;
        }
    }
}
