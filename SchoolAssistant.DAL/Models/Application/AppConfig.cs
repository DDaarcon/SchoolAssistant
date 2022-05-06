using System.ComponentModel.DataAnnotations;

namespace SchoolAssistant.DAL.Models.Application
{
    public class AppConfig
    {
        [Key]
        public string Key { get; set; } = null!;
        public string? Value { get; set; }
    }
}
