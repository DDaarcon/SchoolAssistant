using SchoolAssistant.DAL.Models.LinkingTables;
using SchoolAssistant.DAL.Models.Shared;

namespace SchoolAssistant.DAL.Models.StudentsParents
{
    public class Parent : DbEntity
    {
        public virtual ICollection<ParentToChild> Children { get; set; } = new List<ParentToChild>();
    }
}
