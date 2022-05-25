using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SchoolAssistant.Infrastructure.Models.UsersManagement;
using SchoolAssistant.Logic.UsersManagement;

namespace SchoolAssistant.Web.Pages.UsersManagement
{
    public class CreateUserModel : PageModel
    {
        private readonly IFetchUserListEntriesService _fetchUserListEntriesSvc;
        private readonly IFetchUserRelatedObjectsService _fetchRelatedObjectsSvc;

        public CreateUserModel(
            IFetchUserListEntriesService fetchUserListEntriesSvc,
            IFetchUserRelatedObjectsService fetchRelatedObjectsSvc)
        {
            _fetchUserListEntriesSvc = fetchUserListEntriesSvc;
            _fetchRelatedObjectsSvc = fetchRelatedObjectsSvc;
        }

        public void OnGet()
        {
        }

        public async Task<JsonResult> OnGetRelatedObjectsAsync(FetchRelatedObjectsRequestJson model)
        {
            var objects = await _fetchRelatedObjectsSvc.GetObjectsAsync(model);
            return new JsonResult(objects);
        }

        //public async Task<JsonResult> OnPostAddUserAsync()
    }
}
