using SchoolAssistant.DAL.Models.LinkingTables;
using SchoolAssistant.DAL.Models.Shared;
using SchoolAssistant.DAL.Models.Staff;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAssistant.DAL.Models.Subjects
{
    public class Subject : DbEntity 
    {
        public string Name { get; set; }
    }
}
