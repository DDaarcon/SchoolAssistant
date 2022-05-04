using Microsoft.Extensions.DependencyInjection;
using SchoolAssistant.DAL.Attributes;
using SchoolAssistant.DAL.Help.Application;

namespace SchoolAssistant.DAL.Repositories
{
    public interface IAppConfigRepository
    {
        ConfigRecordOperationsPrimitive<int> ScheduleArrangerCellHeight { get; }
        ConfigRecordOperationsPrimitive<int> ScheduleArrangerCellDuration { get; }
        ConfigRecordOperationsPrimitive<int> DefaultLessonDuration { get; }

        ConfigRecordOperations<string> CustomConfig(string key);
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

            var helper = new ConfigRecordOperationsHelper(() => _context, this);

            helper.SetUpProperties();
        }

        [AppConfigKey("defaultLessonDuration")]
        public ConfigRecordOperationsPrimitive<int> DefaultLessonDuration { get; private set; } = null!;
        [AppConfigKey("scheduleArrangerCellDuration")]
        public ConfigRecordOperationsPrimitive<int> ScheduleArrangerCellDuration { get; private set; } = null!;
        [AppConfigKey("scheduleArrangerCellHeight")]
        public ConfigRecordOperationsPrimitive<int> ScheduleArrangerCellHeight { get; private set; } = null!;



        public ConfigRecordOperations<string> CustomConfig(string key) => 
            new ConfigRecordOperations<string>(key, () => _context, v => v, v => v);

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
    }
}
