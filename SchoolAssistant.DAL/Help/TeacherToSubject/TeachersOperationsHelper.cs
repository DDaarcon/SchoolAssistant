using SchoolAssistant.DAL.Models.LinkingTables;
using SchoolAssistant.DAL.Models.Staff;
using SchoolAssistant.DAL.Models.Subjects;

namespace SchoolAssistant.DAL.Help
{
    public class TeachersOperationsHelper : TeacherToSubjectOperationsHelper<Subject, Teacher>
    {
        public TeachersOperationsHelper(
            Subject thisObject,
            Func<ICollection<TeacherToMainSubject>> getMainLinkings,
            Func<ICollection<TeacherToAdditionalSubject>> getAdditionalLinkings) : base(thisObject, getMainLinkings, getAdditionalLinkings)
        {
        }

        protected override IEnumerable<Teacher> SelectMain => _MainLinkings.Select(x => x.Teacher);
        protected override IEnumerable<Teacher> SelectAdditional => _AdditionalLinkings.Select(x => x.Teacher);

        protected override TeacherToAdditionalSubject NewAdditionalLinking(Teacher related) => new TeacherToAdditionalSubject
        {
            TeacherId = related.Id,
            Subject = _this
        };
        protected override TeacherToAdditionalSubject NewAdditionalLinkingForNewlyCreated(Teacher related) => new TeacherToAdditionalSubject
        {
            Teacher = related,
            Subject = _this
        };
        protected override TeacherToMainSubject NewMainLinking(Teacher related) => new TeacherToMainSubject
        {
            TeacherId = related.Id,
            Subject = _this
        };
        protected override TeacherToMainSubject NewMainLinkingForNewlyCreated(Teacher related) => new TeacherToMainSubject
        {
            Teacher = related,
            Subject = _this
        };
    }
}
