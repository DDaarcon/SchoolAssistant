namespace SchoolAssistant.Infrastructure.Models.ScheduleArranger
{
    public class ScheduleDayLessonsJson
    {
        public DayOfWeek dayIndicator { get; set; }
        public PeriodicLessonTimetableEntryJson[] lessons { get; set; } = null!;
    }
}
