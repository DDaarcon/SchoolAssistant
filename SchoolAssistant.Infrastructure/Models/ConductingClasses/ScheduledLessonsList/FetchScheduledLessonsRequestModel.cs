namespace SchoolAssistant.Infrastructure.Models.ConductingClasses.ScheduledLessonsList
{
    public class FetchScheduledLessonsRequestModel
    {
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
        public bool OnlyUpcoming { get; set; }
        public int? LimitTo { get; set; }
    }
    public class FetchScheduledLessonsRequestJson
    {
        public long? fromTk { get; set; }
        public long? toTk { get; set; }
        public bool OnlyUpcoming { get; set; }
        public int? LimitTo { get; set; }
    }
}
