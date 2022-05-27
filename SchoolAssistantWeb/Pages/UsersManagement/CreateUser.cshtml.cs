using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SchoolAssistant.Infrastructure.Models.UsersManagement;
using SchoolAssistant.Logic.General.Other;
using SchoolAssistant.Logic.UsersManagement;

namespace SchoolAssistant.Web.Pages.UsersManagement
{
    public class CreateUserModel : PageModel
    {
        private readonly IFetchUserListEntriesService _fetchUserListEntriesSvc;
        private readonly IFetchUserRelatedObjectsService _fetchRelatedObjectsSvc;
        private readonly IAddUserService _addUserSvc;
        private readonly IPasswordDeformationService _deformationSvc;

        public CreateUserModel(
            IFetchUserListEntriesService fetchUserListEntriesSvc,
            IFetchUserRelatedObjectsService fetchRelatedObjectsSvc,
            IAddUserService addUserSvc,
            IPasswordDeformationService deformationSvc)
        {
            _fetchUserListEntriesSvc = fetchUserListEntriesSvc;
            _fetchRelatedObjectsSvc = fetchRelatedObjectsSvc;
            _addUserSvc = addUserSvc;
            _deformationSvc = deformationSvc;
        }

        public void OnGet()
        {
        }

        public async Task<JsonResult> OnGetRelatedObjectsAsync(FetchRelatedObjectsRequestJson model)
        {
            var objects = await _fetchRelatedObjectsSvc.GetObjectsAsync(model);
            return new JsonResult(objects);
        }

        public async Task<JsonResult> OnPostAddUserAsync([FromBody] AddUserRequestJson model)
        {
            var res = await _addUserSvc.AddAsync(model);
            return new JsonResult(res);
        }

        public JsonResult OnGetUnscramblePassword(string deformed)
        {
            return new JsonResult(new
            {
                readablePassword = _deformationSvc.GetReadable(deformed)
            });
        }
    }
}
