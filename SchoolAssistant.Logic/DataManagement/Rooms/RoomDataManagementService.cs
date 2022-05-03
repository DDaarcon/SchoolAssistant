using Microsoft.EntityFrameworkCore;
using SchoolAssistant.DAL.Models.Rooms;
using SchoolAssistant.DAL.Repositories;
using SchoolAssistant.Infrastructure.Models.DataManagement.Rooms;
using SchoolAssistant.Infrastructure.Models.Shared.Json;

namespace SchoolAssistant.Logic.DataManagement.Rooms
{
    public interface IRoomDataManagementService
    {
        Task<ResponseJson> CreateOrUpdateAsync(RoomDetailsJson model);
        Task<RoomListEntryJson[]> GetEntriesJsonAsync();
        Task<RoomModificationDataJson?> GetModificationDataJsonAsync(long id);
        Task<string?> GetDefaultNameAsync();
    }

    [Injectable]
    public class RoomDataManagementService : IRoomDataManagementService
    {
        private readonly IModifyRoomFromJsonService _modifyFromJsonService;
        private readonly IRepository<Room> _roomRepo;
        private readonly IAppConfigRepository _configRepo;

        public RoomDataManagementService(
            IModifyRoomFromJsonService modifyRoomFromJsonService,
            IRepository<Room> roomRepo,
            IAppConfigRepository configRepo)
        {
            _modifyFromJsonService = modifyRoomFromJsonService;
            _roomRepo = roomRepo;
            _configRepo = configRepo;
        }

        public async Task<string?> GetDefaultNameAsync()
        {
            var name = await _configRepo.DefaultRoomName.GetAsync();
            return name;
        }

        public Task<RoomListEntryJson[]> GetEntriesJsonAsync()
        {
            return _roomRepo.AsQueryable()
                .Select(x => new RoomListEntryJson
                {
                    id = x.Id,
                    name = x.DisplayName,
                    floor = x.Floor
                }).ToArrayAsync();
        }

        public Task<RoomModificationDataJson?> GetModificationDataJsonAsync(long id)
        {
            throw new NotImplementedException();
        }
        public Task<ResponseJson> CreateOrUpdateAsync(RoomDetailsJson model)
        {
            throw new NotImplementedException();
        }
    }
}
