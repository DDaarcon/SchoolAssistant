using Cronos;
using SchoolAssistant.DAL.Models.Lessons;

namespace SchoolAssistant.Logic
{
    public static class PeriodicLessonExt
    {
        public static CronExpression GetCronExpression(this PeriodicLesson lesson)
            => CronExpression.Parse(lesson.CronPeriodicity);

        public static IEnumerable<DateTime> GetOccurrences(this PeriodicLesson lesson, DateTime from, DateTime to)
            => lesson.GetCronExpression().GetOccurrences(DateTime.SpecifyKind(from, DateTimeKind.Utc), DateTime.SpecifyKind(to, DateTimeKind.Utc));

        public static DateTime? GetNextOccurrence(this PeriodicLesson lesson)
            => lesson.GetCronExpression().GetNextOccurrence(DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc));

        public static IEnumerable<DateTime> GetNextOccurrences(this PeriodicLesson lesson, DateTime from, int limitTo)
        {
            var cron = lesson.GetCronExpression();

            from = DateTime.SpecifyKind(from, DateTimeKind.Utc);

            if (limitTo <= 0)
                yield break;

            var occurrence = cron.GetNextOccurrence(from, true);
            while (limitTo-- > 0)
            {
                yield return occurrence.Value;

                occurrence = cron.GetNextOccurrence(occurrence.Value);
            }
        }

        public static IEnumerable<DateTime> GetPreviousOccurrences(this PeriodicLesson lesson, DateTime to, int limitTo)
        {
            return null;
        }

        /// <returns> <c>DayOfWeek</c> from cron expression. If day is not found (invalid cron), value out of enum range is returned </returns>
        public static DayOfWeek GetDayOfWeek(this PeriodicLesson lesson)
            => lesson.GetNextOccurrence()?.DayOfWeek ?? (DayOfWeek)99;


        public static TimeOnly? GetTime(this PeriodicLesson lesson)
        {
            var occurance = lesson.GetNextOccurrence();
            if (occurance is null) return null;
            return TimeOnly.FromDateTime(occurance.Value);
        }
    }
}
