using SchoolAssistant.DAL.Models.Rooms;
using SchoolAssistant.DAL.Repositories;
using SchoolAssistant.Infrastructure.Models.DataManagement.Rooms;
using SchoolAssistant.Infrastructure.Models.Shared.Json;

namespace SchoolAssistant.Logic.DataManagement.Rooms
{
    public interface IModifyRoomFromJsonService
    {
        Task<ResponseJson> CreateOrUpdateAsync(RoomDetailsJson model);
    }

    [Injectable]
    public class ModifyRoomFromJsonService : IModifyRoomFromJsonService
    {
        private readonly IRepository<Room> _roomRepo;

        public ModifyRoomFromJsonService(
            IRepository<Room> roomRepo)
        {
            _roomRepo = roomRepo;
        }

        public Task<ResponseJson> CreateOrUpdateAsync(RoomDetailsJson model)
        {
            throw new NotImplementedException();
        }
    }
}
