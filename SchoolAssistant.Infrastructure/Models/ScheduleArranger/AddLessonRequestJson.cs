namespace SchoolAssistant.Infrastructure.Models.ScheduleArranger
{
    public class AddLessonRequestJson : IValidateLessonJson
    {
        public long classId { get; set; }
        public DayOfWeek day { get; set; }
        public TimeJson time { get; set; } = null!;
        public int? customDuration { get; set; }
        public long subjectId { get; set; }
        public long lecturerId { get; set; }
        public long roomId { get; set; }

        public long? id => null;
    }
}
