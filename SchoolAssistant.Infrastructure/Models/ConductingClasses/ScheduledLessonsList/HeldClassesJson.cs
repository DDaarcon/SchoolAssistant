namespace SchoolAssistant.Infrastructure.Models.ConductingClasses.ScheduledLessonsList
{
    public class HeldClassesJson
    {
        public string topic { get; set; } = null!;
        public int amountOfPresentStudents { get; set; }
        public int amountOfAllStudents { get; set; }
    }
}
