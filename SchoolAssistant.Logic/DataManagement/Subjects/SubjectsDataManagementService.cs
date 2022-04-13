using Microsoft.EntityFrameworkCore;
using SchoolAssistant.DAL.Models.Subjects;
using SchoolAssistant.DAL.Repositories;
using SchoolAssistant.Infrastructure.Models.DataManagement.Subjects;

namespace SchoolAssistant.Logic.DataManagement.Subjects
{
    public interface ISubjectsDataManagementService
    {
        Task<SubjectDetailsJsonModel?> GetDetailsJsonAsync(long id);
        Task<SubjectListEntryJsonModel[]> GetEntriesJsonAsync();
    }

    [Injectable]
    public class SubjectsDataManagementService : ISubjectsDataManagementService
    {
        private readonly IRepository<Subject> _subjectRepo;

        public SubjectsDataManagementService(
            IRepository<Subject> subjectRepo)
        {
            _subjectRepo = subjectRepo;
        }

        public Task<SubjectListEntryJsonModel[]> GetEntriesJsonAsync()
        {
            var query = _subjectRepo.AsQueryable()
                .Select(x => new SubjectListEntryJsonModel
                {
                    id = x.Id,
                    name = x.Name
                });

            return query.ToArrayAsync();
        }

        public async Task<SubjectDetailsJsonModel?> GetDetailsJsonAsync(long id)
        {
            var subject = await _subjectRepo.GetByIdAsync(id);

            if (subject == null) return null;

            return new SubjectDetailsJsonModel
            {
                id = subject.Id,
                name = subject.Name
            };
        }
    }
}
