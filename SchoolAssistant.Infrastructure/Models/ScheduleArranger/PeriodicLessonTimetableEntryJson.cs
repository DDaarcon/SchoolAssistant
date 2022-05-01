namespace SchoolAssistant.Infrastructure.Models.ScheduleArranger
{
    public class PeriodicLessonTimetableEntryJson
    {
        public long? id { get; set; }

        public TimeJson time { get; set; }
        public int? customDuration { get; set; }

        public IdNameJson subject { get; set; } = null!;
        public IdNameJson lecturer { get; set; } = null!;
        public IdNameJson room { get; set; } = null!;
    }
}
