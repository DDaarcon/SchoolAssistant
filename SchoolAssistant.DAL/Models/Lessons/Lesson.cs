using SchoolAssistant.DAL.Models.Rooms;
using SchoolAssistant.DAL.Models.Shared;
using SchoolAssistant.DAL.Models.Staff;
using SchoolAssistant.DAL.Models.StudentsOrganization;
using SchoolAssistant.DAL.Models.Subjects;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolAssistant.DAL.Models.Lessons
{
    public class Lesson : SemesterDbEntity
    {
        public string CronPeriodicity { get; set; }

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

        public long RoomId { get; set; }
        [ForeignKey(nameof(RoomId))]
        public virtual Room Room { get; set; } = null!;
    }
}
