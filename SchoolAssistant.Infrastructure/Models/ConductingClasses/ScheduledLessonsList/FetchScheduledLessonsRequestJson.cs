namespace SchoolAssistant.Infrastructure.Models.ConductingClasses.ScheduledLessonsList
{
    public class FetchScheduledLessonsRequestJson
    {
        public long? fromTk { get; set; }
        public long? toTk { get; set; }
        public bool onlyUpcoming { get; set; }
        public int? limitTo { get; set; }
    }
}
