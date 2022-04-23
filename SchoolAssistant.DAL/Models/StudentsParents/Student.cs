using SchoolAssistant.DAL.Models.Marks;
using SchoolAssistant.DAL.Models.Shared;
using SchoolAssistant.DAL.Models.StudentsOrganization;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolAssistant.DAL.Models.StudentsParents
{
    public class Student : SchoolYearDbEntity
    {
        public int NumerInJurnal { get; set; }

        public long InfoId { get; set; }
        [ForeignKey(nameof(InfoId))]
        public virtual StudentRegisterRecord Info { get; set; } = null!;

        public ICollection<Mark> Marks { get; set; } = new List<Mark>();

        public long? OrganizationalClassId { get; set; }
        [ForeignKey(nameof(OrganizationalClassId))]
        public virtual OrganizationalClass? OrganizationalClass { get; set; }

        public virtual ICollection<SubjectClass> SubjectClasses { get; set; } = new List<SubjectClass>();
    }
}
