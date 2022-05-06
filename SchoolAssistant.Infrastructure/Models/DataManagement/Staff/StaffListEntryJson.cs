using SchoolAssistant.Infrastructure.Models.Shared.Json;

namespace SchoolAssistant.Infrastructure.Models.DataManagement.Staff
{
    public class StaffListEntryJson : ListEntryJson
    {
        public string name { get; set; } = null!;
        public string specialization { get; set; } = null!;
    }
}
