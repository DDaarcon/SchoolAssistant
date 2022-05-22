namespace SchoolAssistant.Infrastructure.Models.ScheduleShared
{
    public class LessonJson : LessonTimetableEntryJson
    {
        public IdNameJson? orgClass { get; set; }
        public IdNameJson? subjClass { get; set; }
    }
}
