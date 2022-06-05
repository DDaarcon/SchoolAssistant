using AppConfigurationEFCore;
using AppConfigurationEFCore.Setup;
using NUnit.Framework;
using SchoolAssistant.DAL;
using SchoolAssistant.DAL.Help.AppConfiguration;
using SchoolAssistant.DAL.Models.SchoolYears;
using SchoolAssistant.DAL.Repositories;
using SchoolAssistant.Infrastructure.Models.Shared.Json;
using System.Diagnostics;
using System.Threading.Tasks;

namespace SchoolAssistans.Tests.DbEntities
{
    public abstract class BaseDbEntitiesTests
    {
        protected ISchoolYearRepository _schoolYearRepo = null!;
        protected IAppConfiguration<AppConfigRecords> _configRepo = null!;

        protected Task<SchoolYear> _Year => _schoolYearRepo.GetOrCreateCurrentAsync();

        protected SADbContext _Context => TestDatabase.Context;

        [OneTimeTearDown]
        public void Teardown()
        {
            TestDatabase.DisposeContext();
        }

        [OneTimeSetUp]
        public void StartTest()
        {
            Trace.Listeners.Add(new ConsoleTraceListener());
        }

        [OneTimeTearDown]
        public void EndTest()
        {
            Trace.Flush();
        }

        [SetUp]
        public async Task SetupOne()
        {
            TestDatabase.RequestContextFromServices(TestServices.Collection);
            TestServices.Collection.AddAppConfiguration<SADbContext, AppConfigRecords>();

            _schoolYearRepo = new SchoolYearRepository(_Context, null);
            _configRepo = TestServices.GetService<IAppConfiguration<AppConfigRecords>>();
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
            if (!res!.success)
                Debug.WriteLine(res.message);
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
