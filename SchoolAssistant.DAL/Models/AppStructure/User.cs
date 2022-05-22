using Microsoft.AspNetCore.Identity;
using SchoolAssistant.DAL.Enums;
using SchoolAssistant.DAL.Models.Staff;
using SchoolAssistant.DAL.Models.StudentsParents;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolAssistant.DAL.Models.AppStructure
{
    public class User : IdentityUser<long>
    {
        public UserType Type { get; set; }

        public long? StudentId { get; set; }
        [ForeignKey(nameof(StudentId))]
        public virtual StudentRegisterRecord? Student { get; set; }

        public long? TeacherId { get; set; }
        [ForeignKey(nameof(TeacherId))]
        public virtual Teacher? Teacher { get; set; }

        public long? ParentId { get; set; }
        [ForeignKey(nameof(ParentId))]
        public virtual Parent? Parent { get; set; }
    }
}
