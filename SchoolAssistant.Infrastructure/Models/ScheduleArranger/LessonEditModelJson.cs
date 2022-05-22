using SchoolAssistant.Infrastructure.Models.ScheduleShared;

namespace SchoolAssistant.Infrastructure.Models.ScheduleArranger
{
    public class LessonEditModelJson : IValidateLessonJson
    {
        public long? id { get; set; }

        public DayOfWeek day { get; set; }

        public TimeJson time { get; set; } = null!;

        public int? customDuration { get; set; }

        public long subjectId { get; set; }

        public long lecturerId { get; set; }

        public long roomId { get; set; }

        public long classId { get; set; }
    }
}
