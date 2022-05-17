using SchoolAssistant.Infrastructure.Enums.Schedule;

namespace SchoolAssistant.Infrastructure.Models.Schedule
{
    public class ScheduleConfigJson
    {
        public string locale { get; set; } = null!;
        public DayOfWeek[] hiddenDays { get; set; } = null!;
        public int startHour { get; set; }
        public int endHour { get; set; }
        public ScheduleViewerType @for { get; set; }
    }
}
