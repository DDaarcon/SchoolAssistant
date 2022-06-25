using SchoolAssistant.DAL.Models.Shared;
using SchoolAssistant.DAL.Models.Staff;
using SchoolAssistant.DAL.Models.StudentsParents;
using SchoolAssistant.DAL.Models.Subjects;
using SchoolAssistant.Infrastructure.Enums.Marks;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolAssistant.DAL.Models.Marks
{
    public class Mark : SchoolYearDbEntity
    {
        public long StudentId { get; set; }
        [ForeignKey(nameof(StudentId))]
        public virtual Student Student { get; set; } = null!;

        public long SubjectId { get; set; }
        [ForeignKey(nameof(SubjectId))]
        public virtual Subject Subject { get; set; } = null!;

        public long IssuerId { get; set; }
        [ForeignKey(nameof(IssuerId))]
        public virtual Teacher Issuer { get; set; } = null!;

        public long? CollectionId { get; set; }
        [ForeignKey(nameof(CollectionId))]
        public virtual MarksOfClass? Collection { get; set; }

        public string? Description { get; set; }

        public DateTime IssueDate { get; set; }

        public MarkValue Main { get; set; }
        public MarkPrefix? Prefix { get; set; }
        public int? Weight { get; set; }
    }
}
