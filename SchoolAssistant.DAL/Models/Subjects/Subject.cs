using SchoolAssistant.DAL.Help;
using SchoolAssistant.DAL.Models.LinkingTables;
using SchoolAssistant.DAL.Models.Shared;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolAssistant.DAL.Models.Subjects
{
    public class Subject : DbEntity
    {
        public string Name { get; set; } = null!;

        public virtual ICollection<TeacherToMainSubject> MainTeachers { get; set; } = new List<TeacherToMainSubject>();
        public virtual ICollection<TeacherToAdditionalSubject> AdditionalTeachers { get; set; } = new List<TeacherToAdditionalSubject>();



        [NotMapped]
        public TeachersOperationsHelper TeacherOperations { get; init; }

        public Subject()
        {
            TeacherOperations = new TeachersOperationsHelper(this, () => MainTeachers, () => AdditionalTeachers);
        }
    }
}
