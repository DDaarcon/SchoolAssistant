namespace SchoolAssistant.Infrastructure.Models.DataManagement.Students
{
    public class StudentDetailsJson
    {
        public long? id { get; set; }
        public int numberInJournal { get; set; }
        public long? registerRecordId { get; set; }
        public StudentRegisterRecordDetailsJson? registerRecord { get; set; }
        public long? organizationalClassId { get; set; }
    }
}
