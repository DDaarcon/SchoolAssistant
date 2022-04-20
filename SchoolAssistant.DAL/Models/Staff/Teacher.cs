using SchoolAssistant.DAL.Models.Lessons;
using SchoolAssistant.DAL.Models.LinkingTables;
using SchoolAssistant.DAL.Models.StudentsOrganization;
using SchoolAssistant.DAL.Models.Subjects;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolAssistant.DAL.Models.Staff
{
    public class Teacher : StaffPerson
    {
        public virtual ICollection<Subject> MainSubjects { get; set; } = new List<Subject>();
        protected virtual IEnumerable<TeacherToMainSubject> _mainSubjectsLinking { get; set; } = new List<TeacherToMainSubject>();
        public virtual ICollection<Subject> AdditionalSubjects { get; set; } = new List<Subject>();
        protected virtual IEnumerable<TeacherToAdditionalSubject> _additionalSubjectsLinking { get; set; } = new List<TeacherToAdditionalSubject>();

        public long PupilsId { get; set; }
        public virtual OrganizationalClass Pupils { get; set; } = null!;

        public virtual ICollection<PeriodicLesson> Schedule { get; set; } = new List<PeriodicLesson>();


        public void AddMainSubject(Subject? subject)
        {
            if (subject is null || MainSubjects.Contains(subject))
                return;

            MainSubjects.Add(subject);
        }

        public void AddAdditionalSubject(Subject? subject)
        {
            if (subject is null || AdditionalSubjects.Contains(subject))
                return;

            AdditionalSubjects.Add(subject);
        }

        public void RemoveMainSubject(Subject? subject)
        {
            if (subject is null) return;

            if (MainSubjects.Contains(subject))
                MainSubjects.Remove(subject);
        }

        public void RemoveAdditionalSubject(Subject? subject)
        {
            if (subject is null) return;

            if (AdditionalSubjects.Contains(subject))
                AdditionalSubjects.Remove(subject);
        }
    }
}
