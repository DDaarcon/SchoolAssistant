using NUnit.Framework;
using SchoolAssistant.DAL;
using SchoolAssistant.DAL.Models.SchoolYears;
using SchoolAssistant.DAL.Repositories;
using SchoolAssistant.Infrastructure.Models.Shared.Json;
using System.Threading.Tasks;

namespace SchoolAssistans.Tests.DbEntities
{
    public abstract class BaseDbEntitiesTests
    {
        protected ISchoolYearRepository _schoolYearRepo = null!;

        protected Task<SchoolYear> _Year => _schoolYearRepo.GetOrCreateCurrentAsync();

        protected SADbContext _Context => TestDatabase.Context;

        [OneTimeTearDown]
        public void Teardown()
        {
            TestDatabase.DisposeContext();
        }


        [SetUp]
        public async Task SetupOne()
        {
            TestDatabase.RequestContextFromServices(TestServices.Collection);

            _schoolYearRepo = new SchoolYearRepository(_Context, null);
            SetupServices();

            await SetupDataForEveryTestAsync();
        }

        [TearDown]
        public async Task TeardownOne()
        {
            await CleanDataAfterEveryTestAsync();
        }


        protected void AssertResponseSuccess(ResponseJson? res)
        {
            Assert.IsNotNull(res);
            Assert.IsTrue(res!.success);
        }
        protected void AssertResponseFail(ResponseJson? res)
        {
            Assert.IsNotNull(res);
            Assert.IsFalse(res!.success);
        }

        protected abstract void SetupServices();
        protected abstract Task CleanDataAfterEveryTestAsync();
        protected abstract Task SetupDataForEveryTestAsync();

    }
}
