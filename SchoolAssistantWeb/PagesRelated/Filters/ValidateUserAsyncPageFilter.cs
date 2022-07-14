using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SchoolAssistant.DAL.Models.AppStructure;

namespace SchoolAssistant.Web.PagesRelated.Filters
{
    public class ValidateUserAsyncPageFilter : IAsyncPageFilter
    {
        private readonly SignInManager<User> _signInManager;

        public ValidateUserAsyncPageFilter(
            SignInManager<User> signInManager)
        {
            _signInManager = signInManager;
        }

        private readonly string LOGOUT_PAGE_URL = "/Identity/Account/Logout".ToUpper();
        private readonly string LOGIN_PAGE_URL = "/Identity/Account/Login".ToUpper();

        public async Task OnPageHandlerExecutionAsync(PageHandlerExecutingContext context, PageHandlerExecutionDelegate next)
        {
            var user = await _signInManager.ValidateSecurityStampAsync(context.HttpContext.User).ConfigureAwait(false);

            if (user is not null || IsApplowedPage(context))
                await next.Invoke().ConfigureAwait(false);
            else
                context.Result = new RedirectToPageResult("/Account/Logout", new { area = "Identity" });
        }

        public Task OnPageHandlerSelectionAsync(PageHandlerSelectedContext context) => Task.CompletedTask;


        private bool IsApplowedPage(PageHandlerExecutingContext context)
        {
            var url = context.HttpContext.Request.Path.ToString().ToUpper();
            return url == LOGOUT_PAGE_URL
                || url == LOGIN_PAGE_URL;
        }
    }
}
