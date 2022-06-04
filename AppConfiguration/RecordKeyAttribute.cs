namespace AppConfigurationEFCore.Configuration
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    internal class RecordKeyAttribute : Attribute
    {
        public string Key { get; }
        public RecordKeyAttribute(string key)
        {
            Key = key;
        }
    }
}
