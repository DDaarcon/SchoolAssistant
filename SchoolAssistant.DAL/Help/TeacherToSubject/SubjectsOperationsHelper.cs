using SchoolAssistant.DAL.Models.LinkingTables;
using SchoolAssistant.DAL.Models.Staff;
using SchoolAssistant.DAL.Models.Subjects;

namespace SchoolAssistant.DAL.Help
{
    public class SubjectsOperationsHelper : TeacherToSubjectOperationsHelper<Teacher, Subject>
    {
        public SubjectsOperationsHelper(
            Teacher thisObject,
            Func<ICollection<TeacherToMainSubject>> getMainLinkings,
            Func<ICollection<TeacherToAdditionalSubject>> getAdditionalLinkings) : base(thisObject, getMainLinkings, getAdditionalLinkings)
        {
        }

        protected override IEnumerable<Subject> SelectMain => _MainLinkings.Select(x => x.Subject);
        protected override IEnumerable<Subject> SelectAdditional => _AdditionalLinkings.Select(x => x.Subject);

        protected override TeacherToAdditionalSubject NewAdditionalLinking(Subject related) => new TeacherToAdditionalSubject
        {
            SubjectId = related.Id,
            Teacher = _this
        };
        protected override TeacherToAdditionalSubject NewAdditionalLinkingForNewlyCreated(Subject related) => new TeacherToAdditionalSubject
        {
            Subject = related,
            Teacher = _this
        };
        protected override TeacherToMainSubject NewMainLinking(Subject related) => new TeacherToMainSubject
        {
            SubjectId = related.Id,
            Teacher = _this
        };
        protected override TeacherToMainSubject NewMainLinkingForNewlyCreated(Subject related) => new TeacherToMainSubject
        {
            Subject = related,
            Teacher = _this
        };
    }
}
