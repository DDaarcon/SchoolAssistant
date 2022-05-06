using SchoolAssistant.DAL.Models.Shared;
using SchoolAssistant.DAL.Models.StudentsOrganization;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolAssistant.DAL.Models.Marks
{
    public class MarksOfClass : SchoolYearDbEntity
    {
        public string? Description { get; set; }
        public int? Weight { get; set; }

        public long? OrganizationalClassId { get; set; }
        [ForeignKey(nameof(OrganizationalClassId))]
        public virtual OrganizationalClass? OrganizationalClass { get; set; }

        public long? SubjectClassId { get; set; }
        [ForeignKey(nameof(SubjectClassId))]
        public virtual SubjectClass? SubjectClass { get; set; }

        public virtual ICollection<Mark> Marks { get; set; } = new List<Mark>();
    }
}
