using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.DependencyInjection;
using SchoolAssistant.DAL.Models.Shared;
using SchoolAssistant.DAL.Repositories.RepositorySupport;
using System.Linq.Expressions;

namespace SchoolAssistant.DAL.Repositories
{
    public interface IRepository<TDbEntity>
        where TDbEntity : DbEntity
    {
        void Add(TDbEntity entity);
        Task AddAsync(TDbEntity entity);
        void AddRange(IEnumerable<TDbEntity> entities);
        Task AddRangeAsync(IEnumerable<TDbEntity> entities);
        IList<TDbEntity> AsList();
        Task<List<TDbEntity>> AsListAsync();
        IQueryable<TDbEntity> AsQueryable();
        Task DisableIdentityInsertAsync();
        Task EnableIdentityInsertAsync();
        bool Exists(long id);
        bool Exists(Expression<Func<TDbEntity, bool>> predicate);
        Task<bool> ExistsAsync(long id);
        Task<bool> ExistsAsync(Expression<Func<TDbEntity, bool>> predicate);
        TDbEntity? GetById(long id);
        ValueTask<TDbEntity?> GetByIdAsync(long id);
        EntityEntry GetEntry(TDbEntity entity);
        void Remove(TDbEntity entity);
        void RemoveById(long id);
        void RemoveRange(IEnumerable<TDbEntity> entities);
        void Save();
        Task SaveAsync();
        /// <summary>
        /// Disables <c>IDENTITY_INSERT</c> for every entity
        /// </summary>
        Task SaveWithIdenityInsertAsync();
        void Update(TDbEntity entity);
        void UpdateRange(IEnumerable<TDbEntity> entities);
        void UseIndependentDbContext();
    }

    [Injectable(typeof(IRepository<>))]
    public class Repository<TDbEntity> : IRepository<TDbEntity>
        where TDbEntity : DbEntity
    {
        private readonly IServiceScopeFactory? _scopeFactory;
        protected SADbContext _context;
        private readonly IIdentityInsertManager? _identityInsertManagerSvc;

        protected DbSet<TDbEntity> _Repo => _context.Set<TDbEntity>();

        public Repository(
            SADbContext context,
            IServiceScopeFactory? scopeFactory = null,
            IIdentityInsertManager? identityInsertManagerSvc = null)
        {
            _context = context;
            _scopeFactory = scopeFactory;
            _identityInsertManagerSvc = identityInsertManagerSvc;
        }

        #region Inserting Methods

        public virtual void Add(TDbEntity entity)
        {
            _Repo.Add(entity);
        }

        public virtual async Task AddAsync(TDbEntity entity)
        {
            await _Repo.AddAsync(entity);
        }

        public virtual void AddRange(IEnumerable<TDbEntity> entities)
        {
            _Repo.AddRange(entities);
        }

        public virtual async Task AddRangeAsync(IEnumerable<TDbEntity> entities)
        {
            await _Repo.AddRangeAsync(entities);
        }

        #endregion

        #region Updating methods

        public void Update(TDbEntity entity)
        {
            _Repo.Update(entity);
        }

        public void UpdateRange(IEnumerable<TDbEntity> entities)
        {
            _Repo.UpdateRange(entities);
        }

        #endregion

        #region Fetching methods

        public virtual ValueTask<TDbEntity?> GetByIdAsync(long id)
        {
            return _Repo.FindAsync(id);
        }

        public virtual TDbEntity? GetById(long id)
        {
            return _Repo.Find(id);
        }

        public Task<List<TDbEntity>> AsListAsync()
        {
            return _Repo.ToListAsync();
        }

        public IList<TDbEntity> AsList()
        {
            return _Repo.ToList();
        }

        public IQueryable<TDbEntity> AsQueryable()
        {
            return _Repo.AsQueryable();
        }

        #endregion

        #region Deleting methods

        public virtual void Remove(TDbEntity entity)
        {
            _Repo.Remove(entity);
        }

        public void RemoveById(long id)
        {
            _Repo.Remove(GetById(id) ?? throw new Exception("Entity with given id does not exist"));
        }

        public void RemoveRange(IEnumerable<TDbEntity> entities)
        {
            _Repo.RemoveRange(entities);
        }

        #endregion

        #region Existance check methods

        public bool Exists(long id) => _Repo.Any(x => x.Id == id);

        public Task<bool> ExistsAsync(long id) => _Repo.AnyAsync(x => x.Id == id);

        public bool Exists(Expression<Func<TDbEntity, bool>> predicate) => _Repo.Any(predicate);
        public Task<bool> ExistsAsync(Expression<Func<TDbEntity, bool>> predicate) => _Repo.AnyAsync(predicate);

        #endregion

        #region Context methods

        public EntityEntry GetEntry(TDbEntity entity)
        {
            return _context.Entry(entity);
        }

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

        public Task EnableIdentityInsertAsync() =>
            _identityInsertManagerSvc?.EnableIdentityInsertAsync<TDbEntity>() ?? Task.CompletedTask;

        public Task DisableIdentityInsertAsync() =>
            _identityInsertManagerSvc?.DisableIdentityInsertAsync<TDbEntity>() ?? Task.CompletedTask;


        public async Task SaveWithIdenityInsertAsync()
        {
            await (_identityInsertManagerSvc?.EnableIdentityInsertAsync<TDbEntity>() ?? Task.CompletedTask).ConfigureAwait(false);

            await SaveAsync().ConfigureAwait(false);

            await (_identityInsertManagerSvc?.DisableEveryIdentityInsertAsync() ?? Task.CompletedTask).ConfigureAwait(false);
        }

        #endregion
    }
}
