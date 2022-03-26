﻿using SchoolAssistant.DAL.Enums;
using SchoolAssistant.DAL.Models.Shared;
using SchoolAssistant.DAL.Models.Staff;
using SchoolAssistant.DAL.Models.Students;
using SchoolAssistant.DAL.Models.Subjects;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolAssistant.DAL.Models.Marks
{
    public class Mark : SemesterDbEntity
    {
        public long StudentId { get; set; }
        [ForeignKey("StudentId")]
        public virtual Student Student { get; set; } = null!;

        public long SubjectId { get; set; }
        [ForeignKey("SubjectId")]
        public virtual Subject Subject { get; set; } = null!;

        public long IssuerId { get; set; }
        [ForeignKey("IssuerId")]
        public virtual Teacher Issuer { get; set; } = null!;

        public DateTime IssueDate { get; set; }

        public MainMark Main { get; set; }
        public MarkPrefix? Prefix { get; set; }
        public int? Weight { get; set; }
    }
}
