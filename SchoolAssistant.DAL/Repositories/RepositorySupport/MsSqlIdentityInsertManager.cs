using Microsoft.EntityFrameworkCore;
using SchoolAssistant.DAL.Models.Shared;

namespace SchoolAssistant.DAL.Repositories.RepositorySupport
{
    public interface IIdentityInsertManager
    {
        Task DisableEveryIdentityInsertAsync();
        Task DisableIdentityInsertAsync<TDbEntity>() where TDbEntity : DbEntity;
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
            => SetSingleIdentityInsert<TDbEntity>(false);

        public Task DisableIdentityInsertAsync<TDbEntity>()
            where TDbEntity : DbEntity
            => SetSingleIdentityInsert<TDbEntity>(true);

        private Task SetSingleIdentityInsert<TDbEntity>(bool enable)
            where TDbEntity : DbEntity
        {
            var entityType = _dbContext.Model.FindEntityType(typeof(TDbEntity));
            if (entityType is null)
                return Task.CompletedTask;

            var value = enable ? "ON" : "OFF";
            return _dbContext.Database.ExecuteSqlRawAsync(
                $"SET IDENTITY_INSERT {entityType.GetSchema()}.{entityType.GetTableName()} {value}");
        }


        public async Task DisableEveryIdentityInsertAsync()
        {
            using var command = _dbContext.Database.GetDbConnection().CreateCommand();

            // selecting 'turn off identity insert' command for every table with it enabled
            command.CommandText = @"
select 'set identity_insert ['+s.name+'].['+o.name+'] off'
from sys.objects o
inner join sys.schemas s on s.schema_id=o.schema_id
where o.[type]='U'
and exists(select 1 from sys.columns where object_id=o.object_id and is_identity=1)";

            _dbContext.Database.OpenConnection();

            using var result = await command.ExecuteReaderAsync().ConfigureAwait(false);

            while (await result.ReadAsync().ConfigureAwait(false))
            {
                var disableCommand = result.GetString(0);

                await _dbContext.Database.ExecuteSqlRawAsync(disableCommand).ConfigureAwait(false);
            }
        }
    }
}
