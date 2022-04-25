using SchoolAssistant.DAL.Models.StudentsParents;
using SchoolAssistant.DAL.Repositories;
using SchoolAssistant.Infrastructure.Models.DataManagement.Students;
using SchoolAssistant.Infrastructure.Models.Shared.Json;

namespace SchoolAssistant.Logic.DataManagement.Students
{
    public interface IStudentRegisterRecordsDataManagementService
    {
        Task<ResponseJson> CreateOrUpdateAsync(StudentRegisterRecordDetailsJson model);
        Task<StudentRegisterRecordListEntryJson[]> GetEntriesJsonAsync();
        Task<StudentRegisterRecordModificationDataJson?> GetModificationDataJsonAsync(long id);
    }

    [Injectable]
    public class StudentRegisterRecordsDataManagementService : IStudentRegisterRecordsDataManagementService
    {
        private readonly IRepository<StudentRegisterRecord> _registerRepo;

        public StudentRegisterRecordsDataManagementService(
            IRepository<StudentRegisterRecord> registerRepo)
        {
            _registerRepo = registerRepo;
        }


        public async Task<StudentRegisterRecordListEntryJson[]> GetEntriesJsonAsync()
        {
            return null;
        }

        public async Task<StudentRegisterRecordModificationDataJson?> GetModificationDataJsonAsync(long id)
        {
            return null;
        }

        public async Task<ResponseJson> CreateOrUpdateAsync(StudentRegisterRecordDetailsJson model)
        {
            return null;
        }
    }
}
