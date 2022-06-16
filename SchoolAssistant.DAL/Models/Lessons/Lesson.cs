using SchoolAssistant.DAL.Models.Attendance;
using SchoolAssistant.DAL.Models.Shared;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolAssistant.DAL.Models.Lessons
{
    public class Lesson : SchoolYearDbEntity
    {
        /// <summary> Date, when lesson should occur, according to schedule (<see cref="PeriodicLesson"/>) </summary>
        public DateTime Date { get; set; }
        /// <summary> Date, when lesson actually occured, when it is different than scheduled date </summary>
        public DateTime? ActualDate { get; set; }

        public string Topic { get; set; } = null!;

        public long FromScheduleId { get; set; }
        [ForeignKey(nameof(FromScheduleId))]
        public virtual PeriodicLesson FromSchedule { get; set; } = null!;

        public virtual ICollection<Presence> PresenceOfStudents { get; set; } = new List<Presence>();
    }
}
