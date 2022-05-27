using SchoolAssistant.Infrastructure.Models.Shared.Json;

namespace SchoolAssistant.Infrastructure.Models.UsersManagement
{
    public class AddUserResponseJson : ResponseJson
    {
        public string passwordDeformed { get; set; } = null!;
    }
}
