using SchoolAssistant.DAL.Help;
using SchoolAssistant.DAL.Models.Lessons;
using SchoolAssistant.DAL.Models.LinkingTables;
using SchoolAssistant.DAL.Models.StudentsOrganization;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolAssistant.DAL.Models.Staff
{
    public class Teacher : StaffPerson
    {
        public virtual ICollection<TeacherToMainSubject> MainSubjects { get; set; } = new List<TeacherToMainSubject>();
        public virtual ICollection<TeacherToAdditionalSubject> AdditionalSubjects { get; set; } = new List<TeacherToAdditionalSubject>();

        public long PupilsId { get; set; }
        public virtual OrganizationalClass Pupils { get; set; } = null!;

        public virtual ICollection<PeriodicLesson> Schedule { get; set; } = new List<PeriodicLesson>();


        [NotMapped]
        public SubjectsOperationsHelper SubjectOperations { get; init; }

        public Teacher()
        {
            SubjectOperations = new SubjectsOperationsHelper(this, () => MainSubjects, () => AdditionalSubjects);
        }
    }
}
