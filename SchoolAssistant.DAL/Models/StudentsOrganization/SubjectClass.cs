using SchoolAssistant.DAL.Models.Staff;
using SchoolAssistant.DAL.Models.Subjects;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolAssistant.DAL.Models.StudentsOrganization
{
    public class SubjectClass : SchoolClass
    {
        public long SubjectId { get; set; }
        [ForeignKey(nameof(SubjectId))]
        public virtual Subject Subject { get; set; } = null!;

        public long? TeacherId { get; set; }
        [ForeignKey(nameof(TeacherId))]
        public virtual Teacher? Teacher { get; set; }

        public long? OrganizationalClassId { get; set; }
        [ForeignKey(nameof(OrganizationalClassId))]
        public virtual OrganizationalClass? OrganizationalClass { get; set; }
    }
}
