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

        public static double GetMillisecondsJS(this DateTime date)
        {
            return date
                .Subtract(new DateTime(1970, 1, 1, 0, 0, 0))
                .TotalMilliseconds;
        }
    }
}
