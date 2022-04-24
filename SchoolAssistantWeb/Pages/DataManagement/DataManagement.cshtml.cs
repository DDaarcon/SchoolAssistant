using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SchoolAssistant.Infrastructure.Models.DataManagement.Classes;
using SchoolAssistant.Infrastructure.Models.DataManagement.Staff;
using SchoolAssistant.Infrastructure.Models.DataManagement.Subjects;
using SchoolAssistant.Logic.DataManagement.Classes;
using SchoolAssistant.Logic.DataManagement.Staff;
using SchoolAssistant.Logic.DataManagement.Subjects;

namespace SchoolAssistant.Web.Pages.DataManagement
{
    public class DataManagementModel : PageModel
    {
        private readonly ISubjectsDataManagementService _subjectsService;
        private readonly IStaffDataManagementService _staffService;
        private readonly IClassDataManagementService _classService;

        public DataManagementModel(
            ISubjectsDataManagementService subjectsService,
            IStaffDataManagementService staffService,
            IClassDataManagementService classService)
        {
            _subjectsService = subjectsService;
            _staffService = staffService;
            _classService = classService;
        }

        public void OnGet()
        {
        }




        public async Task<JsonResult> OnGetSubjectEntriesAsync()
        {
            var entries = await _subjectsService.GetEntriesJsonAsync();
            return new JsonResult(entries);
        }

        public async Task<JsonResult> OnGetSubjectDetailsAsync(long id)
        {
            var details = await _subjectsService.GetDetailsJsonAsync(id);
            return new JsonResult(details);
        }

        public async Task<JsonResult> OnPostSubjectDataAsync([FromBody] SubjectDetailsJson model)
        {
            var result = await _subjectsService.CreateOrUpdateAsync(model);
            return new JsonResult(result);
        }





        public async Task<JsonResult> OnGetStaffPersonsEntriesAsync()
        {
            var groups = await _staffService.GetGroupsOfEntriesJsonAsync();
            return new JsonResult(groups);
        }
        // TODO: Merge endpoints OnGetStaffPersonDetailsAsync and OnGetAvailableSubjectsAsync
        public async Task<JsonResult> OnGetStaffPersonDetailsAsync(string groupId, long id)
        {
            var details = await _staffService.GetDetailsJsonAsync(groupId, id);
            return new JsonResult(details);
        }

        public async Task<JsonResult> OnPostStaffPersonDataAsync([FromBody] StaffPersonDetailsJson model)
        {
            var result = await _staffService.CreateOrUpdateAsync(model);
            return new JsonResult(result);
        }

        public async Task<JsonResult> OnGetAvailableSubjectsAsync()
        {
            var items = await _subjectsService.GetEntriesJsonAsync();
            return new JsonResult(items);
        }




        public async Task<JsonResult> OnGetClassEntriesAsync()
        {
            var entries = await _classService.GetEntriesJsonAsync();
            return new JsonResult(entries);
        }

        public async Task<JsonResult> OnGetClassModificationDataAsync(long id)
        {
            var modifyModel = await _classService.GetModificationDataJsonAsync(id);
            return new JsonResult(modifyModel);
        }

        public async Task<JsonResult> OnPostClassDataAsync([FromBody] ClassDetailsJson model)
        {
            var result = await _classService.CreateOrUpdateAsync(model);
            return new JsonResult(result);
        }
    }
}
