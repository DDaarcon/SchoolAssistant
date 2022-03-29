using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SchoolAssistant.DAL;

namespace SchoolAssistans.Tests.DbEntities
{
    internal static class TestDatabase
    {
        private const string ConnectionString = @"Server=.\SQLExpress;Database=EFTestSample;Trusted_Connection=True";

        private static readonly object _lock = new();
        private static bool _databaseInitialized;

        private static SADbContext _context = null!;
        public static SADbContext Context { get { return CreateContext(); } private set { _context = value; } }


        public static SADbContext CreateContext(IServiceCollection? services = null)
        {
            InitializeContext(services);

            return _context;
        }

        private static void InitializeContext(IServiceCollection? services)
        {
            lock (_lock)
            {
                if (!_databaseInitialized)
                {
                    if (services is not null)
                    {
                        services.AddDbContext<SADbContext>(options =>
                        {
                            options.UseSqlServer(ConnectionString);
                        });
                        _context = services.BuildServiceProvider().GetRequiredService<SADbContext>();
                    }
                    else
                    {
                        _context = CreateContext();
                    }

                    _context.Database.EnsureDeleted();
                    _context.Database.EnsureCreated();

                    _databaseInitialized = true;
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

        private static SADbContext CreateContext()
            => new SADbContext(
                new DbContextOptionsBuilder<SADbContext>()
                    .UseSqlServer(ConnectionString)
                    .Options);
    }
}
