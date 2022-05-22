using Microsoft.AspNetCore.Mvc.RazorPages;
using SchoolAssistant.Infrastructure.Models.UsersManagement;

namespace SchoolAssistant.Web.Pages.UsersManagement
{
    public class UsersManagementModel : PageModel
    {

        public IEnumerable<UserListEntryModel> Users { get; private set; } = null!;

        public void OnGet()
        {
            Users = new UserListEntryModel[]
            {
                new UserListEntryModel
                {
                    FirstName = "Guy",
                    LastName = "Nice",
                    Email = "nice6969@nice.com",
                    UserName = "niceguy",
                    Type = Infrastructure.Enums.Users.UserTypeForManagement.Student,
                    TypeName = "Uczeñ"
                }
            };
        }
    }
}
