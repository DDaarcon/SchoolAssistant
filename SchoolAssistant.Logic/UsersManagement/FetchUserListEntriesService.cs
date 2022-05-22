using SchoolAssistant.Infrastructure.Models.UsersManagement;

namespace SchoolAssistant.Logic.UsersManagement
{
    public interface IFetchUserListEntriesService
    {
        Task<UserListEntryModel[]?> FetchAsync(FetchUsersListModel model);
    }

    [Injectable]
    public class FetchUserListEntriesService : IFetchUserListEntriesService
    {

        private FetchUsersListModel _model = null!;

        public async Task<UserListEntryModel[]?> FetchAsync(FetchUsersListModel model)
        {
            _model = model;
            return null;
        }
    }
}
