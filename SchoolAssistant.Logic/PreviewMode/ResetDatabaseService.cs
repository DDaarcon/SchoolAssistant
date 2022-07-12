using Microsoft.Extensions.Configuration;
using SchoolAssistant.DAL;
using SchoolAssistant.DAL.Models.AppStructure;
using SchoolAssistant.DAL.Models.Attendance;
using SchoolAssistant.DAL.Models.Lessons;
using SchoolAssistant.DAL.Models.Marks;
using SchoolAssistant.DAL.Models.Rooms;
using SchoolAssistant.DAL.Models.Staff;
using SchoolAssistant.DAL.Models.StudentsOrganization;
using SchoolAssistant.DAL.Models.StudentsParents;
using SchoolAssistant.DAL.Models.Subjects;
using SchoolAssistant.Logic.PreviewMode.ResetDatabaseSupport;

namespace SchoolAssistant.Logic.PreviewMode
{
    public interface IResetDatabaseService
    {
        Task ResetAsync();
    }

    [Injectable]
    public class ResetDatabaseService : IResetDatabaseService
    {
        private readonly SADbContext _dbContext;
        private readonly IConfiguration _config;
        private readonly IRecreateDatabaseService _recreateSvc;

        public ResetDatabaseService(
            SADbContext dbContext,
            IConfiguration config,
            IRecreateDatabaseService recreateSvc)
        {
            _dbContext = dbContext;
            _config = config;
            _recreateSvc = recreateSvc;
        }

        public async Task ResetAsync()
        {
            ClearMostOfTables();

            await _dbContext.SaveChangesAsync().ConfigureAwait(false);

            await _recreateSvc.RecreateAsync().ConfigureAwait(false);
        }

        private void ClearMostOfTables()
        {
            ClearTable<Presence>();
            ClearTable<Mark>();
            ClearTable<MarksOfClass>();
            ClearTable<Lesson>();
            ClearTable<PeriodicLesson>();
            ClearTable<Room>();
            ClearTable<SubjectClass>();
            ClearTable<Subject>();
            ClearTable<Teacher>();
            ClearTable<Student>();
            ClearTable<StudentRegisterRecord>();
            ClearTable<Parent>();
            ClearTable<Role>();
            ClearTable<User>();
            ClearTable(_dbContext.Config);
        }

        private void ClearTable<TDbEntity>(Microsoft.EntityFrameworkCore.DbSet<TDbEntity>? set = null)
            where TDbEntity : class
        {
            set ??= _dbContext.Set<TDbEntity>();

            set?.RemoveRange(set);
        }
    }
}
