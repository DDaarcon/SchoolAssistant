namespace SchoolAssistant.Infrastructure.Models.ConductingClasses.ScheduledLessonsList
{
    public class ScheduledLessonListModel
    {
        public IEnumerable<ScheduledLessonListItemModel> Items { get; set; } = null!;
        public DateTime? IncomingAt { get; set; }
        public int MinutessBeforeClose { get; set; }
    }
}
