using SchoolAssistant.Infrastructure.Models.ScheduleShared;

namespace SchoolAssistant.Infrastructure.Models.ScheduleArranger.PageModelToReact
{
    public class ScheduleArrangerConfigJson : ScheduleTimelineConfigJson
    {
        public int cellDuration { get; set; }
        public int cellHeight { get; set; }

        public long? classId { get; set; }
    }
}
