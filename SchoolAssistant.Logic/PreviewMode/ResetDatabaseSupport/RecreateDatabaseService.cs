using SchoolAssistant.DAL;
using SchoolAssistant.DAL.Models.Staff;
using SchoolAssistant.DAL.Repositories;
using SchoolAssistant.DAL.Seeding;

namespace SchoolAssistant.Logic.PreviewMode.ResetDatabaseSupport
{
    public interface IRecreateDatabaseService
    {

    }

    [Injectable]
    public class RecreateDatabaseService : IRecreateDatabaseService
    {
        private readonly IDefaultDataSeeder _seeder;
        private readonly SADbContext _dbContext;
        private readonly IRepository<Teacher> _teacherRepo;

        public RecreateDatabaseService(
            IDefaultDataSeeder seeder,
            SADbContext dbContext)
        {
            _seeder = seeder;
            _dbContext = dbContext;
        }

        public async Task RecreateAsync()
        {


            await _seeder.SeedAppConfigAsync().ConfigureAwait(false);
            await _seeder.SeedRolesAndUsersAsync().ConfigureAwait(false);


        }

        public async Task RecreateTeachersAsync()
        {

        }
    }
}
