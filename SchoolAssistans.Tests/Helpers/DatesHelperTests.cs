using NUnit.Framework;
using SchoolAssistant.Logic.Help;
using System;

namespace SchoolAssistans.Tests.Helpers
{
    public class DatesHelperTests
    {
        [Test]
        public void GetStartAndEndOfWeekTest()
        {
            var date = new DateTime(2022, 4, 9, 8, 40, 10);

            var (start, end) = DatesHelper.GetStartAndEndOfWeek(date);

            Assert.IsTrue(start.Equals(new DateTime(2022, 4, 3, 0, 0, 0)));
            Assert.IsTrue(end.Equals(new DateTime(2022, 4, 9, 23, 59, 59)));
        }

        [Test]
        public void GetStartAndEndOfWeekTest_AtLeftBorder()
        {
            var date = new DateTime(2022, 4, 3, 0, 0, 0);

            var (start, end) = DatesHelper.GetStartAndEndOfWeek(date);

            Assert.IsTrue(start.Equals(new DateTime(2022, 4, 3, 0, 0, 0)));
            Assert.IsTrue(end.Equals(new DateTime(2022, 4, 9, 23, 59, 59)));
        }

        [Test]
        public void GetStartAndEndOfWeekTest_AtRightBorder()
        {
            var date = new DateTime(2022, 4, 9, 23, 59, 59);

            var (start, end) = DatesHelper.GetStartAndEndOfWeek(date);

            Assert.IsTrue(start.Equals(new DateTime(2022, 4, 3, 0, 0, 0)));
            Assert.IsTrue(end.Equals(new DateTime(2022, 4, 9, 23, 59, 59)));
        }
    }
}
