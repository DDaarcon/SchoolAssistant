namespace SchoolAssistant.Infrastructure.Models.ScheduleArranger
{
    public class ScheduleOtherLessonsJson
    {
        public ScheduleDayLessonsJson[]? teacher { get; set; }
        public ScheduleDayLessonsJson[]? room { get; set; }
    }
}
