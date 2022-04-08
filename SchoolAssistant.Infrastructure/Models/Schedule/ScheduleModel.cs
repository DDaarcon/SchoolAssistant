namespace SchoolAssistant.Infrastructure.Models.Schedule
{
    public class ScheduleModel
    {
        public string Name { get; set; } = null!;

        public IList<ScheduleItemModel> MondayItems { get; set; } = null!;
        public IList<ScheduleItemModel> TuesdayItems { get; set; } = null!;
        public IList<ScheduleItemModel> WednesdayItems { get; set; } = null!;
        public IList<ScheduleItemModel> Thurstems { get; set; } = null!;
        public IList<ScheduleItemModel> FridayItems { get; set; } = null!;
        public IList<ScheduleItemModel>? SaturdayItems { get; set; }
        public IList<ScheduleItemModel>? SundayItems { get; set; }

        public IList<ScheduleItemModel>? Items { get; set; }
    }
}
