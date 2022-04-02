using SchoolAssistant.DAL.Models.Lessons;
using SchoolAssistant.DAL.Models.Marks;
using SchoolAssistant.DAL.Models.Shared;
using SchoolAssistant.DAL.Models.StudentsParents;

namespace SchoolAssistant.DAL.Models.StudentsOrganization
{
    public abstract class SchoolClass : SemesterDbEntity
    {
        public virtual ICollection<Student> Students { get; set; } = new List<Student>();
        public virtual ICollection<MarksOfClass> Marks { get; set; } = new List<MarksOfClass>();
        public virtual ICollection<PeriodicLesson> Schedule { get; set; } = new List<PeriodicLesson>();
    }
}
