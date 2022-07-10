using SchoolAssistant.DAL;
using SchoolAssistant.DAL.Models.Attendance;
using SchoolAssistant.DAL.Models.Lessons;
using SchoolAssistant.DAL.Models.Marks;
using SchoolAssistant.DAL.Models.Rooms;
using SchoolAssistant.DAL.Models.Shared;
using SchoolAssistant.DAL.Models.StudentsOrganization;
using SchoolAssistant.DAL.Models.StudentsParents;
using SchoolAssistant.DAL.Models.Subjects;
using SchoolAssistant.DAL.Seeding;

namespace SchoolAssistant.Logic.PreviewMode
{
    public interface IResetDatabaseService
    {
        Task ResetAsync();
    }

    [Injectable]
    public class ResetDatabaseService : IResetDatabaseService
    {
        private readonly IDefaultDataSeeder _defaultDataSeeder;
        private readonly SADbContext _dbContext;

        public ResetDatabaseService(
            IDefaultDataSeeder defaultDataSeeder,
            SADbContext dbContext)
        {
            _defaultDataSeeder = defaultDataSeeder;
            _dbContext = dbContext;
        }

        public async Task ResetAsync()
        {
            await ClearMostOfTablesAsync().ConfigureAwait(false);

            await _dbContext.SaveChangesAsync().ConfigureAwait(false);
        }

        private async Task ClearMostOfTablesAsync()
        {
            await ClearTableAsync<Presence>().ConfigureAwait(false);

            await ClearTableAsync<Mark>().ConfigureAwait(false);
            await ClearTableAsync<MarksOfClass>().ConfigureAwait(false);

            await ClearTableAsync<Lesson>().ConfigureAwait(false);
            await ClearTableAsync<PeriodicLesson>().ConfigureAwait(false);

            await ClearTableAsync<Room>().ConfigureAwait(false);

            // TODO: check if no error
            await ClearTableAsync<SubjectClass>().ConfigureAwait(false);
            await ClearTableAsync<Subject>().ConfigureAwait(false);

            await ClearTableAsync<Student>().ConfigureAwait(false);
        }

        private async Task ClearStudentsAsync()
        {
            var set = _dbContext.Set<StudentRegisterRecord>();
        }

        private async Task ClearTableAsync<TDbEntity>()
            where TDbEntity : DbEntity
        {
            var set = _dbContext.Set<TDbEntity>();

            set?.RemoveRange(set);
        }
    }
}
