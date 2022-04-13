using Microsoft.EntityFrameworkCore;
using SchoolAssistant.DAL.Models.Subjects;
using SchoolAssistant.DAL.Repositories;
using SchoolAssistant.Infrastructure.Models.DataManagement.Subjects;

namespace SchoolAssistant.Logic.DataManagement.Subjects
{
    public interface ISubjectsListService
    {
        Task<SubjectListEntryJsonModel[]> GetEntriesJsonAsync();
    }

    [Injectable(ServiceLifetime.Scoped)]
    public class SubjectsListService : ISubjectsListService
    {
        private readonly IRepository<Subject> _subjectRepo;

        public SubjectsListService(
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
    }
}
