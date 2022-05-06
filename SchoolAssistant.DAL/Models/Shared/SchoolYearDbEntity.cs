using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolAssistant.DAL.Models.Shared
{
    public class SchoolYearDbEntity : DbEntity
    {
        public long SchoolYearId { get; set; }
        [ForeignKey(nameof(SchoolYearId))]
        public virtual SchoolYears.SchoolYear SchoolYear { get; set; } = null!;
    }
}
