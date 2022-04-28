using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace SchoolAssistant.DAL.ConfigurationSchemas.Help
{
    internal class DateOnlyConverter : ValueConverter<DateOnly, DateTime>
    {
        public DateOnlyConverter() : base(
            x => x.ToDateTime(TimeOnly.MinValue),
            x => DateOnly.FromDateTime(x))
        {
        }
    }
}
