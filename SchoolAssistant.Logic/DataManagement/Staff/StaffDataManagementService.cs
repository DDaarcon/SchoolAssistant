using SchoolAssistant.DAL.Models.Staff;
using SchoolAssistant.DAL.Repositories;
using SchoolAssistant.Infrastructure.Models.DataManagement.Staff;

namespace SchoolAssistant.Logic.DataManagement.Staff
{
    public interface IStaffDataManagementService
    {
        Task<StaffListGroupJson[]> GetGroupsOfEntriesJsonAsync();
    }

    [Injectable]
    public class StaffDataManagementService : IStaffDataManagementService
    {
        private readonly ITeachersDataManagementService _teachersService;
        private readonly IRepository<Teacher> _teacherRepo;

        public StaffDataManagementService(
            ITeachersDataManagementService jsonService,
            IRepository<Teacher> teacherRepo)
        {
            _teacherRepo = teacherRepo;
            _teachersService = jsonService;
        }


        public async Task<StaffListGroupJson[]> GetGroupsOfEntriesJsonAsync()
        {
            return new[]
            {
                await _teachersService.GetTeachersGroupJsonAsync(null)
            };
        }

        public async Task<StaffPersonDetailsJson> GetDetailsJsonAsync(long id)
        {

        }
    }
}
