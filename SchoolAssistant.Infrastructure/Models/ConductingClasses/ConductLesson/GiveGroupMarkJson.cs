namespace SchoolAssistant.Infrastructure.Models.ConductingClasses.ConductLesson
{
    public class GiveGroupMarkJson
    {
        public long lessonId { get; set; }
        public string description { get; set; } = null!;
        public int? weight { get; set; }
        public StudentMarkJson[] marks { get; set; } = null!;
    }
}
