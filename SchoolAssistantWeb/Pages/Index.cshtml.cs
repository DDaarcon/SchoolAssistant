﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SchoolAssistant.DAL.Enums;
using SchoolAssistant.DAL.Models.AppStructure;

namespace SchoolAssistant.Web.Pages
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly UserManager<User> _userManager;
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(
            UserManager<User> userManager,
            ILogger<IndexModel> logger)
        {
            _userManager = userManager;
            _logger = logger;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);

            return user.Type switch
            {
                UserType.Student => RedirectToPage("Dashboard/Student"),
                UserType.Teacher => throw new NotImplementedException(),
                UserType.Administration => throw new NotImplementedException(),
                UserType.Headmaster => throw new NotImplementedException(),
                UserType.SystemAdmin => RedirectToPage("Dashboard/Student"),
                _ => throw new NotImplementedException(),
            };
        }
    }
}