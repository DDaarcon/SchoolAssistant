using SchoolAssistant.DAL.Models.LinkingTables;
using SchoolAssistant.DAL.Models.Shared;
using SchoolAssistant.DAL.Models.Staff;

namespace SchoolAssistant.DAL.Models.Subjects
{
    public class Subject : DbEntity
    {
        public string Name { get; set; } = null!;

        public virtual ICollection<Teacher> MainTeachers { get; set; } = new List<Teacher>();
        protected virtual IEnumerable<TeacherToMainSubject> _mainTeachersLinking { get; set; } = new List<TeacherToMainSubject>();
        public virtual ICollection<Teacher> AdditionalTeachers { get; set; } = new List<Teacher>();
        protected virtual IEnumerable<TeacherToAdditionalSubject> _additionalTeachersLinking { get; set; } = new List<TeacherToAdditionalSubject>();
    }
}
