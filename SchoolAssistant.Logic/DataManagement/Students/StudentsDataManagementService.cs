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

        public StudentsDataManagementService(
            IRepository<OrganizationalClass> orgClassRepo,
            IRepository<Student> studentRepo)
        {
            _orgClassRepo = orgClassRepo;
            _studentRepo = studentRepo;
        }


        public async Task<StudentListEntryJson[]> GetEntriesJsonAsync(long classId)
        {
            return null;
        }

        public async Task<StudentModificationDataJson?> GetModificationDataJsonAsync(long id)
        {
            return null;
        }

        public async Task<ResponseJson> CreateOrUpdateAsync(StudentDetailsJson model)
        {
            return null;
        }
    }
}
