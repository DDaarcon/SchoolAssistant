namespace SchoolAssistant.Logic.Help
{
    public static class DatesHelper
    {
        public static (DateTime start, DateTime end) GetStartAndEndOfCurrentWeek()
        {
            return GetStartAndEndOfWeek(DateTime.Now);
        }

        public static (DateTime start, DateTime end) GetStartAndEndOfWeek(DateTime forDate)
        {
            var daysToSunday = (int)forDate.DayOfWeek;

            var start = forDate.AddDays(-daysToSunday).Date;

            var end = forDate.AddDays(7 - daysToSunday).Date.AddSeconds(-1);

            return (start, end);
        }
    }
}
