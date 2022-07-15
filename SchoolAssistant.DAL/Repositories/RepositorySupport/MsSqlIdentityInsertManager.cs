using Microsoft.EntityFrameworkCore;
using SchoolAssistant.DAL.Models.Shared;

namespace SchoolAssistant.DAL.Repositories.RepositorySupport
{
    public interface IIdentityInsertManager
    {
        void DisableEveryIdentityInsert();
        Task DisableEveryIdentityInsertAsync();
        void DisableIdentityInsert<TDbEntity>() where TDbEntity : DbEntity;
        Task DisableIdentityInsertAsync<TDbEntity>() where TDbEntity : DbEntity;
        void EnableIdentityInsert<TDbEntity>() where TDbEntity : DbEntity;
        Task EnableIdentityInsertAsync<TDbEntity>() where TDbEntity : DbEntity;
    }

    [Injectable]
    public class MsSqlIdentityInsertManager : IIdentityInsertManager
    {
        private readonly SADbContext _dbContext;

        public MsSqlIdentityInsertManager(
            SADbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task EnableIdentityInsertAsync<TDbEntity>()
            where TDbEntity : DbEntity
            => SetSingleIdentityInsertAsync<TDbEntity>(true);

        public Task DisableIdentityInsertAsync<TDbEntity>()
            where TDbEntity : DbEntity
            => SetSingleIdentityInsertAsync<TDbEntity>(false);

        private async Task SetSingleIdentityInsertAsync<TDbEntity>(bool enable)
            where TDbEntity : DbEntity
        {
            var nameAndValue = GetTableNameAndValue<TDbEntity>(enable);
            if (nameAndValue is null)
                return;

            await _dbContext.Database.ExecuteSqlRawAsync(
                $"SET IDENTITY_INSERT {nameAndValue.Value.tableName} {nameAndValue.Value.value};").ConfigureAwait(false);
        }





        public void EnableIdentityInsert<TDbEntity>()
            where TDbEntity : DbEntity
            => SetSingleIdentityInsert<TDbEntity>(true);

        public void DisableIdentityInsert<TDbEntity>()
            where TDbEntity : DbEntity
            => SetSingleIdentityInsert<TDbEntity>(false);

        private void SetSingleIdentityInsert<TDbEntity>(bool enable)
            where TDbEntity : DbEntity
        {
            var nameAndValue = GetTableNameAndValue<TDbEntity>(enable);
            if (nameAndValue is null)
                return;

            _dbContext.Database.ExecuteSqlRaw(
                $"SET IDENTITY_INSERT {nameAndValue.Value.tableName} {nameAndValue.Value.value};");
        }




        private (string tableName, string value)? GetTableNameAndValue<TDbEntity>(bool enable)
            where TDbEntity : DbEntity
        {
            var entityType = _dbContext.Model.FindEntityType(typeof(TDbEntity));
            if (entityType is null)
                return null;

            var value = enable ? "ON" : "OFF";

            var schema = entityType.GetSchema();

            var prefixSchema = String.IsNullOrEmpty(schema)
                ? ""
                : schema + '.';

            return (prefixSchema + entityType.GetTableName(), value);
        }




        public async Task DisableEveryIdentityInsertAsync()
        {
            using var command = _dbContext.Database.GetDbConnection().CreateCommand();

            // selecting 'turn off identity insert' command for every table
            command.CommandText = @"
select 'set identity_insert ['+s.name+'].['+o.name+'] off'
from sys.objects o
inner join sys.schemas s on s.schema_id=o.schema_id
where o.[type]='U'
and exists(select 1 from sys.columns where object_id=o.object_id and is_identity=1)";

            await _dbContext.Database.OpenConnectionAsync().ConfigureAwait(false);

            using var result = await command.ExecuteReaderAsync();

            while (await result.ReadAsync())
            {
                var disableCommand = result.GetString(0);

                await _dbContext.Database.ExecuteSqlRawAsync(disableCommand);
            }
        }
        public void DisableEveryIdentityInsert()
        {
            using var command = _dbContext.Database.GetDbConnection().CreateCommand();

            // selecting 'turn off identity insert' command for every table
            command.CommandText = @"
select 'set identity_insert ['+s.name+'].['+o.name+'] off'
from sys.objects o
inner join sys.schemas s on s.schema_id=o.schema_id
where o.[type]='U'
and exists(select 1 from sys.columns where object_id=o.object_id and is_identity=1)";

            _dbContext.Database.OpenConnection();

            using var result = command.ExecuteReader();

            while (result.Read())
            {
                var disableCommand = result.GetString(0);

                _dbContext.Database.ExecuteSqlRaw(disableCommand);
            }
        }
    }
}
