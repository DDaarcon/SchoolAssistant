using SchoolAssistant.Infrastructure.Enums.Users;

namespace SchoolAssistant.Infrastructure.Models.UsersManagement
{
    public class UserListEntryModel
    {
        public long Id { get; set; }
        public string UserName { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public UserTypeForManagement Type { get; set; }
        public string TypeName { get; set; } = null!;
    }
}
