namespace SchoolAssistant.Infrastructure.Models.ConductingClasses.ScheduledLessonsList
{
    public class ScheduledLessonListEntryJson
    {
        public string className { get; set; } = null!;
        public string subjectName { get; set; } = null!;
        public long startTimeTk { get; set; }
        public int duration { get; set; }
        public HeldClassesJson? heldClasses { get; set; }
    }
}
