// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SchoolAssistant.DAL.Models.AppStructure;

namespace SchoolAssistant.Web.Areas.Identity.Pages.Account
{
    public class LogoutModel : PageModel
    {
        private readonly SignInManager<User> _signInManager;
        private readonly ILogger<LogoutModel> _logger;

        public LogoutModel(SignInManager<User> signInManager, ILogger<LogoutModel> logger)
        {
            _signInManager = signInManager;
            _logger = logger;
        }

        public Task<IActionResult> OnGetAsync(string? returnUrl = null)
        {
            return LogOutAndRedirectAsync(returnUrl);
        }

        public Task<IActionResult> OnPostAsync(string? returnUrl = null)
        {
            return LogOutAndRedirectAsync(returnUrl);
        }

        private async Task<IActionResult> LogOutAndRedirectAsync(string? returnUrl)
        {
            await _signInManager.SignOutAsync().ConfigureAwait(false);
            if (returnUrl != null)
            {
                return LocalRedirect(returnUrl);
            }
            else
            {
                return RedirectToPage("/Index");
            }
        }
    }
}
