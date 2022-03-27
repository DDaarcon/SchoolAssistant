using SchoolAssistant.DAL.Models.Subjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAssistant.DAL.Models.StudentsOrganization
{
    public class SubjectClass : SchoolClass
    {
        public long SubjectId { get; set; }
        [ForeignKey("SubjectId")]
        public virtual Subject Subject { get; set; } = null!;
    }
}
