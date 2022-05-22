﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.DependencyInjection;
using SchoolAssistant.DAL.Models.AppStructure;
using System.Linq.Expressions;

namespace SchoolAssistant.DAL.Repositories
{
    public interface IUserRepository
    {
        IList<User> AsList();
        Task<List<User>> AsListAsync();
        IQueryable<User> AsQueryable();
        bool Exists(long id);
        bool Exists(Expression<Func<User, bool>> predicate);
        Task<bool> ExistsAsync(long id);
        Task<bool> ExistsAsync(Expression<Func<User, bool>> predicate);
        User? GetById(long id);
        ValueTask<User?> GetByIdAsync(long id);
        EntityEntry GetEntry(User entity);
        void Save();
        Task SaveAsync();
        void Update(User entity);
        void UpdateRange(IEnumerable<User> entities);
        void UseIndependentDbContext();

        UserManager<User> Manager { get; }
    }

    [Injectable]
    public class UserRepository : IUserRepository
    {
        public UserManager<User> Manager { get; init; }


        private readonly IServiceScopeFactory? _scopeFactory;
        private SADbContext _context;
        private DbSet<User> _Repo => _context.Set<User>();

        public UserRepository(
            SADbContext context,
            IServiceScopeFactory? scopeFactory,
            UserManager<User> manager)
        {
            _context = context;
            _scopeFactory = scopeFactory;
            Manager = manager;
        }

        public IList<User> AsList() => _Repo.ToList();

        public Task<List<User>> AsListAsync() => _Repo.ToListAsync();

        public IQueryable<User> AsQueryable() => _Repo.AsQueryable();

        public bool Exists(long id) => _Repo.Any(x => x.Id == id);

        public Task<bool> ExistsAsync(long id) => _Repo.AnyAsync(x => x.Id == id);

        public bool Exists(Expression<Func<User, bool>> predicate) => _Repo.Any(predicate);
        public Task<bool> ExistsAsync(Expression<Func<User, bool>> predicate) => _Repo.AnyAsync(predicate);

        public User? GetById(long id) => _Repo.Find(id);

        public ValueTask<User?> GetByIdAsync(long id) => _Repo.FindAsync(id);

        public EntityEntry GetEntry(User entity) => _context.Entry(entity);

        public void Save() => _context.SaveChanges();

        public Task SaveAsync() => _context.SaveChangesAsync();

        public void Update(User entity) => _Repo.Update(entity);

        public void UpdateRange(IEnumerable<User> entities) => _Repo.UpdateRange(entities);

        public void UseIndependentDbContext()
        {
            _context = _scopeFactory?.CreateScope().ServiceProvider.GetRequiredService<SADbContext>() ?? _context;
        }
    }
}
