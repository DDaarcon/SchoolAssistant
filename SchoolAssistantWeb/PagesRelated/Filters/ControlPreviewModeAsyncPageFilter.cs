using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using SchoolAssistant.DAL.Repositories;
using SchoolAssistant.Infrastructure.Enums.PreviewHelper;
using SchoolAssistant.Infrastructure.Models.PreviewMode;
using SchoolAssistant.Logic.PreviewMode;
using SchoolAssistant.Web.Areas.Identity.Pages.Account;

namespace SchoolAssistant.Web.PagesRelated.Filters
{
    public class ControlPreviewModeAsyncPageFilter : IAsyncPageFilter
    {
        private readonly IControlPreviewModeService _controlSvc;
        private readonly IConfiguration _config;
        private readonly IUserRepository _userRepo;

        public ControlPreviewModeAsyncPageFilter(
            IControlPreviewModeService controlSvc,
            IConfiguration config,
            IUserRepository userRepo)
        {
            _controlSvc = controlSvc;
            _config = config;
            _userRepo = userRepo;
        }

        public Task OnPageHandlerExecutionAsync(PageHandlerExecutingContext context, PageHandlerExecutionDelegate next)
        {
            return next!.Invoke();
        }

        private ViewDataDictionary _viewData = null!;

        public async Task OnPageHandlerSelectionAsync(PageHandlerSelectedContext context)
        {
            if (context is null
                || context.HandlerInstance is not PageModel model
                || model.ViewData is null)
            {
                return;
            }
            _viewData = model.ViewData;

            if (model is LoginModel)
            {
                await PrepareForLoginPageAsync().ConfigureAwait(false);
            }
            else
                SetUpPreviewMenuTypeOtherThanLogin(model);

            _viewData[ViewDataHelper.IsPreviewModeOn.Label] = _controlSvc.IsEnabled;

            return;
        }

        private async Task PrepareForLoginPageAsync()
        {
            _viewData[ViewDataHelper.PreviewMenuType.Label] = PreviewMenuType.LoginMenu;

            string prefix = "PreviewMode:Logins:";
            var logins = new PreviewLoginsJson
            {
                administratorUserName = _config[$"{prefix}Administrator:UserName"],
                administratorPassword = _config[$"{prefix}Administrator:Password"]
            };

            var teacherUserName = _config[$"{prefix}Teacher:UserName"];
            if (await _userRepo.ExistsAsync(x => x.UserName == teacherUserName).ConfigureAwait(false))
            {
                logins.teacherUserName = teacherUserName;
                logins.teacherPassword = _config[$"{prefix}Teacher:Password"];
            }

            var studentUserName = _config[$"{prefix}Student:UserName"];
            if (await _userRepo.ExistsAsync(x => x.UserName == studentUserName).ConfigureAwait(false))
            {
                logins.studentUserName = studentUserName;
                logins.studentPassword = _config[$"{prefix}Student:Password"];
            }

            _viewData[ViewDataHelper.PreviewMenuLogins.Label] = logins;
        }

        private void SetUpPreviewMenuTypeOtherThanLogin(PageModel model)
        {
            _viewData[ViewDataHelper.PreviewMenuType.Label] = model switch
            {
                Pages.IndexModel => PreviewMenuType.IndexPage,
                Pages.Dashboard.TeacherModel => PreviewMenuType.TeacherDashboard,
                Pages.Dashboard.StudentModel => PreviewMenuType.StudentDashboard,
                Pages.ConductingClasses.ScheduledLessonsModel => PreviewMenuType.ScheduledLessonsPage,
                Pages.DataManagement.DataManagementModel => PreviewMenuType.DataManagementPage,
                Pages.ScheduleArranger.ScheduleArrangerModel => PreviewMenuType.ScheduleArrangerPage,
                Pages.UsersManagement.UsersManagementModel => PreviewMenuType.UserManagementPage,
                Pages.UsersManagement.CreateUserModel => PreviewMenuType.CreateUserPage,
                _ => null
            };
        }
    }
}
