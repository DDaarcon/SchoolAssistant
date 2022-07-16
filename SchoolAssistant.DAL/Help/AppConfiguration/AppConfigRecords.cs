using AppConfigurationEFCore.Configuration;

namespace SchoolAssistant.DAL.Help.AppConfiguration
{
    public class AppConfigRecords
    {
        [RecordKey("defaultLessonDuration")]
        public VTRecordHandler<int> DefaultLessonDuration { get; private set; } = null!;
        [RecordKey("scheduleArrangerCellDuration")]
        public VTRecordHandler<int> ScheduleArrangerCellDuration { get; private set; } = null!;
        [RecordKey("scheduleArrangerCellHeight")]
        public VTRecordHandler<int> ScheduleArrangerCellHeight { get; private set; } = null!;
        [RecordKey("defaultRoomName")]
        public RecordHandler<string> DefaultRoomName { get; private set; } = null!;
        [RecordKey("scheduleStartHour")]
        public VTRecordHandler<int> ScheduleStartHour { get; private set; } = null!;
        [RecordKey("scheduleEndHour")]
        public VTRecordHandler<int> ScheduleEndhour { get; private set; } = null!;

        [RecordKey("minutesBeforeLessonConsideredClose")]
        public VTRecordHandler<int> MinutesBeforeLessonConsideredClose { get; set; } = null!;

        [RecordKey("hiddenDays")]
        public RecordHandler<IEnumerable<DayOfWeek>> HiddenDays { get; set; } = null!;

        [RecordKey("lessonsListTopicLengthLimit")]
        public VTRecordHandler<int> LessonsListTopicLengthLimit { get; set; } = null!;
    }
}
