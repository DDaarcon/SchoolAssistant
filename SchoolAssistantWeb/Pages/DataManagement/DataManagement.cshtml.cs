using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SchoolAssistant.Web.Pages.DataManagement
{
    public class DataManagementModel : PageModel
    {
        public void OnGet()
        {
        }

        public JsonResult OnGetSubjectEntries()
        {
            return new JsonResult(new { });
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
