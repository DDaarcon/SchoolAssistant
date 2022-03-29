using Microsoft.AspNetCore.Identity;
using SchoolAssistant.DAL.Enums;
using SchoolAssistant.DAL.Models.Staff;
using SchoolAssistant.DAL.Models.Students;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolAssistant.DAL.Models.AppStructure
{
    public class User : IdentityUser<long>
    {
        public UserType Type { get; set; }

        public long? StudentId { get; set; }
        [ForeignKey(nameof(StudentId))]
        public virtual Student? Student { get; set; }

        public long? TeacherId { get; set; }
        [ForeignKey(nameof(TeacherId))]
        public virtual Teacher? Teacher { get; set; }
    }
}
