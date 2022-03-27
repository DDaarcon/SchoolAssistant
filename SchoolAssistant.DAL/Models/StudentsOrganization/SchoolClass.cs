using SchoolAssistant.DAL.Models.Shared;
using SchoolAssistant.DAL.Models.Students;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAssistant.DAL.Models.StudentsOrganization
{
    public abstract class SchoolClass : SemesterDbEntity
    {
        public virtual ICollection<Student> Students { get; set; } = new List<Student>();
    }
}
