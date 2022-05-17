using SchoolAssistant.Infrastructure.Enums.Schedule;

namespace SchoolAssistant.Infrastructure.Models.Schedule
{
    public class ScheduleConfigJson
    {
        public string locale { get; set; } = null!;
        public DayOfWeek[] hiddenDays { get; set; } = null!;
        public string startTime { get; set; } = null!;
        public string endTime { get; set; } = null!;
        public ScheduleViewerType @for { get; set; }
    }
}
