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
        public SchoolYearDataSupplier()
        {
            Current = new()
            {
                Current = true,
                Year = GetYear()
            };
        }

        public SchoolYear Current { get; }

        private short GetYear()
        {
            var today = DateTime.Today;
            short year = (short)today.Year;

            if (today.Month <= 8)
                return (short)(year - 1);
            return year;
        }
    }
}
