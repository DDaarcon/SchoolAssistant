using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SchoolAssistant.Infrastructure.Models.DataManagement.Subjects;
using SchoolAssistant.Logic.DataManagement.Staff;
using SchoolAssistant.Logic.DataManagement.Subjects;

namespace SchoolAssistant.Web.Pages.DataManagement
{
    public class DataManagementModel : PageModel
    {
        private readonly ISubjectsDataManagementService _subjectsService;
        private readonly IStaffDataManagementService _staffService;

        public DataManagementModel(
            ISubjectsDataManagementService subjectsService,
            IStaffDataManagementService staffService)
        {
            _subjectsService = subjectsService;
            _staffService = staffService;
        }

        public void OnGet()
        {
        }

        public async Task<JsonResult> OnGetSubjectEntriesAsync()
        {
            var entries = await _subjectsService.GetEntriesJsonAsync();
            return new JsonResult(entries);
        }

        public async Task<JsonResult> OnGetSubjectDetailsAsync(long id)
        {
            var details = await _subjectsService.GetDetailsJsonAsync(id);
            return new JsonResult(details);
        }

        public async Task<JsonResult> OnPostSubjectDataAsync([FromBody] SubjectDetailsJson model)
        {
            var result = await _subjectsService.CreateOrUpdateAsync(model);
            return new JsonResult(result);
        }


        public async Task<JsonResult> OnGetStaffPersonsEntriesAsync()
        {
            var groups = await _staffService.GetGroupsOfEntriesJsonAsync();
            return new JsonResult(groups);
        }

        public async Task<JsonResult> OnGetStaffPersonDetailsAsync(long id)
        {
            return new JsonResult(null);
        }
    }
}
