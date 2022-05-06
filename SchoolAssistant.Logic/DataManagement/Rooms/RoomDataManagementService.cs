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

        public Task<string?> GetDefaultNameAsync()
        {
            return _configRepo.DefaultRoomName.GetAsync();
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

        public async Task<RoomModificationDataJson?> GetModificationDataJsonAsync(long id)
        {
            var room = await _roomRepo.GetByIdAsync(id);
            if (room is null) return null;

            return new RoomModificationDataJson
            {
                defaultName = await GetDefaultNameAsync(),
                data = new RoomDetailsJson
                {
                    id = room.Id,
                    name = room.Name,
                    number = room.Number,
                    floor = room.Floor
                }
            };
        }
        public Task<ResponseJson> CreateOrUpdateAsync(RoomDetailsJson model)
        {
            return _modifyFromJsonService.CreateOrUpdateAsync(model);
        }
    }
}
