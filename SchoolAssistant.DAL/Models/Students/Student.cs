using SchoolAssistant.DAL.Interfaces;
using SchoolAssistant.DAL.Models.Marks;
using SchoolAssistant.DAL.Models.Shared;
using SchoolAssistant.DAL.Models.StudentsOrganization;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolAssistant.DAL.Models.Students
{
    public class Student : SemesterDbEntity, IPerson
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public ICollection<Mark> Marks { get; set; } = new List<Mark>();

        public long? OrganizationalClassId { get; set; }
        [ForeignKey(nameof(OrganizationalClassId))]
        public virtual OrganizationalClass? OrganizationalClass { get; set; }

        public virtual ICollection<SubjectClass> SubjectClasses { get; set; } = new List<SubjectClass>();
    }
}
