using SchoolAssistant.DAL.Models.Staff;
using SchoolAssistant.DAL.Models.Subjects;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolAssistant.DAL.Models.LinkingTables
{
    public abstract class TeacherToSubject
    {
        public long TeacherId { get; set; }
        [ForeignKey("TeacherId")]
        public virtual Teacher Teacher { get; set; } = null!;
        public long SubjectId { get; set; }
        [ForeignKey("SubjectId")]
        public virtual Subject Subject { get; set; } = null!;
    }

    public class TeacherToMainSubject : TeacherToSubject { }

    public class TeacherToAdditionalSubject : TeacherToSubject { }
}
