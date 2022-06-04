using AppConfigurationEFCore.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AppConfigurationEFCore
{
    public interface IAppConfiguration<TRecords>
        where TRecords : class, new()
    {
        TRecords Records { get; }
        RecordHandler<string> CustomConfig(string key);

        void Save();
        Task SaveAsync();
        void UseIndependentDbContext();
    }

    public class AppConfiguration<TDbContext, TRecords> : IAppConfiguration<TRecords>
        where TDbContext : DbContext
        where TRecords : class, new()
    {
        private TDbContext _context;
        private readonly IServiceScopeFactory? _scopeFactory;


        public AppConfiguration(
            TDbContext context,
            IServiceScopeFactory? scopeFactory,
            TRecords records)
        {
            _context = context;
            _scopeFactory = scopeFactory;

            Records = records;
        }


        public TRecords Records { get; init; }


        public RecordHandler<string> CustomConfig(string key) =>
            new RecordHandler<string>(key, () => _context, v => v, v => v);

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
            _context = _scopeFactory?.CreateScope().ServiceProvider.GetRequiredService<TDbContext>() ?? _context;
        }
    }
}
