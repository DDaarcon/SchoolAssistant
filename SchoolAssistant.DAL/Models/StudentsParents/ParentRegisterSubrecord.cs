using Microsoft.EntityFrameworkCore;
using SchoolAssistant.DAL.Interfaces;

namespace SchoolAssistant.DAL.Models.StudentsParents
{
    [Owned]
    public class ParentRegisterSubrecord : IPerson
    {
        public string FirstName { get; set; } = null!;
        public string? SecondName { get; set; } = null!;
        public string LastName { get; set; } = null!;

        public string PhoneNumber { get; set; } = null!;
        public string? Email { get; set; }

        public string Address { get; set; } = null!;
    }
}
