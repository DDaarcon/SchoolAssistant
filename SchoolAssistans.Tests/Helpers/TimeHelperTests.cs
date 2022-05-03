using NUnit.Framework;
using SchoolAssistant.Logic.Help;
using System;

namespace SchoolAssistans.Tests.Helpers
{
    public class TimeHelperTests
    {
        [Test]
        public void Should_overlap_left_side()
        {
            var aStart = new TimeOnly(10, 0);
            var aDur = 50;
            var bStart = new TimeOnly(10, 30);
            var bDur = 45;

            Assert.IsTrue(TimeHelper.AreOverlapping(aStart, aDur, bStart, bDur));
        }
        [Test]
        public void Should_overlap_right_side()
        {
            var aStart = new TimeOnly(11, 0);
            var aDur = 50;
            var bStart = new TimeOnly(10, 30);
            var bDur = 45;

            Assert.IsTrue(TimeHelper.AreOverlapping(aStart, aDur, bStart, bDur));
        }
        [Test]
        public void Should_overlap_within_one()
        {
            var aStart = new TimeOnly(10, 20);
            var aDur = 10;
            var bStart = new TimeOnly(10, 0);
            var bDur = 45;

            Assert.IsTrue(TimeHelper.AreOverlapping(aStart, aDur, bStart, bDur));
        }
        [Test]
        public void Should_overlap_within_two()
        {
            var aStart = new TimeOnly(10, 0);
            var aDur = 50;
            var bStart = new TimeOnly(10, 20);
            var bDur = 10;

            Assert.IsTrue(TimeHelper.AreOverlapping(aStart, aDur, bStart, bDur));
        }
        [Test]
        public void Should_not_overlap_left_side()
        {
            var aStart = new TimeOnly(10, 0);
            var aDur = 45;
            var bStart = new TimeOnly(12, 30);
            var bDur = 45;

            Assert.IsFalse(TimeHelper.AreOverlapping(aStart, aDur, bStart, bDur));
        }
        [Test]
        public void Should_not_overlap_right_side()
        {
            var aStart = new TimeOnly(11, 0);
            var aDur = 45;
            var bStart = new TimeOnly(10, 0);
            var bDur = 45;

            Assert.IsFalse(TimeHelper.AreOverlapping(aStart, aDur, bStart, bDur));
        }
        [Test]
        public void Should_not_overlap_touching_left()
        {
            var aStart = new TimeOnly(10, 0);
            var aDur = 45;
            var bStart = new TimeOnly(10, 45);
            var bDur = 45;

            Assert.IsFalse(TimeHelper.AreOverlapping(aStart, aDur, bStart, bDur));
        }
        [Test]
        public void Should_not_overlap_touching_right()
        {
            var aStart = new TimeOnly(11, 0);
            var aDur = 45;
            var bStart = new TimeOnly(10, 15);
            var bDur = 45;

            Assert.IsFalse(TimeHelper.AreOverlapping(aStart, aDur, bStart, bDur));
        }
    }
}
