using SchoolAssistant.DAL.Models.Staff;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolAssistant.DAL.Models.StudentsOrganization
{
    public class OrganizationalClass : SchoolClass
    {
        public long SupervisorId { get; set; }
        [ForeignKey("SupervisorId")]
        public virtual Teacher Supervisor { get; set; } = null!;
    }
}
