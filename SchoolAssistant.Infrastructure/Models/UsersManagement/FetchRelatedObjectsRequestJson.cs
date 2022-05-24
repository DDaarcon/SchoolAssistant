using SchoolAssistant.Infrastructure.Enums.Users;

namespace SchoolAssistant.Infrastructure.Models.UsersManagement
{
    public class FetchRelatedObjectsRequestJson
    {
        //public int? skip { get; set; }
        //public int? take { get; set; }
        public UserTypeForManagement ofType { get; set; }
    }
}
