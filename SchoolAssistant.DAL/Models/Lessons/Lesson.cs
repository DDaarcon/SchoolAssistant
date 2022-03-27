using SchoolAssistant.DAL.Models.Shared;
using SchoolAssistant.DAL.Models.Staff;
using SchoolAssistant.DAL.Models.StudentsOrganization;
using SchoolAssistant.DAL.Models.Subjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAssistant.DAL.Models.Lessons
{
    public class Lesson : SemesterDbEntity
    {
        public long SubjectId { get; set; }
        [ForeignKey(nameof(SubjectId))]
        public virtual Subject Subject { get; set; } = null!;

        public long? ParticipatingOrganizationalClassId { get; set; }
        [ForeignKey(nameof(ParticipatingOrganizationalClassId))]
        public virtual OrganizationalClass? ParticipatingOrganizationalClass { get; set; }

        public long? ParticipatingSubjectClassId { get; set; }
        public virtual SubjectClass? ParticipatingSubjectClass { get; set; }

        public long LecturerId { get; set; }
        [ForeignKey(nameof(LecturerId))]
        public virtual Teacher Lecturer { get; set; } = null!;
    }
}
