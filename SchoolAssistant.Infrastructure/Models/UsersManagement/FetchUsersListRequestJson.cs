using SchoolAssistant.Infrastructure.Enums.Users;

namespace SchoolAssistant.Infrastructure.Models.UsersManagement
{
    public class FetchUsersListRequestJson
    {
        public int? skip { get; set; }
        public int? take { get; set; }
        public UserTypeForManagement ofType { get; set; }
    }
}
