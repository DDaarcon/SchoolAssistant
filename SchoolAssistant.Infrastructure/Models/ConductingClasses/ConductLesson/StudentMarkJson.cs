namespace SchoolAssistant.Infrastructure.Models.ConductingClasses.ConductLesson
{
    public class StudentMarkJson
    {
        public long studentId { get; set; }
        public MarkJson mark { get; set; } = null!;
    }
}
