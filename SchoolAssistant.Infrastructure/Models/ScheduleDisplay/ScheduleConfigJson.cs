using SchoolAssistant.Infrastructure.Enums.Schedule;
using SchoolAssistant.Infrastructure.Models.ScheduleShared;

namespace SchoolAssistant.Infrastructure.Models.ScheduleDisplay
{
    public class ScheduleConfigJson : ScheduleTimelineConfigJson
    {
        public ScheduleViewerType @for { get; set; }
    }
}
