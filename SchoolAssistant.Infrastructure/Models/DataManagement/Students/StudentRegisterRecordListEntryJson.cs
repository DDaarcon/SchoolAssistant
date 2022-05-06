using SchoolAssistant.Infrastructure.Models.Shared.Json;

namespace SchoolAssistant.Infrastructure.Models.DataManagement.Students
{
    public class StudentRegisterRecordListEntryJson : ListEntryJson
    {
        public string name { get; set; } = null!;
        public string? className { get; set; }
    }
}
