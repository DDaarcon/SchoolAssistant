using SchoolAssistant.DAL.Interfaces;
using SchoolAssistant.DAL.Models.Shared;

namespace SchoolAssistant.DAL.Models.Staff
{
    public abstract class StaffPerson : DbEntity, IPerson
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
