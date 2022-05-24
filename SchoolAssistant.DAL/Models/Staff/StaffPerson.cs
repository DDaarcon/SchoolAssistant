using SchoolAssistant.DAL.Interfaces;
using SchoolAssistant.DAL.Models.Shared;

namespace SchoolAssistant.DAL.Models.Staff
{
    public abstract class StaffPerson : DbEntity, IPerson
    {
        public string FirstName { get; set; } = null!;
        public string? SecondName { get; set; }
        public string LastName { get; set; } = null!;

        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
