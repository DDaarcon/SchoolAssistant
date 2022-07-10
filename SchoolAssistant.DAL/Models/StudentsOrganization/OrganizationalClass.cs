using SchoolAssistant.DAL.Models.Staff;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolAssistant.DAL.Models.StudentsOrganization
{
    [Table("OrganizationalClasses")]
    public class OrganizationalClass : SchoolClass
    {
        public int Grade { get; set; }
        public string? Distinction { get; set; }
        public string? Specialization { get; set; }

        public long? SupervisorId { get; set; }
        [ForeignKey("SupervisorId")]
        public virtual Teacher? Supervisor { get; set; }

        [NotMapped]
        public string Name => $"{Grade}{Distinction}";
    }
}
