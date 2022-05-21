namespace SchoolAssistant.Infrastructure.Models.ScheduleDisplay
{
    public class ScheduleItemModel
    {
        public string Name { get; set; } = null!;
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string TeacherName { get; set; } = null!;
        public string Room { get; set; } = null!;
    }
}
