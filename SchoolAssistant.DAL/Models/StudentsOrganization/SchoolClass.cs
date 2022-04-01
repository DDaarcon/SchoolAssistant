using SchoolAssistant.DAL.Models.Shared;
using SchoolAssistant.DAL.Models.StudentsParents;

namespace SchoolAssistant.DAL.Models.StudentsOrganization
{
    public abstract class SchoolClass : SemesterDbEntity
    {
        public virtual ICollection<Student> Students { get; set; } = new List<Student>();
    }
}
