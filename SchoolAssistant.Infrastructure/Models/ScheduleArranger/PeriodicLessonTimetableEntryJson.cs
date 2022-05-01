namespace SchoolAssistant.Infrastructure.Models.ScheduleArranger
{
    public class PeriodicLessonTimetableEntryJson
    {
        public long? id { get; set; }

        public int hour { get; set; }
        public int minutes { get; set; }
        public int? customDuration { get; set; }

        public IdNameJson subject { get; set; } = null!;
        public IdNameJson lecturer { get; set; } = null!;
        public IdNameJson room { get; set; } = null!;
    }
}
