namespace SchoolAssistant.Infrastructure.Models.UsersManagement
{
    public class SimpleRelatedObjectJson
    {
        public long id { get; set; }
        public string firstName { get; set; } = null!;
        public string lastName { get; set; } = null!;
        public string? email { get; set; }
    }
}
