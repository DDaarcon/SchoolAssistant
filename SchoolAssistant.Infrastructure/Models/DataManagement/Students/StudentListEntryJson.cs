namespace SchoolAssistant.Infrastructure.Models.DataManagement.Students
{
    public class StudentListEntryJson
    {
        public long id { get; set; }
        public string name { get; set; } = null!;
        public int numberInJournal { get; set; }
    }
}
