namespace SchoolAssistant.DAL.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    internal class AppConfigKeyAttribute : Attribute
    {
        public string Key { get; }
        public AppConfigKeyAttribute(string key)
        {
            Key = key;
        }
    }
}
