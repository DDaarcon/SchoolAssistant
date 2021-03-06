namespace SchoolAssistant.Infrastructure.Models.ScheduleShared
{
    public class TimeJson
    {
        public int hour { get; set; }
        public int minutes { get; set; }

        public TimeJson() { }

        public TimeJson(TimeOnly time)
        {
            hour = time.Hour;
            minutes = time.Minute;
        }
    }
}
