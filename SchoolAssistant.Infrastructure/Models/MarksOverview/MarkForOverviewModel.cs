namespace SchoolAssistant.Infrastructure.Models.MarksOverview
{
    public class MarkForOverviewModel
    {
        public string Mark { get; set; } = null!;
        public string Subject { get; set; } = null!;
        public DateTime? Date { get; set; }
        public string? Issuer { get; set; }
        public string ColorHex { get; set; } = null!;
    }
}
