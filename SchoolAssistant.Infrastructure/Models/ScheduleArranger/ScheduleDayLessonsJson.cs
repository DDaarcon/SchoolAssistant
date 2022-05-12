namespace SchoolAssistant.Infrastructure.Models.ScheduleArranger
{
    public class ScheduleDayLessonsJson : ScheduleDayLessonsJson<ScheduleLessonTimetableEntryJson> { }
    public class ScheduleDayLessonsJson<TLesson>
        where TLesson : ScheduleLessonTimetableEntryJson
    {
        public DayOfWeek dayIndicator { get; set; }
        public TLesson[] lessons { get; set; } = null!;
    }
}
