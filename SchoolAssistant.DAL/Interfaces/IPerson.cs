using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAssistant.DAL.Interfaces
{
    public interface IPerson
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
