namespace SchoolAssistant.Infrastructure.Models.DataManagement.Classes
{
    public class ClassListEntryJson
    {
        public long id { get; set; }
        public string name { get; set; } = null!;
        public string? specialization { get; set; }
        public int amountOfStudents { get; set; }
    }
}
