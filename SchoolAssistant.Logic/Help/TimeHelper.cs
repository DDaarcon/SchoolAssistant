namespace SchoolAssistant.Logic.Help
{
    public static class TimeHelper
    {
        public static bool AreOverlapping(TimeOnly timeA, int aDurationMinutes, TimeOnly timeB, int bDurationMinutes)
        {
            var aEnd = timeA.AddMinutes(aDurationMinutes);
            var bEnd = timeB.AddMinutes(bDurationMinutes);

            return timeA > timeB && timeA < bEnd
                || aEnd > timeB && aEnd < bEnd
                || timeA <= timeB && aEnd >= bEnd;
        }
    }
}
