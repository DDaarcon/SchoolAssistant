using SchoolAssistant.DAL.Models.LinkingTables;
using SchoolAssistant.DAL.Models.StudentsOrganization;
using SchoolAssistant.DAL.Models.Subjects;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolAssistant.DAL.Models.Staff
{
    public class Teacher : StaffPerson
    {
        protected virtual ICollection<TeacherToMainSubject> _mainSubjects { get; set; } = new List<TeacherToMainSubject>();
        protected virtual ICollection<TeacherToAdditionalSubject> _additionalSubjects { get; set; } = new List<TeacherToAdditionalSubject>();

        public long PupilsId { get; set; }
        public virtual OrganizationalClass Pupils { get; set; } = null!;

        [NotMapped]
        public IReadOnlyCollection<Subject> MainSubjects
        {
            get
            {
                return _mainSubjects.Select(x => x.Subject).ToList();
            }
        }
        [NotMapped]
        public IReadOnlyCollection<Subject> AdditionalSubjects
        {
            get
            {
                return _additionalSubjects.Select(x => x.Subject).ToList();
            }
        }

        public void AddMainSubject(Subject subject)
        {
            if (subject is null || _mainSubjects.Select(x => x.Subject).Contains(subject))
                return;

            _mainSubjects.Add(new TeacherToMainSubject
            {
                Subject = subject,
                Teacher = this
            });
        }

        public void AddAdditionalSubject(Subject subject)
        {
            if (subject is null || _additionalSubjects.Select(x => x.Subject).Contains(subject))
                return;

            _additionalSubjects.Add(new TeacherToAdditionalSubject
            {
                Subject = subject,
                Teacher = this
            });
        }

        public void RemoveMainSubject(Subject subject)
        {
            if (subject is null) return;

            var relationToDelete = _mainSubjects.FirstOrDefault(x => x.Subject == subject);

            if (relationToDelete is not null)
                _mainSubjects.Remove(relationToDelete);
        }

        public void RemoveAdditionalSubject(Subject subject)
        {
            if (subject is null) return;

            var relationToDelete = _additionalSubjects.FirstOrDefault(x => x.Subject == subject);

            if (relationToDelete is not null)
                _additionalSubjects.Remove(relationToDelete);
        }
    }
}
