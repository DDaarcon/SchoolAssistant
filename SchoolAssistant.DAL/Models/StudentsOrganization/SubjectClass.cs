using SchoolAssistant.DAL.Models.Subjects;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolAssistant.DAL.Models.StudentsOrganization
{
    public class SubjectClass : SchoolClass
    {
        public long SubjectId { get; set; }
        [ForeignKey("SubjectId")]
        public virtual Subject Subject { get; set; } = null!;
    }
}
