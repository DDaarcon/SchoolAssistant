using AppConfigurationEFCore.Entities;
using Microsoft.EntityFrameworkCore;

namespace AppConfigurationEFCore.Configuration
{
    public class PrimitiveRecordHandler<T> : RecordHandler<T?>
        where T : struct
    {
        public PrimitiveRecordHandler(string key, Func<DbContext> getContext, Func<string?, T?> toType, Func<T?, string?> fromType) : base(key, getContext, toType, fromType)
        {
        }
    }

    public class RecordHandler<T>
    {
        readonly string _key;
        readonly Func<DbContext> _getContext;
        readonly Func<string?, T?> _toType;
        readonly Func<T?, string?> _fromType;

        DbContext _Context => _getContext();
        DbSet<AppConfig> _Repo => _Context.Set<AppConfig>();


        public RecordHandler(
            string key,
            Func<DbContext> getContext,
            Func<string?, T?> toType,
            Func<T?, string?> fromType)
        {
            _key = key;
            _getContext = getContext;
            _toType = toType;
            _fromType = fromType;
        }

        public T? Get() => _toType(_Repo.FirstOrDefault(x => x.Key == _key)?.Value);

        public async Task<T?> GetAsync() => _toType((await _Repo.FirstOrDefaultAsync(x => x.Key == _key))?.Value);

        public void Set(T? value)
        {
            var entry = _Repo.FirstOrDefault(x => x.Key == _key);
            entry ??= Add();

            entry.Value = _fromType(value);
        }

        public async Task SetAsync(T? value)
        {
            var entry = await _Repo.FirstOrDefaultAsync(x => x.Key == _key);
            entry ??= Add();

            entry.Value = _fromType(value);
        }

        public void SetAndSave(T? value)
        {
            Set(value);
            _Context.SaveChanges();
        }

        public async Task SetAndSaveAsync(T? value)
        {
            await SetAsync(value);
            await _Context.SaveChangesAsync();
        }

        public bool SetIfEmpty(T? value)
        {
            var entry = _Repo.FirstOrDefault(x => x.Key == _key);
            if (!String.IsNullOrEmpty(entry?.Value))
                return false;

            Set(value);
            return true;
        }

        public async Task<bool> SetIfEmptyAsync(T? value)
        {
            var entry = await _Repo.FirstOrDefaultAsync(x => x.Key == _key);
            if (!String.IsNullOrEmpty(entry?.Value))
                return false;

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
