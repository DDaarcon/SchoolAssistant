using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SchoolAssistant.Logic.DataManagement.Subjects;

namespace SchoolAssistant.Web.Pages.DataManagement
{
    public class DataManagementModel : PageModel
    {
        private readonly ISubjectsListService _listService;

        public DataManagementModel(
            ISubjectsListService listService)
        {
            _listService = listService;
        }

        public void OnGet()
        {
        }

        public async Task<JsonResult> OnGetSubjectEntriesAsync()
        {
            var entries = await _listService.GetEntriesJsonAsync();
            return new JsonResult(entries);
        }

        public JsonResult OnGetSubjectData(long id)
        {
            return new JsonResult(new { id = id, name = "dasdasd", sampleData = "balblabala" });
        }

        public JsonResult OnPostSubjectData()
        {

            return new JsonResult(new { success = true });
        }
    }
}
