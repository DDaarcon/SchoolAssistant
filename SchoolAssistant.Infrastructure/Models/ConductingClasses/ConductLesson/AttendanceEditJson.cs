namespace SchoolAssistant.Infrastructure.Models.ConductingClasses.ConductLesson
{
    public class AttendanceEditJson
    {
        public long id { get; set; }
        public StudentPresenceEditJson[] students { get; set; } = null!;
    }
}
