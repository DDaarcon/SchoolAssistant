namespace SchoolAssistant.Logic.Help
{
    public static class CronExpressionsHelper
    {
        public static string Weekly(int hour, int minute, DayOfWeek day)
            => $"{minute} {hour} * * {(int)day}";
    }
}
