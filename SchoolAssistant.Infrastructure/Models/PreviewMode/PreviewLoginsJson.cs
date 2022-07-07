namespace SchoolAssistant.Infrastructure.Models.PreviewMode
{
    public class PreviewLoginsJson
    {
        public string administratorUserName { get; set; } = null!;
        public string administratorPassword { get; set; } = null!;
        public string? teacherUserName { get; set; }
        public string? teacherPassword { get; set; }
        public string? studentUserName { get; set; }
        public string? studentPassword { get; set; }
    }
}
