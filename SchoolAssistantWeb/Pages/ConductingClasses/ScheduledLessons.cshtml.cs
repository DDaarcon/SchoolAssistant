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
        private readonly IScheduledLessonListService _scheduledLessonsListSvc;

        private User _user = null!;
        public ScheduledLessonListModel ScheduledLessonListModel { get; set; } = null!;

        public ScheduledLessonsModel(
            IUserRepository userRepo,
            IScheduledLessonListService scheduledLessonsListSvc)
        {
            _userRepo = userRepo;
            _scheduledLessonsListSvc = scheduledLessonsListSvc;
        }

        public async Task OnGetAsync()
        {
            await FetchUserAsync();

            ScheduledLessonListModel = (await _scheduledLessonsListSvc.GetModelForTeacherAsync(_user.TeacherId!.Value, new FetchScheduledLessonListModel
            {
                From = DateTime.Now.AddDays(-1),
                LimitTo = 30
            }))!;
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
