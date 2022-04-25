namespace SchoolAssistant.Infrastructure.Models.DataManagement.Students
{
    public class ParentRegisterSubrecordDetailsJson
    {
        public string firstName { get; set; } = null!;
        public string? secondName { get; set; }
        public string lastName { get; set; } = null!;
        public string phoneNumber { get; set; } = null!;
        public string? email { get; set; }
        public string address { get; set; } = null!;
    }
}
