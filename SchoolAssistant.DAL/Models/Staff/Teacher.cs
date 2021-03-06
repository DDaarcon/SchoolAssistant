using SchoolAssistant.DAL.Help;
using SchoolAssistant.DAL.Interfaces;
using SchoolAssistant.DAL.Models.AppStructure;
using SchoolAssistant.DAL.Models.Lessons;
using SchoolAssistant.DAL.Models.LinkingTables;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolAssistant.DAL.Models.Staff
{
    [Table("Teachers")]
    public class Teacher : StaffPerson, IHasUser
    {
        public virtual ICollection<TeacherToMainSubject> MainSubjects { get; set; } = new List<TeacherToMainSubject>();
        public virtual ICollection<TeacherToAdditionalSubject> AdditionalSubjects { get; set; } = new List<TeacherToAdditionalSubject>();


        public virtual ICollection<PeriodicLesson> Schedule { get; set; } = new List<PeriodicLesson>();

        public virtual User? User { get; set; }


        [NotMapped]
        public SubjectsOperationsHelper SubjectOperations { get; }

        public Teacher()
        {
            SubjectOperations = new SubjectsOperationsHelper(this, () => MainSubjects, () => AdditionalSubjects);
        }
    }
}
