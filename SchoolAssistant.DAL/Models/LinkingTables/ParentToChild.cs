using SchoolAssistant.DAL.Models.StudentsParents;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolAssistant.DAL.Models.LinkingTables
{
    public class ParentToChild
    {
        public bool IsSecondParent { get; set; }

        public long ChildInfoId { get; set; }
        [ForeignKey(nameof(ChildInfoId))]
        public virtual StudentRegisterRecord ChildInfo { get; set; } = null!;
    }
}
