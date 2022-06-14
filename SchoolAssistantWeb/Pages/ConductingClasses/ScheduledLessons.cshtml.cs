using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SchoolAssistant.DAL.Models.AppStructure;
using SchoolAssistant.DAL.Repositories;
using SchoolAssistant.Infrastructure.Models.ConductingClasses.ScheduledLessonsList;
using SchoolAssistant.Logic.ConductingClasses;

namespace SchoolAssistant.Web.Pages.ConductingClasses
{
    public class ScheduledLessonsModel : PageModel
    {
        private readonly IUserRepository _userRepo;
        private readonly IFetchScheduledLessonListEntriesService _scheduledLessonsListSvc;
        private readonly IFetchScheduledLessonListConfigService _scheduledLessonsListConfigSvc;

        private User _user = null!;
        public ScheduledLessonListEntriesJson ScheduledLessonListEntries { get; set; } = null!;
        public ScheduledLessonListConfigJson ScheduledLessonListConfig { get; set; } = null!;

        public ScheduledLessonsModel(
            IUserRepository userRepo,
            IFetchScheduledLessonListEntriesService scheduledLessonsListSvc,
            IFetchScheduledLessonListConfigService scheduledLessonsListConfigSvc)
        {
            _userRepo = userRepo;
            _scheduledLessonsListSvc = scheduledLessonsListSvc;
            _scheduledLessonsListConfigSvc = scheduledLessonsListConfigSvc;
        }

        public async Task OnGetAsync()
        {
            await FetchUserAsync();

            ScheduledLessonListEntries = (await _scheduledLessonsListSvc.GetModelForTeacherAsync(_user.TeacherId!.Value, new FetchScheduledLessonsRequestModel
            {
                From = DateTime.Now.AddDays(-1),
                LimitTo = 30
            }))!;
            ScheduledLessonListConfig = await _scheduledLessonsListConfigSvc.GetDefaultConfigAsync();
        }


        public async Task<JsonResult> OnGetOlderLessonsAsync()
        {
            return null;
        }


        private async Task FetchUserAsync()
        {
            _user = await _userRepo.Manager.GetUserAsync(User);

            if (_user is null)
            {
                // TODO: redirect to error page
                throw new NotImplementedException();
            }
            if (_user.Teacher is null)
            {
                // TODO: redirect to error page
                throw new NotImplementedException();
            }
        }
    }
}
