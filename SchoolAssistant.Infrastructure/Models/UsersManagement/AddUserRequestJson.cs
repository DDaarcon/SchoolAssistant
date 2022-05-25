using SchoolAssistant.Infrastructure.Enums.Users;

namespace SchoolAssistant.Infrastructure.Models.UsersManagement
{
    public class AddUserRequestJson
    {
        public string userName { get; set; } = null!;
        public string email { get; set; } = null!;
        public string? phoneNumber { get; set; }

        public UserTypeForManagement relatedType { get; set; }
        public long relatedId { get; set; }
    }
}
