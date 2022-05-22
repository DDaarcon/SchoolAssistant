using SchoolAssistant.Infrastructure.Enums.Users;

namespace SchoolAssistant.Infrastructure.Models.UsersManagement
{
    public class FetchUsersListModel
    {
        public int? Skip { get; set; }
        public int? Take { get; set; }
        public UserTypeForManagement OfType { get; set; }
    }
}
