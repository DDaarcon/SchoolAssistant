namespace AppConfigurationEFCore.Configuration
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class RecordKeyAttribute : Attribute
    {
        public string Key { get; }
        public RecordKeyAttribute(string key)
        {
            Key = key;
        }
    }
}
