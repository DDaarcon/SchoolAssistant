namespace SchoolAssistant.Infrastructure.Models.DataManagement.Rooms
{
    public class RoomDetailsJson
    {
        public long? id { get; set; }
        public string name { get; set; } = null!;
        public int floor { get; set; }
        public int? number { get; set; }
    }
}
