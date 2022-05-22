using SchoolAssistant.Infrastructure.Models.ScheduleShared;

namespace SchoolAssistant.Infrastructure.Models.ScheduleArranger.PageModelToReact
{
    public class ScheduleTeacherEntryJson : IdNameJson
    {
        public string shortName { get; set; } = null!;
        public long[] mainSubjectIds { get; set; } = null!;
        public long[] additionalSubjectIds { get; set; } = null!;
    }
}
