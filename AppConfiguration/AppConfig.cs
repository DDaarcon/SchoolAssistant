using System.ComponentModel.DataAnnotations;

namespace AppConfigurationEFCore.Entities
{
    public class AppConfig
    {
        [Key]
        public string Key { get; set; } = null!;
        public string? Value { get; set; }
    }
}
