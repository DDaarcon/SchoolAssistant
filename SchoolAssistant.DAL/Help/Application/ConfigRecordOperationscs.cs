using Microsoft.EntityFrameworkCore;
using SchoolAssistant.DAL.Models.Application;

namespace SchoolAssistant.DAL.Help.Application
{
    public class ConfigRecordOperationsDecimal : ConfigRecordOperationsNullable<decimal>
    {
        public ConfigRecordOperationsDecimal(string key, Func<SADbContext> getContext) : base(key, getContext) { }
        protected override Func<string?, decimal?> _ToType { get => v => v is not null ? int.Parse(v) : null; }
        protected override Func<decimal?, string?> _FromType { get => v => v.ToString(); }
    }
    public class ConfigRecordOperationsInt : ConfigRecordOperationsNullable<int>
    {
        public ConfigRecordOperationsInt(string key, Func<SADbContext> getContext) : base(key, getContext) { }
        protected override Func<string?, int?> _ToType { get => v => v is not null ? int.Parse(v) : null; }
        protected override Func<int?, string?> _FromType { get => v => v.ToString(); }
    }
    public class ConfigRecordOperationsString : ConfigRecordOperations<string?>
    {
        public ConfigRecordOperationsString(string key, Func<SADbContext> getContext) : base(key, getContext) { }
        protected override Func<string?, string?> _ToType { get => v => v; }
        protected override Func<string?, string?> _FromType { get => v => v; }
    }





    public abstract class ConfigRecordOperationsNullable<T> : ConfigRecordOperations<Nullable<T>>
        where T : struct
    {
        protected ConfigRecordOperationsNullable(string key, Func<SADbContext> getContext) : base(key, getContext) { }
    }

    public abstract class ConfigRecordOperations<T>
    {
        readonly string _key;
        readonly Func<SADbContext> _getContext;

        SADbContext _Context => _getContext();
        DbSet<AppConfig> _Repo => _Context.Set<AppConfig>();

        protected abstract Func<string?, T?> _ToType { get; }
        protected abstract Func<T?, string?> _FromType { get; }

        public ConfigRecordOperations(
            string key,
            Func<SADbContext> getContext)
        {
            _key = key;
            _getContext = getContext;
        }

        public T? Get() => _ToType(_Repo.FirstOrDefault(x => x.Key == _key)?.Value);

        public async Task<T?> GetAsync() => _ToType((await _Repo.FirstOrDefaultAsync(x => x.Key == _key))?.Value);

        public void Set(T value)
        {
            var entry = _Repo.FirstOrDefault(x => x.Key == _key);
            entry ??= Add();

            entry.Value = _FromType(value);
        }

        public async Task SetAsync(T value)
        {
            var entry = await _Repo.FirstOrDefaultAsync(x => x.Key == _key);
            entry ??= Add();

            entry.Value = _FromType(value);
        }

        public void SetAndSave(T value)
        {
            Set(value);
            _Context.SaveChanges();
        }

        public async Task SetAndSaveAsync(T value)
        {
            await SetAsync(value);
            await _Context.SaveChangesAsync();
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
