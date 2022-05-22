using SchoolAssistant.DAL.Interfaces;
using SchoolAssistant.DAL.Models.AppStructure;
using SchoolAssistant.DAL.Models.LinkingTables;
using SchoolAssistant.DAL.Models.Shared;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolAssistant.DAL.Models.StudentsParents
{
    public class Parent : DbEntity
    {
        public virtual ICollection<ParentToChild> Children { get; set; } = new List<ParentToChild>();
        public virtual User? User { get; set; }

        [NotMapped]
        public IPerson? Info
        {
            get
            {
                var linking = Children.FirstOrDefault();
                if (linking is null) return null;

                var record = linking.ChildInfo;
                if (record is null) return null;

                return linking.IsSecondParent ? record.SecondParent : record.FirstParent;
            }
        }
    }
}
