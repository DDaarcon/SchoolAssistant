namespace SchoolAssistant.Infrastructure.Models.ConductingClasses.ScheduledLessonsList
{
    public class ScheduledLessonListJson
    {
        public ScheduledLessonListEntryJson[] entries { get; set; } = null!;
        public long? incomingAtTk { get; set; }
        public int minutesBeforeLessonIsSoon { get; set; }

        public string? tableClassName { get; set; }
        public string? theadClassName { get; set; }
        public string? theadTrClassName { get; set; }
        public string? tbodyClassName { get; set; }
        public string? tbodyTrClassName { get; set; }
    }
}
