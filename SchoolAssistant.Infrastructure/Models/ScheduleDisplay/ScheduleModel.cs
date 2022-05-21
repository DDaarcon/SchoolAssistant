namespace SchoolAssistant.Infrastructure.Models.ScheduleDisplay
{
    public class ScheduleModel
    {
        public string Name { get; set; } = null!;
        public string Locale { get; set; } = null!;

        public TimeSpan Earliest { get; set; }
        public TimeSpan Latest { get; set; }

        public bool AnyInSaturday { get; set; }
        public bool AnyInSunday { get; set; }
        public IList<ScheduleItemModel> Lessons { get; set; } = null!;
    }
}
