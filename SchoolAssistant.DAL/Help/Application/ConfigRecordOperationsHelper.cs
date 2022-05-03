using SchoolAssistant.DAL.Attributes;
using System.Reflection;

namespace SchoolAssistant.DAL.Help.Application
{
    internal class ConfigRecordOperationsHelper
    {
        private readonly Func<SADbContext> _getContext;
        public ConfigRecordOperationsHelper(
            Func<SADbContext> getContext)
        {
            _getContext = getContext;
        }

        public bool IsPropertyValid(PropertyInfo property)
        {
            return property.PropertyType == typeof(ConfigRecordOperations)
                && property.GetCustomAttribute<AppConfigKeyAttribute>() is not null;
        }

        public ConfigRecordOperations? CreateForProperty(PropertyInfo property)
        {
            if (!IsPropertyValid(property)) return null;
            var attr = property.GetCustomAttribute<AppConfigKeyAttribute>()!;

            return new ConfigRecordOperations(attr.Key, _getContext);
        }
    }
}
