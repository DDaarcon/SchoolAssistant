namespace SchoolAssistant.Infrastructure.Interfaces.Permissions
{
    public interface IPermissions
    {
        public bool CanAccessConfiguration { get; set; }
        public bool CanViewAllClassesData { get; set; }
    }
}
