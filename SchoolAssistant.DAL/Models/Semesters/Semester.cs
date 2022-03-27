using SchoolAssistant.DAL.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAssistant.DAL.Models.Semesters
{
    public class Semester : DbEntity
    {
        public short Year { get; set; }

        public bool Current { get; set; }

    }
}
