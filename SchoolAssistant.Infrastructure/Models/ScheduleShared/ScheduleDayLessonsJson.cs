namespace SchoolAssistant.Infrastructure.Models.ScheduleShared
{
    public class ScheduleDayLessonsJson : ScheduleDayLessonsJson<LessonTimetableEntryJson> { }
    public class ScheduleDayLessonsJson<TLesson>
        where TLesson : LessonTimetableEntryJson
    {
        public DayOfWeek dayIndicator { get; set; }
        public TLesson[] lessons { get; set; } = null!;
    }
}
