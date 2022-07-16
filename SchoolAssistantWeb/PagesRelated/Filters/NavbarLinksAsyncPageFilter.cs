using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using SchoolAssistant.DAL.Enums;
using SchoolAssistant.DAL.Models.AppStructure;
using SchoolAssistant.DAL.Repositories;

namespace SchoolAssistant.Web.PagesRelated.Filters
{
    public class NavbarLinksAsyncPageFilter : IAsyncPageFilter
    {
        private readonly IUserRepository _userRepo;

        public NavbarLinksAsyncPageFilter(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }

        private User _user = null!;
        private ViewDataDictionary _vd = null!;

        public async Task OnPageHandlerExecutionAsync(PageHandlerExecutingContext context, PageHandlerExecutionDelegate next)
        {
            await next!.Invoke().ConfigureAwait(false);
        }

        public async Task OnPageHandlerSelectionAsync(PageHandlerSelectedContext context)
        {
            if (context is null
                || context.HandlerInstance is not PageModel model
                || model.ViewData is null
                || !await TryFetchUserAsync().ConfigureAwait(false))
            {
                return;
            }

            _vd = model.ViewData;

            NavigationLinks();
        }

        private async Task<bool> TryFetchUserAsync()
        {
            _user = (await _userRepo.GetCurrentUserCachedAsync().ConfigureAwait(false))!;
            return _user is not null;
        }



        private void NavigationLinks()
        {
            IEnumerable<string> toEnable = _Empty;

            if ((new[] { UserType.Student, UserType.Parent }).Contains(_user.Type))
            {
                AddTruths(toEnable);
                return;
            }

            toEnable = toEnable.Concat(_user.Type switch
            {
                UserType.Teacher => Take(
                    ViewDataHelper.EnableScheduleArranger.Label),
                UserType.Administration => Take(
                    ViewDataHelper.EnableDataManagement.Label,
                    ViewDataHelper.EnableScheduleArranger.Label,
                    ViewDataHelper.EnableUsersList.Label,
                    ViewDataHelper.EnableUsersManagement.Label),
                UserType.Headmaster => Take(
                    ViewDataHelper.EnableDataManagement.Label,
                    ViewDataHelper.EnableScheduleArranger.Label,
                    ViewDataHelper.EnableUsersList.Label,
                    ViewDataHelper.EnableUsersManagement.Label),
                UserType.SystemAdmin => Take(
                    ViewDataHelper.EnableDataManagement.Label,
                    ViewDataHelper.EnableScheduleArranger.Label,
                    ViewDataHelper.EnableUsersList.Label,
                    ViewDataHelper.EnableUsersManagement.Label),
                _ => throw new NotImplementedException(),
            });

            AddTruths(toEnable);
        }




        private void AddTruths(IEnumerable<string> labels)
        {
            foreach (var label in labels)
            {
                _vd[label] = true;
            }
        }
        private IEnumerable<string> Take(params string[] labels) => labels;
        private IEnumerable<string> _Empty => Enumerable.Empty<string>();
    }
}
