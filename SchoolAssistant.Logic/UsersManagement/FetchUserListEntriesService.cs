using SchoolAssistant.DAL.Repositories;
using SchoolAssistant.Infrastructure.Enums.Users;
using SchoolAssistant.Infrastructure.Models.UsersManagement;
using SchoolAssistant.Logic.UsersManagement.FetchUserListEntriesHelp;

namespace SchoolAssistant.Logic.UsersManagement
{
    public interface IFetchUserListEntriesService
    {
        Task<UserListEntryJson[]> FetchAsync(FetchUsersListModel model);
    }

    [Injectable]
    public class FetchUserListEntriesService : IFetchUserListEntriesService
    {
        private readonly IUserRepository _userRepo;

        private FetchUsersListModel _model = null!;
        private IEnumerable<UserPersonModel> _users = null!;

        private IUserListEntriesHelper _helper = null!;

        public FetchUserListEntriesService(
            IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }

        public async Task<UserListEntryJson[]> FetchAsync(FetchUsersListModel model)
        {
            _model = model;
            if (!ValidateModel()) return new UserListEntryJson[0];
            RemoveNegativeSkipAndTake();

            CreateHelper();

            await FetchUsersFromDB();

            FilterQueryByType();
            OrderQueryByLastNameThenFirstName();
            SkipAndTake();

            return SelectEntries();
        }

        private bool ValidateModel()
        {
            if (_model == null) return false;
            if (!Enum.IsDefined(typeof(UserTypeForManagement), _model.OfType)) return false;
            return true;
        }

        private void RemoveNegativeSkipAndTake()
        {
            if (_model.Skip.HasValue && _model.Skip.Value <= 0)
                _model.Skip = null;
            if (_model.Take.HasValue && _model.Take.Value <= 0)
                _model.Take = null;
        }

        private void CreateHelper()
        {
            _helper = _model.OfType switch
            {
                UserTypeForManagement.Student => new StudentUsersListEntriesHelper(),
                UserTypeForManagement.Teacher => new TeacherUsersListEntriesHelper(),
                UserTypeForManagement.Administration => throw new NotImplementedException(),
                UserTypeForManagement.Headmaster => throw new NotImplementedException(),
                UserTypeForManagement.SystemAdmin => throw new NotImplementedException(),
                UserTypeForManagement.Parent => throw new NotImplementedException(),
                _ => throw new NotImplementedException(),
            };
        }

        private async Task FetchUsersFromDB()
        {
            _users = (await _userRepo.AsListAsync()).Select(x => new UserPersonModel(x, _helper.GetInfo(x)))
                .Where(x => x.info != null);
        }

        private void FilterQueryByType()
        {
            _users = _users.Where(x => x.user.Type == _helper.DbType);
        }

        private void OrderQueryByLastNameThenFirstName()
        {
            _users = _users.OrderBy(x =>
                x.info?.LastName ?? null)
                .ThenBy(x =>
                x.info?.FirstName ?? null);
        }

        private void SkipAndTake()
        {
            if (_model.Skip.HasValue)
                _users = _users.Skip(_model.Skip.Value);
            if (_model.Take.HasValue)
                _users = _users.Take(_model.Take.Value);
        }

        private UserListEntryJson[] SelectEntries()
        {
            return _users.Select(_helper.CreateEntry).ToArray();
        }
    }
}
