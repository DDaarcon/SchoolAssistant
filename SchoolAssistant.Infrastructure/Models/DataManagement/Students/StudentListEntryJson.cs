using SchoolAssistant.Infrastructure.Models.Shared.Json;

namespace SchoolAssistant.Infrastructure.Models.DataManagement.Students
{
    public class StudentListEntryJson : ListEntryJson
    {
        public string name { get; set; } = null!;
        public int numberInJournal { get; set; }
    }
}
