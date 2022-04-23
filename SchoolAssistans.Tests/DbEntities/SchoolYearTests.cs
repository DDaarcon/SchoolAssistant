using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using SchoolAssistant.DAL.Models.SchoolYears;
using SchoolAssistant.DAL.Repositories;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolAssistans.Tests.DbEntities
{
    public class SchoolYearTests
    {

        private ISchoolYearService _schoolYearService;
        private IRepository<SchoolYear> _repo;

        [OneTimeSetUp]
        public void Setup()
        {
            TestDatabase.CreateContext(TestServices.Collection);

            _repo = new Repository<SchoolYear>(TestDatabase.Context, null);
            _schoolYearService = new SchoolYearService(_repo);
        }

        [OneTimeTearDown]
        public void Teardown()
        {
            TestDatabase.DisposeContext();
        }

        [Test]
        public void Should_create_school_year_when_none()
        {
            if (_repo.AsQueryable().Any())
            {
                _repo.RemoveRange(_repo.AsList());
                _repo.Save();
            }

            var year = _schoolYearService.GetOrCreateCurrent();

            Assert.IsNotNull(year);
            Assert.IsTrue(year.Current);
            Assert.IsTrue(year.Year == GetStartYearForCurrent());
        }

        [Test]
        public async Task Should_create_school_year_when_none_async()
        {
            if (await _repo.AsQueryable().AnyAsync())
            {
                _repo.RemoveRange(await _repo.AsListAsync());
                await _repo.SaveAsync();
            }

            var year = await _schoolYearService.GetOrCreateCurrentAsync();

            Assert.IsNotNull(year);
            Assert.IsTrue(year.Current);
            Assert.IsTrue(year.Year == GetStartYearForCurrent());
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
