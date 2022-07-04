using Microsoft.Extensions.Configuration;

namespace SchoolAssistant.Logic.PreviewMode
{
    public interface IControlPreviewModeService
    {
        bool IsEnabled { get; }
    }

    [Injectable(ServiceLifetime.Singleton)]
    public class ControlPreviewModeService : IControlPreviewModeService
    {
        private readonly IConfiguration _configuration;

        public ControlPreviewModeService(
            IConfiguration configuration)
        {
            _configuration = configuration;
            IsEnabled = bool.TryParse(_configuration["PreviewMode"], out bool enabled) ? enabled : false;
        }

        public bool IsEnabled { get; private set; }
    }
}
