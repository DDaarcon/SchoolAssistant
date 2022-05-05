namespace SchoolAssistant.Infrastructure.Models.ScheduleArranger
{
    public class ScheduleOtherLessonsJson
    {
        public PeriodicLessonTimetableEntryJson[]? teacher { get; set; }
        public PeriodicLessonTimetableEntryJson[]? room { get; set; }
    }
}
