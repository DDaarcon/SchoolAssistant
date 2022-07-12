using SchoolAssistant.DAL.Models.SchoolYears;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAssistant.Logic.PreviewMode.ResetDatabaseSupport
{
    public interface ISchoolYearDataSupplier
    {
        public SchoolYear Current { get; }
    }

    [Injectable]
    public class SchoolYearDataSupplier : ISchoolYearDataSupplier
    {
        public SchoolYear Current { get; } = new()
        {
            Current = true,
            Year = DateTime.Today.Year
        };
    }
}
