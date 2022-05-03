using Microsoft.Extensions.DependencyInjection;
using SchoolAssistant.DAL.Attributes;
using SchoolAssistant.DAL.Help.Application;

namespace SchoolAssistant.DAL.Repositories
{
    public interface IAppConfigRepository
    {
        ConfigRecordOperations ScheduleArrangerCellHeight { get; }
        ConfigRecordOperations ScheduleArrangerCellDuration { get; }
        ConfigRecordOperations DefaultLessonDuration { get; }

        ConfigRecordOperations CustomConfig(string key);
        void Save();
        Task SaveAsync();
        void UseIndependentDbContext();
    }

    [Injectable]
    public class AppConfigRepository : IAppConfigRepository
    {
        private SADbContext _context;
        private readonly IServiceScopeFactory? _scopeFactory;


        public AppConfigRepository(
            SADbContext context,
            IServiceScopeFactory? scopeFactory)
        {
            _context = context;
            _scopeFactory = scopeFactory;

            SetupConfigRecords();
        }

        [AppConfigKey("defaultLessonDuration")]
        public ConfigRecordOperationsInt DefaultLessonDuration { get; private set; } = null!;
        [AppConfigKey("scheduleArrangerCellDuration")]
        public ConfigRecordOperationsInt ScheduleArrangerCellDuration { get; private set; } = null!;
        [AppConfigKey("scheduleArrangerCellHeight")]
        public ConfigRecordOperationsInt ScheduleArrangerCellHeight { get; private set; } = null!;



        public ConfigRecordOperationsString CustomConfig(string key) => new ConfigRecordOperationsString(key, () => _context);

        public void Save()
        {
            _context.SaveChanges();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void UseIndependentDbContext()
        {
            _context = _scopeFactory?.CreateScope().ServiceProvider.GetRequiredService<SADbContext>() ?? _context;
        }


        private void SetupConfigRecords()
        {
            var helper = new ConfigRecordOperationsHelper(() => _context);
            var properties = GetType().GetProperties()!;

            foreach (var property in properties)
            {
                if (helper.IsPropertyValid(property))
                    property.SetValue(this, helper.CreateForProperty.In);
            }
        }
    }
}
