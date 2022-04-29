using Microsoft.EntityFrameworkCore;
using SchoolAssistant.DAL.Models.StudentsOrganization;
using SchoolAssistant.DAL.Models.StudentsParents;
using SchoolAssistant.DAL.Repositories;
using SchoolAssistant.Infrastructure.Models.DataManagement.Students;
using SchoolAssistant.Infrastructure.Models.Shared.Json;

namespace SchoolAssistant.Logic.DataManagement.Students
{
    public interface IStudentsDataManagementService
    {
        Task<ResponseJson> CreateOrUpdateAsync(StudentDetailsJson model);
        Task<StudentListEntryJson[]> GetEntriesJsonAsync(long classId);
        Task<StudentModificationDataJson?> GetModificationDataJsonAsync(long id);
    }

    [Injectable]
    public class StudentsDataManagementService : IStudentsDataManagementService
    {
        private readonly IRepository<OrganizationalClass> _orgClassRepo;
        private readonly IRepository<Student> _studentRepo;
        private readonly IStudentRegisterRecordsDataManagementService _registerDataManagementService;
        private readonly IModifyStudentFromJsonService _modifyFromJsonSvc;

        public StudentsDataManagementService(
            IRepository<OrganizationalClass> orgClassRepo,
            IRepository<Student> studentRepo,
            IStudentRegisterRecordsDataManagementService registerDataManagementService,
            IModifyStudentFromJsonService modifyFromJsonSvc)
        {
            _orgClassRepo = orgClassRepo;
            _studentRepo = studentRepo;
            _registerDataManagementService = registerDataManagementService;
            _modifyFromJsonSvc = modifyFromJsonSvc;
        }


        public Task<StudentListEntryJson[]> GetEntriesJsonAsync(long classId)
        {
            var query = _orgClassRepo.AsQueryable()
                .Where(x => x.Id == classId)
                .SelectMany(x => x.Students);

            return query.Select(x => new StudentListEntryJson
            {
                id = x.Id,
                name = x.Info.GetFullName(),
                numberInJournal = x.NumberInJournal
            }).ToArrayAsync();
        }

        public async Task<StudentModificationDataJson?> GetModificationDataJsonAsync(long id)
        {
            var student = await _studentRepo.GetByIdAsync(id);
            if (student is null)
                return null;

            var registerRecords = await _registerDataManagementService.GetEntriesJsonAsync();

            return new StudentModificationDataJson
            {
                data = new StudentDetailsJson
                {
                    id = student.Id,
                    numberInJournal = student.NumberInJournal,
                    organizationalClassId = student.OrganizationalClassId,
                    registerRecordId = student.InfoId
                },
                registerRecords = registerRecords
            };
        }

        public Task<ResponseJson> CreateOrUpdateAsync(StudentDetailsJson model)
        {
            return _modifyFromJsonSvc.CreateOrUpdateAsync(model);
        }
    }
}
