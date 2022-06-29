using AppConfigurationEFCore.Configuration;

namespace SchoolAssistant.DAL.Help.AppConfiguration
{
    public class DaysOfWeekRecordHandlerRule : IRecordHandlerRule<IEnumerable<DayOfWeek>>
    {
        public string? FromType(IEnumerable<DayOfWeek>? en)
        {
            if (en is null) return null;

            return String.Join(' ', en.Select(x => (int)x));
        }

        public IEnumerable<DayOfWeek>? ToType(string? db)
        {
            if (db is null) return null;

            return db.ToCharArray()
                .Select<char, int?>(ch => int.TryParse(ch.ToString(), out int val) ? val : null)
                .Where(num => num.HasValue && Enum.IsDefined(typeof(DayOfWeek), num.Value))
                .Distinct()
                .Cast<DayOfWeek>();
        }
    }
}
