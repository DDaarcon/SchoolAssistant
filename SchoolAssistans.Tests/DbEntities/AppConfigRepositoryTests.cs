using NUnit.Framework;
using System.Threading.Tasks;

namespace SchoolAssistans.Tests.DbEntities
{
    internal class AppConfigRepositoryTests : BaseDbEntitiesTests
    {
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
        }


        [Test]
        public void Should_add_config_record()
        {
            _configRepo.Records.DefaultLessonDuration.Set(45);
            _configRepo.Save();

            Assert.AreEqual(45, _configRepo.Records.DefaultLessonDuration.Get());
        }

        [Test]
        public async Task Should_add_config_record_async()
        {
            await _configRepo.Records.DefaultLessonDuration.SetAsync(48);
            await _configRepo.SaveAsync();

            Assert.AreEqual(48, await _configRepo.Records.DefaultLessonDuration.GetAsync());
        }

        [Test]
        public void Should_add_and_save_config_record()
        {
            _configRepo.Records.DefaultLessonDuration.SetAndSave(38);

            Assert.AreEqual(38, _configRepo.Records.DefaultLessonDuration.Get());
        }

        [Test]
        public async Task Should_add_and_save_config_record_async()
        {
            await _configRepo.Records.DefaultLessonDuration.SetAndSaveAsync(35);

            Assert.AreEqual(35, await _configRepo.Records.DefaultLessonDuration.GetAsync());
        }
    }
}
