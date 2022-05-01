namespace SchoolAssistant.Infrastructure.Models.ScheduleArranger
{
    public class PeriodicLessonTimetableEntryJson
    {
        public long? id { get; set; }

        public int hour { get; set; }
        public int minutes { get; set; }
        public int? customDuration { get; set; }

        public string subjectName { get; set; } = null!;
        public string lecturerName { get; set; } = null!;
        public string roomName { get; set; } = null!;
    }
}
