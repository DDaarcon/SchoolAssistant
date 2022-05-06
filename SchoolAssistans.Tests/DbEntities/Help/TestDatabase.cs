﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SchoolAssistant.DAL;
using System.Diagnostics;
using System.Threading.Tasks;

namespace SchoolAssistans.Tests.DbEntities
{
    internal static class TestDatabase
    {
        private const string ConnectionString = @"Server=.\SQLExpress;Database=EFTestSample;Trusted_Connection=True";

        private static readonly object _lock = new();
        private static bool _databaseInitialized;

        private static SADbContext? _context;
        public static SADbContext Context { get { return CreateContext(); } private set { _context = value; } }


        public static SADbContext CreateContext(IServiceCollection? services = null)
        {
            InitializeContext(services);

            return _context!;
        }

        private static void InitializeContext(IServiceCollection? services)
        {
            lock (_lock)
            {
                if (!_databaseInitialized)
                {
                    if (services is not null)
                    {
                        AddContextToServices(services);
                        _databaseInitialized = true;
                        RequestContextFromServices(services);
                    }
                    else
                    {
                        _context = ConstructContext();
                        _databaseInitialized = true;
                    }

                    _context!.Database.EnsureDeleted();
                    _context.Database.EnsureCreated();
                }
            }
        }

        public static void DisposeContext()
        {
            _context?.Database.EnsureDeleted();
            _context?.Database.CloseConnection();
            _context?.Dispose();
            _databaseInitialized = false;
        }

        private static SADbContext ConstructContext()
            => new SADbContext(
                new DbContextOptionsBuilder<SADbContext>()
                    .LogTo(message => Debug.WriteLine(message))
                    .UseSqlServer(ConnectionString)
                    .UseLazyLoadingProxies()
                    .Options);

        public static async Task ClearDataAsync<TDbEntity>()
            where TDbEntity : class
        {
            if (_context is null) return;

            var set = _context.Set<TDbEntity>();

            set?.RemoveRange(set);

            await _context.SaveChangesAsync();
        }

        public static void StopTrackingEntities() => _context?.ChangeTracker.Clear();

        public static void RequestContextFromServices(IServiceCollection services)
        {
            if (!_databaseInitialized) InitializeContext(services);
            else _context = services.BuildServiceProvider().GetRequiredService<SADbContext>();
        }

        private static void AddContextToServices(IServiceCollection services)
        {
            services.AddDbContext<SADbContext>(options =>
            {
                options.UseSqlServer(ConnectionString);
                options.UseLazyLoadingProxies();
            });
        }
    }
}
