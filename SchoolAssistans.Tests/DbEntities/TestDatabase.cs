using Microsoft.EntityFrameworkCore;
using SchoolAssistant.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAssistans.Tests.DbEntities
{
    internal static class TestDatabase
    {
        private const string ConnectionString = @"Server=.\SQLExpress;Database=EFTestSample;Trusted_Connection=True";

        private static readonly object _lock = new();
        private static bool _databaseInitialized;

        private static SADbContext _context = null!;
        public static SADbContext Context { get { return GetContext(); } private set { _context = value; } }


        public static SADbContext GetContext()
        {
            InitializeContext();

            return _context;
        }

        private static void InitializeContext()
        {
            lock (_lock)
            {
                if (!_databaseInitialized)
                {
                    _context = CreateContext();

                    _context.Database.EnsureDeleted();
                    _context.Database.EnsureCreated();

                    _databaseInitialized = true;
                }
            }
        }

        public static void DisposeContext()
        {
            _context?.Database.EnsureDeleted();
            _context?.Dispose();
        }

        private static SADbContext CreateContext()
            => new SADbContext(
                new DbContextOptionsBuilder<SADbContext>()
                    .UseSqlServer(ConnectionString)
                    .Options);
    }
}
