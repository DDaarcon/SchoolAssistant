using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SchoolAssistant.Infrastructure.Enums.PreviewHelper;
using SchoolAssistant.Logic.PreviewMode;
using SchoolAssistant.Web.Areas.Identity.Pages.Account;

namespace SchoolAssistant.Web.PagesRelated.Filters
{
    public class ControlPreviewModeAsyncPageFilter : IAsyncPageFilter
    {
        private readonly IControlPreviewModeService _controlSvc;

        public ControlPreviewModeAsyncPageFilter(
            IControlPreviewModeService controlSvc)
        {
            _controlSvc = controlSvc;
        }

        public Task OnPageHandlerExecutionAsync(PageHandlerExecutingContext context, PageHandlerExecutionDelegate next)
        {
            return next!.Invoke();
        }

        public Task OnPageHandlerSelectionAsync(PageHandlerSelectedContext context)
        {
            if (context is null
                || context.HandlerInstance is not PageModel model
                || model.ViewData is null)
            {
                return Task.CompletedTask;
            }

            if (model is LoginModel)
            {
                model.ViewData[ViewDataHelper.PreviewMenuType.Label] = PreviewMenuType.LoginMenu;
            }

            model.ViewData[ViewDataHelper.IsPreviewModeOn.Label] = _controlSvc.IsEnabled;

            return Task.CompletedTask;
        }
    }
}
