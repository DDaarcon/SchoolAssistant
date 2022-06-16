using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolAssistant.Logic;
using NUnit.Framework;
using SchoolAssistant.DAL.Models.Lessons;
using SchoolAssistant.Logic.Help;

namespace SchoolAssistans.Tests.Helpers
{
    internal class PeriodicLessonHelperTests 
    {

        [Test]
        public void ShouldGetNext10Occurrences()
        {
            var lesson = new PeriodicLesson
            {
                CronPeriodicity = CronExpressionsHelper.Weekly(10, 30, DayOfWeek.Tuesday)
            };

            var date = new DateTime(2022, 6, 8); // wednesday

            var occ = lesson.GetNextOccurrences(date, 10);

            OccurrencesMatch(occ,
                new DateTime(2022, 06, 14, 10, 30, 0),
                new DateTime(2022, 06, 21, 10, 30, 0),
                new DateTime(2022, 06, 28, 10, 30, 0),
                new DateTime(2022, 07, 5, 10, 30, 0),
                new DateTime(2022, 07, 12, 10, 30, 0),
                new DateTime(2022, 07, 19, 10, 30, 0),
                new DateTime(2022, 07, 26, 10, 30, 0),
                new DateTime(2022, 08, 2, 10, 30, 0),
                new DateTime(2022, 08, 9, 10, 30, 0),
                new DateTime(2022, 08, 16, 10, 30, 0));
        }

        [Test]
        public void ShouldGetPrevious10Occurrences()
        {
            var lesson = new PeriodicLesson
            {
                CronPeriodicity = CronExpressionsHelper.Weekly(10, 30, DayOfWeek.Tuesday)
            };

            var date = new DateTime(2022, 6, 6); // monday

            var occ = lesson.GetPreviousOccurrences(date, 10);

            OccurrencesMatch(occ,
                new DateTime(2022, 05, 31, 10, 30, 0),
                new DateTime(2022, 05, 24, 10, 30, 0),
                new DateTime(2022, 05, 17, 10, 30, 0),
                new DateTime(2022, 05, 10, 10, 30, 0),
                new DateTime(2022, 05, 3, 10, 30, 0),
                new DateTime(2022, 04, 26, 10, 30, 0),
                new DateTime(2022, 04, 19, 10, 30, 0),
                new DateTime(2022, 04, 12, 10, 30, 0),
                new DateTime(2022, 04, 5, 10, 30, 0),
                new DateTime(2022, 03, 29, 10, 30, 0));
        }

        [Test]
        public void ShouldGetEmpty_WhenLimitNegative()
        {
            var lesson = new PeriodicLesson
            {
                CronPeriodicity = CronExpressionsHelper.Weekly(10, 30, DayOfWeek.Tuesday)
            };

            var date = new DateTime(2022, 6, 6); // monday

            var occ = lesson.GetPreviousOccurrences(date, -2);
            OccurrencesMatch(occ);

            occ = lesson.GetNextOccurrences(date, -5);
            OccurrencesMatch(occ);
        }


        private void OccurrencesMatch(IEnumerable<DateTime> res, params DateTime[] toMatch)
        {
            Assert.IsNotNull(res);
            Assert.AreEqual(res.Count(), toMatch.Length);

            for (int i = 0; i < toMatch.Length; i++)
                Assert.AreEqual(res.ElementAt(i), toMatch[i]);
        }
    }
}
