using SchoolAssistant.DAL.Models.Shared;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolAssistant.DAL.Models.StudentsParents
{
    public class Parent : SchoolYearDbEntity
    {
        public bool IsSecondParent { get; set; }

        public long ChildInfoId { get; set; }
        [ForeignKey(nameof(ChildInfoId))]
        public StudentRegisterRecord ChildInfo { get; set; } = null!;
    }
}
