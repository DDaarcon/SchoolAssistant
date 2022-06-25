using SchoolAssistant.Infrastructure.Enums.Marks;

namespace SchoolAssistant.Infrastructure.Models.ConductingClasses.ConductLesson
{
    public class MarkJson
    {
        public string? prefix { get; set; }
        public MarkValue value { get; set; }
    }
}
