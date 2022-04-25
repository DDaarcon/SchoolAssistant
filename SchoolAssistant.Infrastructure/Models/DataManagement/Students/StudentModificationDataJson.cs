namespace SchoolAssistant.Infrastructure.Models.DataManagement.Students
{
    public class StudentModificationDataJson
    {
        public StudentDetailsJson data { get; set; } = null!;
        public StudentRegisterRecordListEntryJson[] registerRecords { get; set; } = null!;
    }
}
