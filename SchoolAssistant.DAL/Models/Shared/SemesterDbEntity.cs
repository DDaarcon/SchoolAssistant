using SchoolAssistant.DAL.Models.Semesters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAssistant.DAL.Models.Shared
{
    public class SemesterDbEntity : DbEntity
    {
        public long SemesterId { get; set; }
        [ForeignKey("SemesterId")]
        public virtual Semester Semester { get; set; } = null!;
    }
}
