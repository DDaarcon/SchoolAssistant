namespace SchoolAssistant.Infrastructure.Models.ConductingClasses.ScheduledLessonsList
{
    public class FetchScheduledLessonListModel
    {
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
        public bool OnlyUpcoming { get; set; }
        public int? LimitTo { get; set; }
    }
}
