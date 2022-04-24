namespace SchoolAssistant.Infrastructure.Models.DataManagement.Students
{
    public class StudentRegisterRecordDetailsJson
    {
        public long? id { get; set; }

        public string firstName { get; set; } = null!;
        public string? secondName { get; set; }
        public string lastName { get; set; } = null!;

        public string dateOfBirth { get; set; } = null!;
        public string placeOfBirth { get; set; } = null!;

        public string personalId { get; set; } = null!;
        public string address { get; set; } = null!;
    }
}
