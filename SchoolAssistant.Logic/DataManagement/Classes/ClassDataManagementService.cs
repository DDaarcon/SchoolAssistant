using Microsoft.EntityFrameworkCore;
using SchoolAssistant.DAL.Models.StudentsOrganization;
using SchoolAssistant.DAL.Repositories;
using SchoolAssistant.Infrastructure.Models.DataManagement.Classes;
using SchoolAssistant.Infrastructure.Models.Shared.Json;

namespace SchoolAssistant.Logic.DataManagement.Classes
{
    public interface IClassDataManagementService
    {
        Task<ResponseJson> CreateOrUpdateAsync(ClassDetailsJson model);
        Task<ClassListEntryJson[]> GetEntriesJsonAsync();
        Task<ClassModificationDataJson?> GetModificationDataJsonAsync(long id);
    }

    [Injectable]
    public class ClassDataManagementService : IClassDataManagementService
    {
        private readonly IModifyClassFromJsonService _modifyFromJsonSvc;
        private readonly IRepository<OrganizationalClass> _repo;

        public ClassDataManagementService(
            IModifyClassFromJsonService modifyFromJsonSvc,
            IRepository<OrganizationalClass> repo)
        {
            _modifyFromJsonSvc = modifyFromJsonSvc;
            _repo = repo;
        }

        public Task<ClassListEntryJson[]> GetEntriesJsonAsync()
        {
            var query = _repo.AsQueryable()
                .OrderBy(x => x.Grade)
                .ThenByDescending(x => x.Distinction);

            return query.Select(x => new ClassListEntryJson
            {
                name = x.Name,
                specialization = x.Specialization,
                amountOfStudents = x.Students.Count
            }).ToArrayAsync();
        }

        public async Task<ClassModificationDataJson?> GetModificationDataJsonAsync(long id)
        {
            var orgClass = await _repo.GetByIdAsync(id);
            if (orgClass is null) return null;

            return new ClassModificationDataJson
            {
                data = CreateDetailsJson(orgClass)
            };
        }

        public Task<ResponseJson> CreateOrUpdateAsync(ClassDetailsJson model)
        {
            return _modifyFromJsonSvc.CreateOrUpdateAsync(model);
        }


        private ClassDetailsJson CreateDetailsJson(OrganizationalClass orgClass)
        {
            return new ClassDetailsJson
            {
                id = orgClass.Id,
                distinction = orgClass.Distinction,
                grade = orgClass.Grade,
                specialization = orgClass.Specialization,
            };
        }
    }
}
