namespace SchoolAssistant.Infrastructure.Models.ConductingClasses.ScheduledLessonsList
{
    public class FetchScheduledLessonsRequestJson
    {
        public long? fromTk { get; set; }
        public long? toTk { get; set; }
        public bool OnlyUpcoming { get; set; }
        public int? LimitTo { get; set; }
    }
}
