using SchoolAssistant.Infrastructure.Models.Shared.Json;

namespace SchoolAssistant.Infrastructure.Models.DataManagement.Classes
{
    public class ClassListEntryJson : ListEntryJson
    {
        public string name { get; set; } = null!;
        public string? specialization { get; set; }
        public int amountOfStudents { get; set; }
    }
}
