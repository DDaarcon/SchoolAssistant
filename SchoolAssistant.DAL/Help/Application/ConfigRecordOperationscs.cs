using Microsoft.EntityFrameworkCore;
using SchoolAssistant.DAL.Models.Application;

namespace SchoolAssistant.DAL.Help.Application
{
    public class ConfigRecordOperations
    {
        readonly string _key;
        readonly Func<SADbContext> _getContext;

        private SADbContext _Context => _getContext();
        private DbSet<AppConfig> _Repo => _Context.Set<AppConfig>();

        public ConfigRecordOperations(
            string key,
            Func<SADbContext> getContext)
        {
            _key = key;
            _getContext = getContext;
        }

        public string? Get() => _Repo.FirstOrDefault(x => x.Key == _key)?.Value;

        public async Task<string?> GetAsync() => (await _Repo.FirstOrDefaultAsync(x => x.Key == _key))?.Value;


        public void Set(string value)
        {
            var entry = _Repo.FirstOrDefault(x => x.Key == _key);
            entry ??= Add();

            entry.Value = value;
        }

        public async Task SetAsync(string value)
        {
            var entry = await _Repo.FirstOrDefaultAsync(x => x.Key == _key);
            entry ??= Add();

            entry.Value = value;
        }

        public void SetAndSave(string value)
        {
            Set(value);
            _Context.SaveChanges();
        }

        public async Task SetAndSaveAsync(string value)
        {
            await SetAsync(value);
            await _Context.SaveChangesAsync();
        }

        public bool SetIfEmpty(string value)
        {
            if (_Repo.Any(x => x.Key == _key && !String.IsNullOrEmpty(value))) return false;
            Set(value);
            return true;
        }

        public async Task<bool> SetIfEmptyAsync(string value)
        {
            if (await _Repo.AnyAsync(x => x.Key == _key && !String.IsNullOrEmpty(value))) return false;
            await SetAsync(value);
            return true;
        }

        private AppConfig Add()
        {
            var entry = new AppConfig
            {
                Key = _key
            };
            _Repo.Add(entry);
            return entry;
        }
    }
}
