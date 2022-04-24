using Microsoft.EntityFrameworkCore;
using SchoolAssistant.DAL.Models.Staff;
using SchoolAssistant.DAL.Repositories;
using SchoolAssistant.Infrastructure.Models.DataManagement.Staff;
using SchoolAssistant.Infrastructure.Models.Shared.Json;

namespace SchoolAssistant.Logic.DataManagement.Staff
{
    public interface ITeachersDataManagementService
    {
        Task<ResponseJson> CreateOrUpdateAsync(StaffPersonDetailsJson model);
        Task<StaffPersonDetailsJson?> GetDetailsJsonAsync(long id);
        Task<StaffListEntryJson[]> GetEntriesJsonAsync(IQueryable<Teacher>? query);
        Task<StaffListGroupJson> GetGroupJsonAsync(IQueryable<Teacher>? query);
    }

    [Injectable]
    public class TeachersDataManagementService : ITeachersDataManagementService
    {
        private readonly IModifyTeacherFromJsonService _modifySvc;
        private readonly IRepository<Teacher> _repo;

        private string _GeneralTeachersId => nameof(Teacher);

        public TeachersDataManagementService(
            IModifyTeacherFromJsonService modifySvc,
            IRepository<Teacher> teacherRepo)
        {
            _modifySvc = modifySvc;
            _repo = teacherRepo;
        }


        public async Task<StaffListGroupJson> GetGroupJsonAsync(IQueryable<Teacher>? query)
        {
            return new StaffListGroupJson
            {
                id = _GeneralTeachersId,
                name = "Nauczyciele",
                entries = await GetEntriesJsonAsync(query)
            };
        }

        public Task<StaffListEntryJson[]> GetEntriesJsonAsync(IQueryable<Teacher>? query)
        {
            query ??= _repo.AsQueryable();
            return query.Select(x => new StaffListEntryJson
            {
                id = x.Id,
                name = x.GetFullName(),
                specialization = String.Join(", ", x.MainSubjects.Select(x => x.Subject.Name))
            }).ToArrayAsync();
        }

        public async Task<StaffPersonDetailsJson?> GetDetailsJsonAsync(long id)
        {
            var teacher = await _repo.GetByIdAsync(id);

            if (teacher is null) return null;

            return new StaffPersonDetailsJson
            {
                id = teacher.Id,
                firstName = teacher.FirstName,
                secondName = teacher.SecondName,
                lastName = teacher.LastName,
                mainSubjectsIds = teacher.SubjectOperations.MainIter.Select(x => x.Id).ToArray(),
                additionalSubjectsIds = teacher.SubjectOperations.AdditionalIter.Select(x => x.Id).ToArray()
            };
        }

        public Task<ResponseJson> CreateOrUpdateAsync(StaffPersonDetailsJson model)
        {
            return _modifySvc.CreateOrUpdateAsync(model);
        }
    }
}
