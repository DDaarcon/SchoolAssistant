namespace SchoolAssistant.Infrastructure.Models.ScheduleShared
{
    public class LessonTimetableEntryJson
    {
        public long? id { get; set; }

        public TimeJson time { get; set; } = null!;
        public int? customDuration { get; set; }

        public IdNameJson subject { get; set; } = null!;
        public IdNameJson lecturer { get; set; } = null!;
        public IdNameJson room { get; set; } = null!;
    }
}
