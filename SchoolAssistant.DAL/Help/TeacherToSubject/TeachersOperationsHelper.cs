using SchoolAssistant.DAL.Models.LinkingTables;
using SchoolAssistant.DAL.Models.Staff;
using SchoolAssistant.DAL.Models.Subjects;

namespace SchoolAssistant.DAL.Help
{
    public class TeachersOperationsHelper : TeacherToSubjectOperationsHelper<Subject, Teacher>
    {
        public TeachersOperationsHelper(
            Subject thisObject,
            ICollection<TeacherToMainSubject> mainLinkings,
            ICollection<TeacherToAdditionalSubject> additionalLinkings) : base(thisObject, mainLinkings, additionalLinkings)
        {
        }

        protected override IEnumerable<Teacher> SelectMain => _mainLinkings.Select(x => x.Teacher);
        protected override IEnumerable<Teacher> SelectAdditional => _additionalLinkings.Select(x => x.Teacher);

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
