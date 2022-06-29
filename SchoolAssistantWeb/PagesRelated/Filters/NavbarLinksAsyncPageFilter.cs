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
                    ViewDataHelper.EnableScheduleArrangerLabel,
                    ViewDataHelper.EnableUsersListLabel),
                UserType.Administration => Take(
                    ViewDataHelper.EnableDataManagementLabel,
                    ViewDataHelper.EnableScheduleArrangerLabel,
                    ViewDataHelper.EnableUsersListLabel,
                    ViewDataHelper.EnableUsersManagementLabel),
                UserType.Headmaster => Take(
                    ViewDataHelper.EnableDataManagementLabel,
                    ViewDataHelper.EnableScheduleArrangerLabel,
                    ViewDataHelper.EnableUsersListLabel,
                    ViewDataHelper.EnableUsersManagementLabel),
                UserType.SystemAdmin => Take(
                    ViewDataHelper.EnableDataManagementLabel,
                    ViewDataHelper.EnableScheduleArrangerLabel,
                    ViewDataHelper.EnableUsersListLabel,
                    ViewDataHelper.EnableUsersManagementLabel),
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
