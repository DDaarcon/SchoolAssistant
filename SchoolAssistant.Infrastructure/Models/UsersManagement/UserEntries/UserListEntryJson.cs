namespace SchoolAssistant.Infrastructure.Models.UsersManagement
{
    public class UserListEntryJson
    {
        public long id { get; set; }
        public string userName { get; set; } = null!;
        public string firstName { get; set; } = null!;
        public string lastName { get; set; } = null!;
        public string email { get; set; } = null!;
    }
}
