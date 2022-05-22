using SchoolAssistant.DAL.Repositories;
using SchoolAssistant.Infrastructure.Enums.Users;
using SchoolAssistant.Infrastructure.Models.UsersManagement;
using SchoolAssistant.Logic.UsersManagement.FetchUserListEntriesHelp;

namespace SchoolAssistant.Logic.UsersManagement
{
    public interface IFetchUserListEntriesService
    {
        Task<UserListEntryJson[]> FetchAsync(FetchUsersListRequestJson model);
    }

    [Injectable]
    public class FetchUserListEntriesService : IFetchUserListEntriesService
    {
        private readonly IUserRepository _userRepo;

        private FetchUsersListRequestJson _model = null!;
        private IEnumerable<UserPersonModel> _users = null!;

        private IUserListEntriesHelper _helper = null!;

        public FetchUserListEntriesService(
            IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }

        public async Task<UserListEntryJson[]> FetchAsync(FetchUsersListRequestJson model)
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
            if (!Enum.IsDefined(typeof(UserTypeForManagement), _model.ofType)) return false;
            return true;
        }

        private void RemoveNegativeSkipAndTake()
        {
            if (_model.skip.HasValue && _model.skip.Value <= 0)
                _model.skip = null;
            if (_model.take.HasValue && _model.take.Value <= 0)
                _model.take = null;
        }

        private void CreateHelper()
        {
            _helper = _model.ofType switch
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
            if (_model.skip.HasValue)
                _users = _users.Skip(_model.skip.Value);
            if (_model.take.HasValue)
                _users = _users.Take(_model.take.Value);
        }

        private UserListEntryJson[] SelectEntries()
        {
            return _users.Select(_helper.CreateEntry).ToArray();
        }
    }
}
