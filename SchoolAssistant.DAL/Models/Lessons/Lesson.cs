using SchoolAssistant.DAL.Models.Attendance;
using SchoolAssistant.DAL.Models.Shared;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolAssistant.DAL.Models.Lessons
{
    public class Lesson : SchoolYearDbEntity
    {
        public DateTime Date { get; set; }
        public string Topic { get; set; } = null!;

        public long FromScheduleId { get; set; }
        [ForeignKey(nameof(FromScheduleId))]
        public virtual PeriodicLesson FromSchedule { get; set; } = null!;

        public virtual ICollection<Presence> PresenceOfStudents { get; set; } = new List<Presence>();
    }
}
