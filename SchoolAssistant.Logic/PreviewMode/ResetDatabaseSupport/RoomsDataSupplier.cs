using SchoolAssistant.DAL.Models.Rooms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAssistant.Logic.PreviewMode.ResetDatabaseSupport
{
    public interface IRoomsDataSupplier
    {
        Room[] All { get; }
        Room Room1 { get; }
        Room Room2 { get; }
        Room Room3 { get; }
        Room Room4 { get; }
        Room Room5 { get; }
        Room Room6 { get; }
        Room Gym1 { get; }
        Room Gym2 { get; }
        Room ItRoom1 { get; }
        Room ItRoom2 { get; }
    }

    [Injectable]
    public class RoomsDataSupplier : IRoomsDataSupplier
    {
        public Room[] All { get; }


        public Room Room1 { get; } = new()
        {
            Name = "Sala",
            Floor = 0,
            Number = 1
        };
        public Room Room2 { get; } = new()
        {
            Name = "Sala",
            Floor = 0,
            Number = 2
        };
        public Room Room3 { get; } = new()
        {
            Name = "Sala",
            Floor = 0,
            Number = 3
        };
        public Room Room4 { get; } = new()
        {
            Name = "Sala",
            Floor = 0,
            Number = 4
        };
        public Room Room5 { get; } = new()
        {
            Name = "Sala",
            Floor = 1,
            Number = 5
        };
        public Room Room6 { get; } = new()
        {
            Name = "Sala",
            Floor = 1,
            Number = 6
        };

        public Room Gym1 { get; } = new()
        {
            Name = "Sala gimnastyczna",
            Floor = 0,
            Number = 1
        };
        public Room Gym2 { get; } = new()
        {
            Name = "Sala gimnastyczna",
            Floor = 0,
            Number = 2
        };
        
        public Room ItRoom1 { get; } = new()
        {
            Name = "Sala informatyczna",
            Floor = 1,
            Number = 1
        };
        public Room ItRoom2 { get; } = new()
        {
            Name = "Sala informatyczna",
            Floor = 1,
            Number = 2
        };


        public RoomsDataSupplier()
        {
            All = new[]
            {
                Room1,
                Room2,
                Room3,
                Room4,
                Room5,
                Room6,
                Gym1,
                Gym2,
                ItRoom1,
                ItRoom2
            };
        }
    }
}
