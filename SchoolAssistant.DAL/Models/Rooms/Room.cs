using SchoolAssistant.DAL.Models.Shared;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolAssistant.DAL.Models.Rooms
{
    public class Room : DbEntity
    {
        public string Name { get; set; } = null!;
        public int Floor { get; set; }
        public int? Number { get; set; }

        [NotMapped]
        public string DisplayName => $"{Name}{(Number.HasValue ? $" nr {Number.Value}" : "")}";
    }
}
