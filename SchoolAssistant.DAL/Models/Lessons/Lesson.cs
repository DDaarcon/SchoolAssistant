using SchoolAssistant.DAL.Models.Attendance;
using SchoolAssistant.DAL.Models.Shared;

namespace SchoolAssistant.DAL.Models.Lessons
{
    public class Lesson : SemesterDbEntity
    {
        public DateTime Date { get; set; }
        public string Topic { get; set; } = null!;

        public virtual ICollection<Presence> PresenceOfStudents { get; set; } = new List<Presence>();
    }
}
