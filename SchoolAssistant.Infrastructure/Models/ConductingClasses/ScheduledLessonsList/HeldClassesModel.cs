namespace SchoolAssistant.Infrastructure.Models.ConductingClasses.ScheduledLessonsList
{
    public class HeldClassesModel
    {
        public string Topic { get; set; } = null!;
        public int AmountOfPresentStudents { get; set; }
        public int AmountOfAllStudents { get; set; }
    }
}
