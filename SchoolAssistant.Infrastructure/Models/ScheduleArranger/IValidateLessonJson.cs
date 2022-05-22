using SchoolAssistant.Infrastructure.Models.ScheduleShared;

namespace SchoolAssistant.Infrastructure.Models.ScheduleArranger
{
    public interface IValidateLessonJson
    {
        long? id { get; }
        DayOfWeek day { get; }
        TimeJson time { get; }
        int? customDuration { get; }
        long subjectId { get; }
        long lecturerId { get; }
        long roomId { get; }
        long classId { get; }
    }
}
