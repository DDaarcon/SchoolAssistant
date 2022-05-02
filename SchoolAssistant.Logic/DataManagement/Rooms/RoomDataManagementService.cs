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
        Task<string> GetDefaultNameAsync();
    }

    [Injectable]
    public class RoomDataManagementService : IRoomDataManagementService
    {
        private readonly IModifyRoomFromJsonService _modifyFromJsonService;
        private readonly IRepository<Room> _roomRepo;

        public RoomDataManagementService(
            IModifyRoomFromJsonService modifyRoomFromJsonService,
            IRepository<Room> roomRepo)
        {
            _modifyFromJsonService = modifyRoomFromJsonService;
            _roomRepo = roomRepo;
        }

        public Task<string> GetDefaultNameAsync()
        {
            throw new NotImplementedException();
        }

        public Task<RoomListEntryJson[]> GetEntriesJsonAsync()
        {
            throw new NotImplementedException();
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
