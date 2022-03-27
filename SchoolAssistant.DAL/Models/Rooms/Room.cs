using SchoolAssistant.DAL.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAssistant.DAL.Models.Rooms
{
    public class Room : DbEntity
    {
        public string Name { get; set; }
        public int Floor { get; set; }
        public int Number { get; set; }

    }
}
