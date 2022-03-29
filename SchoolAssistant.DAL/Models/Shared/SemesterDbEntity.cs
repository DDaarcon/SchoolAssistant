using SchoolAssistant.DAL.Models.Semesters;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolAssistant.DAL.Models.Shared
{
    public class SemesterDbEntity : DbEntity
    {
        public long SemesterId { get; set; }
        [ForeignKey("SemesterId")]
        public virtual Semester Semester { get; set; } = null!;
    }
}
