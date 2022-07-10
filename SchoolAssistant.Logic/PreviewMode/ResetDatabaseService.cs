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
            await ClearMostOfTablesAsync().ConfigureAwait(false);

            await ClearTeachersAsync().ConfigureAwait(false);
            await ClearStudentsAsync().ConfigureAwait(false);

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

            await ClearTableAsync<SubjectClass>().ConfigureAwait(false);
            await ClearTableAsync<Subject>().ConfigureAwait(false);
            await ClearTableAsync<Student>().ConfigureAwait(false);

            await ClearTableAsync<Role>().ConfigureAwait(false);
            await ClearTableAsync<User>().ConfigureAwait(false);
            await ClearTableAsync(_dbContext.Config).ConfigureAwait(false);
        }

        private async Task ClearTeachersAsync()
        {
            var set = _dbContext.Set<Teacher>();
            var sampleTeacherEntityId = long.TryParse(_config["PreviewMode:Logins:Teacher:RelatedEntityId"], out var id)
                ? id
                : 0;

            set.RemoveRange(set.Where(x => x.Id == sampleTeacherEntityId));
        }

        private async Task ClearStudentsAsync()
        {
            var set = _dbContext.Set<StudentRegisterRecord>();
            var sampleStudentEntityId = long.TryParse(_config["PreviewMode:Logins:Student:RelatedEntityId"], out var id)
                ? id
                : 0;

            set.RemoveRange(set.Where(x => x.Id == sampleStudentEntityId));
        }

        private async Task ClearTableAsync<TDbEntity>(Microsoft.EntityFrameworkCore.DbSet<TDbEntity>? set = null)
            where TDbEntity : class
        {
            set ??= _dbContext.Set<TDbEntity>();

            set?.RemoveRange(set);
        }
    }
}
