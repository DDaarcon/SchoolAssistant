using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SchoolAssistant.Logic.ConductingClasses.ConductLesson;

namespace SchoolAssistant.Web.PageFilters
{
    public class EnableLessonConductionPanelAsyncPageFilter : IAsyncPageFilter
    {
        private readonly IModelForLessonConductionPanelService _displayPanelSvc;

        public EnableLessonConductionPanelAsyncPageFilter(
            IModelForLessonConductionPanelService displayPanelSvc)
        {
            _displayPanelSvc = displayPanelSvc;
        }

        public async Task OnPageHandlerExecutionAsync(PageHandlerExecutingContext context, PageHandlerExecutionDelegate next)
        {
            await next!.Invoke().ConfigureAwait(false);
        }

        public async Task OnPageHandlerSelectionAsync(PageHandlerSelectedContext context)
        {
            if (context is not null && context.HandlerInstance is PageModel model)
            {
                if (await _displayPanelSvc.CheckSessionStateForLessonIdAsync().ConfigureAwait(false))
                {
                    if (model.ViewData[_displayPanelSvc.ViewDataKey] is null)
                    {
                        model.ViewData[_displayPanelSvc.ViewDataKey] = await _displayPanelSvc.GetModelAsync().ConfigureAwait(false);
                    }
                }
                else
                {
                    model.ViewData[_displayPanelSvc.ViewDataKey] = null;
                }
            }
        }
    }
}
