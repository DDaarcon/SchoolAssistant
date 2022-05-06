using SchoolAssistant.DAL.Models.Shared;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolAssistant.DAL.Models.SchoolYears
{
    [Table("SchoolYears")]
    public class SchoolYear : DbEntity
    {
        /// <summary> Year of start of the school year </summary>
        public short Year { get; set; }

        public bool Current { get; set; }

    }
}
