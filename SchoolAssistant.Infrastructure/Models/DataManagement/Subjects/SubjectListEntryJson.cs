using SchoolAssistant.Infrastructure.Models.Shared.Json;

namespace SchoolAssistant.Infrastructure.Models.DataManagement.Subjects
{
    public class SubjectListEntryJson : ListEntryJson
    {
        public string name { get; set; } = null!;
    }
}
