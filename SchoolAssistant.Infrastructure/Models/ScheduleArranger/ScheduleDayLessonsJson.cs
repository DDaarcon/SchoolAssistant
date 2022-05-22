using SchoolAssistant.Infrastructure.Models.ScheduleShared;

namespace SchoolAssistant.Infrastructure.Models.ScheduleArranger
{
    public class ScheduleDayLessonsJson : ScheduleDayLessonsJson<LessonTimetableEntryJson> { }
    public class ScheduleDayLessonsJson<TLesson>
        where TLesson : LessonTimetableEntryJson
    {
        public DayOfWeek dayIndicator { get; set; }
        public TLesson[] lessons { get; set; } = null!;
    }
}
