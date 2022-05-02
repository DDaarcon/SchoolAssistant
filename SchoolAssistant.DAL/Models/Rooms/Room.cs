using SchoolAssistant.DAL.Models.Shared;

namespace SchoolAssistant.DAL.Models.Rooms
{
    public class Room : DbEntity
    {
        public string? Name { get; set; }
        public int Floor { get; set; }
        public int Number { get; set; }

    }
}
