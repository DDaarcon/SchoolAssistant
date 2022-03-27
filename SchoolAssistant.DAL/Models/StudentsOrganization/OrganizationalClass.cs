using SchoolAssistant.DAL.Models.Staff;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAssistant.DAL.Models.StudentsOrganization
{
    public class OrganizationalClass : SchoolClass
    {
        public long SupervisorId { get; set; }
        [ForeignKey("SupervisorId")]
        public virtual Teacher Supervisor { get; set; } = null!;
    }
}
