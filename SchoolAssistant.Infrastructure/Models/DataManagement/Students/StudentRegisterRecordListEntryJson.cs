namespace SchoolAssistant.Infrastructure.Models.DataManagement.Students
{
    public class StudentRegisterRecordListEntryJson
    {
        public long id { get; set; }
        public string name { get; set; } = null!;
        public string? className { get; set; }
    }
}
