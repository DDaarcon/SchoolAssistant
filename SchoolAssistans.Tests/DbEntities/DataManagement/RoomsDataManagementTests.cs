using NUnit.Framework;
using SchoolAssistant.DAL.Models.Rooms;
using SchoolAssistant.DAL.Repositories;
using SchoolAssistant.Infrastructure.Models.DataManagement.Rooms;
using SchoolAssistant.Logic.DataManagement.Rooms;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolAssistans.Tests.DbEntities.DataManagement
{
    public class RoomsDataManagementTests : BaseDbEntitiesTests
    {
        private IRoomDataManagementService _dataManagementService = null!;
        private IModifyRoomFromJsonService _modifyFromJsonService = null!;
        private IAppConfigRepository _configRepo = null!;

        private IRepository<Room> _roomRepo = null!;

        private Room _room = null!;

        protected override async Task CleanDataAfterEveryTestAsync()
        {
            await TestDatabase.ClearDataAsync<Room>();
        }

        protected override async Task SetupDataForEveryTestAsync()
        {
            _room = await FakeData.Room(_roomRepo);
        }

        protected override void SetupServices()
        {
            _roomRepo = new Repository<Room>(_Context, null);
            _configRepo = new AppConfigRepository(_Context, null);

            _modifyFromJsonService = new ModifyRoomFromJsonService(_roomRepo);
            _dataManagementService = new RoomDataManagementService(_modifyFromJsonService, _roomRepo, _configRepo);
        }

        private RoomDetailsJson _SampleDetailsJson => new RoomDetailsJson
        {
            name = "Sala informatyczna",
            number = 4,
            floor = 1
        };



        [Test]
        public async Task Should_fetch_entries()
        {
            var rooms = new List<Room> { _room };
            for (int i = 0; i < 10; i++)
                rooms.Add(await FakeData.Room(_roomRepo));

            var res = await _dataManagementService.GetEntriesJsonAsync();

            Assert.IsNotNull(res);

            foreach (var roomEntry in res)
            {
                Assert.IsTrue(rooms.Any(x =>
                    x.DisplayName == roomEntry.name
                    && x.Floor == roomEntry.floor
                    && x.Id == roomEntry.id));
            }
        }


        [Test]
        public async Task Should_fetch_modification_data()
        {
            await _configRepo.DefaultRoomName.SetAndSaveAsync("Sala lekcyjna");
            var res = await _dataManagementService.GetModificationDataJsonAsync(_room.Id);

            Assert.IsNotNull(res);
            Assert.IsNotNull(res!.data);

            Assert.AreEqual(res.defaultName, "Sala lekcyjna");
            Assert.AreEqual(res.data.id, _room.Id);
            Assert.AreEqual(res.data.name, _room.Name);
            Assert.AreEqual(res.data.floor, _room.Floor);
            Assert.AreEqual(res.data.number, _room.Number);
        }



        [Test]
        public async Task Should_add_new_room()
        {
            var model = _SampleDetailsJson;

            var res = await _dataManagementService.CreateOrUpdateAsync(model);

            AssertResponseSuccess(res);

            var list = _roomRepo.AsList();

            Assert.IsTrue(await _roomRepo.ExistsAsync(x =>
                x.Name == model.name
                && x.Number == model.number
                && x.Floor == model.floor));
        }

        [Test]
        public async Task Should_modify_room()
        {
            var model = _SampleDetailsJson;
            model.id = _room.Id;

            var res = await _dataManagementService.CreateOrUpdateAsync(model);

            AssertResponseSuccess(res);

            Assert.IsTrue(await _roomRepo.ExistsAsync(x =>
                x.Id == model.id
                && x.Name == model.name
                && x.Number == model.number
                && x.Floor == model.floor));
        }



        #region Fails


        [Test]
        public async Task Should_fail_name_is_null()
        {
            var model = _SampleDetailsJson;
            model.name = null!;

            var res = await _dataManagementService.CreateOrUpdateAsync(model);

            AssertResponseFail(res);
        }

        [Test]
        public async Task Should_fail_name_is_empty()
        {
            var model = _SampleDetailsJson;
            model.name = "";

            var res = await _dataManagementService.CreateOrUpdateAsync(model);

            AssertResponseFail(res);
        }

        [Test]
        public async Task Should_fail_exists_with_same_name_and_number()
        {
            var model = _SampleDetailsJson;
            model.name = _room.Name;
            model.number = _room.Number;

            var res = await _dataManagementService.CreateOrUpdateAsync(model);

            AssertResponseFail(res);
        }

        [Test]
        public async Task Should_fail_model_is_null()
        {
            var res = await _dataManagementService.CreateOrUpdateAsync(null!);

            AssertResponseFail(res);
        }

        #endregion
    }
}
