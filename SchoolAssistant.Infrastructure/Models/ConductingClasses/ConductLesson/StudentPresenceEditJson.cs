using SchoolAssistant.Infrastructure.Enums.Attendance;

namespace SchoolAssistant.Infrastructure.Models.ConductingClasses.ConductLesson
{
    public class StudentPresenceEditJson
    {
        public long id { get; set; }
        public PresenceStatus? presence { get; set; }
    }
}
