using NUnit.Framework;
using SchoolAssistant.DAL.Models.SchoolYears;
using SchoolAssistant.DAL.Repositories;
using System;
using System.Threading.Tasks;

namespace SchoolAssistans.Tests.DbEntities
{
    public class SchoolYearTests
    {

        private ISchoolYearRepository _schoolYearRepo = null!;

        [OneTimeSetUp]
        public void Setup()
        {
            TestDatabase.CreateContext(TestServices.Collection);

            _schoolYearRepo = new SchoolYearRepository(TestDatabase.Context, null);
        }

        [OneTimeTearDown]
        public void Teardown()
        {
            TestDatabase.DisposeContext();
        }

        [SetUp]
        public async Task SetupOne()
        {
            await TestDatabase.ClearDataAsync<SchoolYear>();
        }

        [Test]
        public void Should_create_school_year_when_none()
        {
            var year = _schoolYearRepo.GetOrCreateCurrent();

            Assert.IsNotNull(year);
            Assert.IsTrue(year.Current);
            Assert.IsTrue(year.Year == GetStartYearForCurrent());
        }

        [Test]
        public async Task Should_create_school_year_when_none_async()
        {
            var year = await _schoolYearRepo.GetOrCreateCurrentAsync();

            Assert.IsNotNull(year);
            Assert.IsTrue(year.Current);
            Assert.IsTrue(year.Year == GetStartYearForCurrent());
        }

        [Test]
        public void Should_create_by_calendar_year()
        {
            var current = _schoolYearRepo.GetOrCreateCurrent();
            var prev = _schoolYearRepo.GetOrCreate(current.Year - 1);

            Assert.IsNotNull(prev);
            Assert.AreEqual(prev.Year, GetStartYearForCurrent() - 1);
            Assert.IsFalse(prev.Current);
        }

        [Test]
        public async Task Should_create_by_calendar_year_async()
        {
            var current = await _schoolYearRepo.GetOrCreateCurrentAsync();
            var prev = await _schoolYearRepo.GetOrCreateAsync(current.Year - 1);

            Assert.IsNotNull(prev);
            Assert.AreEqual(prev.Year, GetStartYearForCurrent() - 1);
            Assert.IsFalse(prev.Current);
        }

        [Test]
        public async Task Should_change_current_async()
        {
            var current = await _schoolYearRepo.GetOrCreateCurrentAsync();

            var newCurrent = new SchoolYear
            {
                Year = (short)(current.Year - 1),
                Current = true
            };
            await _schoolYearRepo.AddAsync(newCurrent);
            await _schoolYearRepo.SaveAsync();

            Assert.IsFalse(current.Current);
            Assert.IsTrue(newCurrent.Current);
        }

        private int GetStartYearForCurrent()
        {
            var today = DateTime.Today;

            return today.DayOfYear < 365 / 2
                ? today.Year - 1
                : today.Year;
        }
    }
}
