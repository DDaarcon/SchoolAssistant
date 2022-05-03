namespace SchoolAssistant.Infrastructure.Models.DataManagement.Rooms
{
    public class RoomModificationDataJson
    {
        public RoomDetailsJson data { get; set; } = null!;
        public string? defaultName { get; set; }
    }
}
