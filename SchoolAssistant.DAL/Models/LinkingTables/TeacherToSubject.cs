using Microsoft.EntityFrameworkCore;
using SchoolAssistant.DAL.Models.Staff;
using SchoolAssistant.DAL.Models.Subjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
