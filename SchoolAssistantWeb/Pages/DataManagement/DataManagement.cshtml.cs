using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SchoolAssistant.Infrastructure.Models.DataManagement.Subjects;
using SchoolAssistant.Logic.DataManagement.Subjects;

namespace SchoolAssistant.Web.Pages.DataManagement
{
    public class DataManagementModel : PageModel
    {
        private readonly ISubjectsDataManagementService _subjectsService;

        public DataManagementModel(
            ISubjectsDataManagementService subjectsService)
        {
            _subjectsService = subjectsService;
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

        public JsonResult OnPostAA()
        {
            return new JsonResult(new { success = true });
        }

        public async Task<JsonResult> OnPostSubjectDataAsync(SubjectDetailsJsonModel model)
        {

            return new JsonResult(new { success = true });
        }
    }
}
