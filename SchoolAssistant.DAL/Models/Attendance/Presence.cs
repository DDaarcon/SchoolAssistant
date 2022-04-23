using SchoolAssistant.DAL.Enums;
using SchoolAssistant.DAL.Models.Lessons;
using SchoolAssistant.DAL.Models.Shared;
using SchoolAssistant.DAL.Models.StudentsParents;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolAssistant.DAL.Models.Attendance
{
    public class Presence : SchoolYearDbEntity
    {
        public long LessonId { get; set; }
        [ForeignKey(nameof(LessonId))]
        public virtual Lesson Lesson { get; set; } = null!;

        public long StudentId { get; set; }
        [ForeignKey(nameof(StudentId))]
        public virtual Student Student { get; set; } = null!;

        public PresenceStatus Status { get; set; }
    }
}
