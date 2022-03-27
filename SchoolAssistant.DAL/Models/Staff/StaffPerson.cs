using SchoolAssistant.DAL.Interfaces;
using SchoolAssistant.DAL.Models.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAssistant.DAL.Models.Staff
{
    public abstract class StaffPerson : DbEntity, IPerson
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
