using SchoolAssistant.Infrastructure.Models.Shared.Json;

namespace SchoolAssistant.Infrastructure.Models.DataManagement.Rooms
{
    public class RoomListEntryJson : ListEntryJson
    {
        public string name { get; set; } = null!;
        public int floor { get; set; }
    }
}
