using SchoolAssistant.DAL.Interfaces;
using SchoolAssistant.DAL.Models.AppStructure;
using SchoolAssistant.DAL.Models.Shared;

namespace SchoolAssistant.DAL.Models.StudentsParents
{
    public class StudentRegisterRecord : DbEntity, IPerson, IHasUser
    {
        public string FirstName { get; set; } = null!;
        public string? SecondName { get; set; } = null!;
        public string LastName { get; set; } = null!;

        public DateOnly DateOfBirth { get; set; }
        public string PlaceOfBirth { get; set; } = null!;

        /// <summary> e.g. PESEL, ID (card) number </summary>
        public string PersonalID { get; set; } = null!;

        public string Address { get; set; } = null!;
        public string? Email { get; set; }

        public ParentRegisterSubrecord FirstParent { get; set; } = null!;
        public ParentRegisterSubrecord? SecondParent { get; set; }

        public virtual ICollection<Student> StudentInstances { get; set; } = new List<Student>();

        public virtual User? User { get; set; }
    }
}
