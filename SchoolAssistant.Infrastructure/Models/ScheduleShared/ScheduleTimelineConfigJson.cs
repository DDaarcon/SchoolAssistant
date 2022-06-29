namespace SchoolAssistant.Infrastructure.Models.ScheduleShared
{
    public class ScheduleTimelineConfigJson
    {
        public int startHour { get; set; }
        public int endHour { get; set; }

        public DayOfWeek[] hiddenDays { get; set; } = null!;
        public int defaultLessonDuration { get; set; }
    }
}
