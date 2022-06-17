namespace SchoolAssistant.Logic.Help
{
    public static class DatesHelper
    {
        public static (DateTime start, DateTime end) GetStartAndEndOfCurrentWeek()
        {
            return GetStartAndEndOfWeek(DateTime.UtcNow);
        }

        public static (DateTime start, DateTime end) GetStartAndEndOfWeek(DateTime forDate)
        {
            var daysToSunday = (int)forDate.DayOfWeek;

            var start = forDate.AddDays(-daysToSunday).Date;

            var end = forDate.AddDays(7 - daysToSunday).Date.AddSeconds(-1);

            return (start, end);
        }

        public static double GetMillisecondsJs(this DateTime date)
        {
            return date
                .Subtract(new DateTime(1970, 1, 1, 0, 0, 0))
                .TotalMilliseconds;
        }

        public static double GetMillisecondsJsUTC(this DateTime date)
        {
            return date.ToUniversalTime()
                .Subtract(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc))
                .TotalMilliseconds;
        }



        public static long? GetTicksJs(this DateTime? date) => date.HasValue ? GetTicksJs(date.Value) : null;
        public static long GetTicksJs(this DateTime date) => (date.ToUniversalTime().Ticks - UnixEpochTicks) / 10000;


        public static long GetTicksJsFakeUtc(this DateTime date) => (DateTime.SpecifyKind(date, DateTimeKind.Utc).Ticks - UnixEpochTicks) / 10000;
        public static long GetTicksJsFakeLocal(this DateTime date) => (DateTime.SpecifyKind(date, DateTimeKind.Local).ToUniversalTime().Ticks - UnixEpochTicks) / 10000;

        public static DateTime? FromTicksJs(long? ticksJs) => ticksJs.HasValue ? FromTicksJs(ticksJs.Value) : null;
        public static DateTime FromTicksJs(long ticksJs) => new DateTime((ticksJs * 10000) + UnixEpochTicks, DateTimeKind.Utc);

        private static readonly long UnixEpochTicks = (new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).Ticks;
    }
}
