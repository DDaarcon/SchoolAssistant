using Microsoft.EntityFrameworkCore;
using SchoolAssistant.DAL.Models.Subjects;
using SchoolAssistant.DAL.Repositories;
using SchoolAssistant.Infrastructure.Models.DataManagement.Subjects;
using SchoolAssistant.Infrastructure.Models.Shared.Json;

namespace SchoolAssistant.Logic.DataManagement.Subjects
{
    public interface ISubjectsDataManagementService
    {
        Task<ResponseJson> CreateOrUpdateAsync(SubjectDetailsJson model);
        Task<SubjectDetailsJson?> GetDetailsJsonAsync(long id);
        Task<SubjectListEntryJson[]> GetEntriesJsonAsync();
    }

    [Injectable]
    public class SubjectsDataManagementService : ISubjectsDataManagementService
    {
        private readonly IRepository<Subject> _subjectRepo;
        private readonly IModifySubjectFromJsonService _modifySvc;

        public SubjectsDataManagementService(
            IRepository<Subject> subjectRepo,
            IModifySubjectFromJsonService modifySvc)
        {
            _subjectRepo = subjectRepo;
            _modifySvc = modifySvc;
        }

        public Task<SubjectListEntryJson[]> GetEntriesJsonAsync()
        {
            var query = _subjectRepo.AsQueryable()
                .Select(x => new SubjectListEntryJson
                {
                    id = x.Id,
                    name = x.Name
                });

            return query.ToArrayAsync();
        }

        public async Task<SubjectDetailsJson?> GetDetailsJsonAsync(long id)
        {
            var subject = await _subjectRepo.GetByIdAsync(id);

            if (subject == null) return null;

            return new SubjectDetailsJson
            {
                id = subject.Id,
                name = subject.Name
            };
        }

        public Task<ResponseJson> CreateOrUpdateAsync(SubjectDetailsJson model)
        {
            return _modifySvc.CreateOrUpdateAsync(model);
        }
    }
}
