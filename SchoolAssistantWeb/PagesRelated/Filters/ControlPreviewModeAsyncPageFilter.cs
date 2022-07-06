﻿using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SchoolAssistant.Logic.PreviewMode;

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

            model.ViewData[ViewDataHelper.IsPreviewModeOnLabel] = _controlSvc.IsEnabled;

            return Task.CompletedTask;
        }
    }
}