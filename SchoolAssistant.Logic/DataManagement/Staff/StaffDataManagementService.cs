using SchoolAssistant.DAL.Models.Staff;
using SchoolAssistant.Infrastructure.Models.DataManagement.Staff;
using SchoolAssistant.Infrastructure.Models.Shared.Json;

namespace SchoolAssistant.Logic.DataManagement.Staff
{
    public interface IStaffDataManagementService
    {
        Task<ResponseJson> CreateOrUpdateAsync(StaffPersonDetailsJson model);
        Task<StaffPersonDetailsJson?> GetDetailsJsonAsync(string groupId, long id);
        Task<StaffListGroupJson[]> GetGroupsOfEntriesJsonAsync();
    }

    [Injectable]
    public class StaffDataManagementService : IStaffDataManagementService
    {
        private readonly ITeachersDataManagementService _teachersService;

        public StaffDataManagementService(
            ITeachersDataManagementService teachersService)
        {
            _teachersService = teachersService;
        }


        public async Task<StaffListGroupJson[]> GetGroupsOfEntriesJsonAsync()
        {
            return new[]
            {
                await _teachersService.GetGroupJsonAsync(null)
            };
        }

        public async Task<StaffPersonDetailsJson?> GetDetailsJsonAsync(string groupId, long id)
        {
            return groupId switch
            {
                nameof(Teacher) => await _teachersService.GetDetailsJsonAsync(id),
                _ => null
            };
        }

        public async Task<ResponseJson> CreateOrUpdateAsync(StaffPersonDetailsJson model)
        {
            return model.groupId switch
            {
                nameof(Teacher) => await _teachersService.CreateOrUpdateAsync(model),
                _ => new ResponseJson
                {
                    message = "Błąd! Nieprawidłowa kategoria personelu"
                }
            };
        }
    }
}
