using NUnit.Framework;
using SchoolAssistant.DAL.Repositories;
using System.Threading.Tasks;

namespace SchoolAssistans.Tests.DbEntities
{
    internal class AppConfigRepositoryTests : BaseDbEntitiesTests
    {
        private IAppConfigRepository _configRepo = null!;

        protected override Task CleanDataAfterEveryTestAsync()
        {
            return Task.CompletedTask;
        }

        protected override Task SetupDataForEveryTestAsync()
        {
            return Task.CompletedTask;
        }

        protected override void SetupServices()
        {
            _configRepo = new AppConfigRepository(_Context, null);
        }


        [Test]
        public void Should_add_config_record()
        {
            _configRepo.DefaultLessonDuration.Set("45");
            _configRepo.Save();

            Assert.AreEqual("45", _configRepo.DefaultLessonDuration.Get());
        }

        [Test]
        public async Task Should_add_config_record_async()
        {
            await _configRepo.DefaultLessonDuration.SetAsync("48");
            await _configRepo.SaveAsync();

            Assert.AreEqual("48", await _configRepo.DefaultLessonDuration.GetAsync());
        }

        [Test]
        public void Should_add_and_save_config_record()
        {
            _configRepo.DefaultLessonDuration.SetAndSave("30");

            Assert.AreEqual("30", _configRepo.DefaultLessonDuration.Get());
        }

        [Test]
        public async Task Should_add_and_save_config_record_async()
        {
            await _configRepo.DefaultLessonDuration.SetAndSaveAsync("35");

            Assert.AreEqual("35", await _configRepo.DefaultLessonDuration.GetAsync());
        }
    }
}
