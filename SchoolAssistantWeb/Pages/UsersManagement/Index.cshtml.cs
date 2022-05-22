using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SchoolAssistant.Infrastructure.Models.UsersManagement;
using SchoolAssistant.Logic.UsersManagement;

namespace SchoolAssistant.Web.Pages.UsersManagement
{
    public class UsersManagementModel : PageModel
    {
        private readonly IFetchUserListEntriesService _fetchEntriesSvc;

        public IEnumerable<UserListEntryJson> Users { get; private set; } = null!;

        public UsersManagementModel(
            IFetchUserListEntriesService fetchEntriesSvc)
        {
            _fetchEntriesSvc = fetchEntriesSvc;
        }


        public async Task OnGetAsync()
        {
            Users = await _fetchEntriesSvc.FetchAsync(new FetchUsersListRequestJson
            {
                take = 40,
                ofType = Infrastructure.Enums.Users.UserTypeForManagement.Teacher
            });
        }

        public async Task<JsonResult> OnGetUserListEntriesAsync(FetchUsersListRequestJson model)
        {
            var users = await _fetchEntriesSvc.FetchAsync(model);
            return new JsonResult(users);
        }
    }
}
