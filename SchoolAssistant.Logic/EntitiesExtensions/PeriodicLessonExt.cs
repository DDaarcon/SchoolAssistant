using Cronos;
using SchoolAssistant.DAL.Models.Lessons;

namespace SchoolAssistant.Logic
{
    public static class PeriodicLessonExt
    {
        public static CronExpression GetCronExpression(this PeriodicLesson lesson)
            => CronExpression.Parse(lesson.CronPeriodicity);

        public static DateTime? GetNextOccurance(this PeriodicLesson lesson)
            => lesson.GetCronExpression().GetNextOccurrence(DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc));

        /// <returns> <c>DayOfWeek</c> from cron expression. If day is not found (invalid cron), value out of enum range is returned </returns>
        public static DayOfWeek GetDayOfWeek(this PeriodicLesson lesson)
            => lesson.GetNextOccurance()?.DayOfWeek ?? (DayOfWeek)99;


        public static TimeOnly? GetTime(this PeriodicLesson lesson)
        {
            var occurance = lesson.GetNextOccurance();
            if (occurance is null) return null;
            return TimeOnly.FromDateTime(occurance.Value);
        }
    }
}
