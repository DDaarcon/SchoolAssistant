namespace SchoolAssistant.Infrastructure.Models.ScheduleArranger
{
    public class ScheduleArrangerConfigJson
    {
        public int defaultLessonDuration { get; set; }
        public int startHour { get; set; }
        public int endHour { get; set; }

        public int cellDuration { get; set; }
        public int cellHeight { get; set; }
    }
}
