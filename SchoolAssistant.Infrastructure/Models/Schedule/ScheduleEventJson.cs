namespace SchoolAssistant.Infrastructure.Models.Schedule
{
    public class ScheduleEventJson
    {
        public string id { get; set; } = null!;
        public string title { get; set; } = null!;
        public double start { get; set; }
        public double end { get; set; }
        public string @class { get; set; } = null!;
        public string lecturer { get; set; } = null!;
        public string room { get; set; } = null!;
        public string subject { get; set; } = null!;
    }
}
