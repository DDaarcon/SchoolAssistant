namespace SchoolAssistant.Infrastructure.Models.DataManagement.Staff
{
    public class StaffPersonDetailsJson
    {
        public long? id { get; set; }
        public string? groupId { get; set; }
        public string firstName { get; set; } = null!;
        public string? secondName { get; set; }
        public string lastName { get; set; } = null!;

        public long[]? mainSubjectsIds { get; set; }
        public long[]? additionalSubjectsIds { get; set; }
    }
}
