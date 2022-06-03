namespace SchoolAssistant.Infrastructure.Models.ConductingClasses.ScheduledLessonsList
{
    public class ScheduledLessonListItemModel
    {
        public string ClassName { get; set; } = null!;
        public string SubjectName { get; set; } = null!;
        public DateTime StartTime { get; set; }
        public int Duration { get; set; }
        public HeldClassesModel? HeldClasses { get; set; }
    }
}
