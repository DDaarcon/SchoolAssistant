namespace SchoolAssistant.Infrastructure.Models.DataManagement.Staff
{
    public class StaffListGroupJson
    {
        public string id { get; set; } = null!;
        public string name { get; set; } = null!;
        public StaffListEntryJson[] entries { get; set; } = null!;
    }
}
