namespace SchoolAssistant.Infrastructure.Models.ConductingClasses.ConductLesson
{
    public class GiveMarkJson
    {
        public long lessonId { get; set; }
        public MarkJson mark { get; set; } = null!;
        public string description { get; set; } = null!;
        public int? weight { get; set; }
        public long studentId { get; set; }
    }
}
