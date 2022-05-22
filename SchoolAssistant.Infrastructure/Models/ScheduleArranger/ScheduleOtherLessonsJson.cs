using SchoolAssistant.Infrastructure.Models.ScheduleShared;

namespace SchoolAssistant.Infrastructure.Models.ScheduleArranger
{
    public class ScheduleOtherLessonsJson
    {
        public ScheduleDayLessonsJson<LessonJson>[]? teacher { get; set; }
        public ScheduleDayLessonsJson<LessonJson>[]? room { get; set; }
    }
}
