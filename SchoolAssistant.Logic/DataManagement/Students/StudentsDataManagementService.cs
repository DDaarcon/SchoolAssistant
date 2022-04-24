using SchoolAssistant.DAL.Models.StudentsOrganization;
using SchoolAssistant.DAL.Repositories;
using SchoolAssistant.Infrastructure.Models.DataManagement.Students;
using SchoolAssistant.Infrastructure.Models.Shared.Json;

namespace SchoolAssistant.Logic.DataManagement.Students
{
    public interface IStudentsDataManagementService
    {
        Task<ResponseJson> CreateOrUpdateStudentAsync(StudentDetailsJson model);
        Task<ResponseJson> CreateOrUpdateStudentRegisterRecordAsync(StudentRegisterRecordDetailsJson model);
        Task<StudentListEntryJson[]> GetEntriesJsonAsync(long classId);
        Task<StudentModificationDataJson?> GetModificationDataJsonAsync(long id);
    }

    [Injectable]
    public class StudentsDataManagementService : IStudentsDataManagementService
    {
        private readonly IRepository<OrganizationalClass> _orgClassRepo;

        public StudentsDataManagementService(
            IRepository<OrganizationalClass> orgClassRepo)
        {
            _orgClassRepo = orgClassRepo;
        }


        public async Task<StudentListEntryJson[]> GetEntriesJsonAsync(long classId)
        {
            return null;
        }

        public async Task<StudentModificationDataJson?> GetModificationDataJsonAsync(long id)
        {
            return null;
        }

        public async Task<ResponseJson> CreateOrUpdateStudentAsync(StudentDetailsJson model)
        {
            return null;
        }

        public async Task<ResponseJson> CreateOrUpdateStudentRegisterRecordAsync(StudentRegisterRecordDetailsJson model)
        {
            return null;
        }
    }
}
