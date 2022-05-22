using SchoolAssistant.DAL.Enums;
using SchoolAssistant.DAL.Interfaces;
using SchoolAssistant.DAL.Models.AppStructure;
using SchoolAssistant.DAL.Repositories;
using SchoolAssistant.Infrastructure.Enums.Users;
using SchoolAssistant.Infrastructure.Models.UsersManagement;

namespace SchoolAssistant.Logic.UsersManagement
{
    public interface IFetchUserListEntriesService
    {
        Task<UserListEntryModel[]> FetchAsync(FetchUsersListModel model);
    }

    [Injectable]
    public class FetchUserListEntriesService : IFetchUserListEntriesService
    {
        private readonly IUserRepository _userRepo;

        private FetchUsersListModel _model = null!;
        private IEnumerable<UserAndInfo> _users = null!;

        public FetchUserListEntriesService(
            IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }

        public async Task<UserListEntryModel[]> FetchAsync(FetchUsersListModel model)
        {
            _model = model;
            if (model == null) return new UserListEntryModel[0];
            VerifyModel();

            await FetchUsersFromDB();

            FilterQueryByType();
            OrderQueryByLastNameThenFirstName();
            SkipAndTake();

            return SelectEntries();
        }

        private void VerifyModel()
        {
            if (_model.Skip.HasValue && _model.Skip.Value <= 0)
                _model.Skip = null;
            if (_model.Take.HasValue && _model.Take.Value <= 0)
                _model.Take = null;
        }

        private async Task FetchUsersFromDB()
        {
            _users = (await _userRepo.AsListAsync()).Select(x => new UserAndInfo
            {
                user = x,
                info = GetInfo(x)!
            })
                .Where(x => x.info != null);
        }

        private void FilterQueryByType()
        {
            var type = GetDbUserType();
            _users = _users.Where(x => x.user.Type == type);
        }

        private UserType GetDbUserType()
        {
            return _model.OfType switch
            {
                UserTypeForManagement.Student => UserType.Student,
                UserTypeForManagement.Teacher => UserType.Teacher,
                UserTypeForManagement.Parent => UserType.Parent,
                _ => (UserType)(-1)
            };
        }

        private void OrderQueryByLastNameThenFirstName()
        {
            // TODO: Possible null reference
            _users = _users.OrderBy(x =>
                x.info?.LastName ?? null)
                .ThenBy(x =>
                x.info?.FirstName ?? null);
        }

        private IPerson? GetInfo(User user)
        {
            return user.Type == UserType.Student ? user.Student
                : user.Type == UserType.Teacher ? user.Teacher
                : user.Type == UserType.Parent ? user.Parent?.Info
                : null;
        }

        private void SkipAndTake()
        {
            if (_model.Skip.HasValue)
                _users = _users.Skip(_model.Skip.Value);
            if (_model.Take.HasValue)
                _users = _users.Take(_model.Take.Value);
        }

        private UserListEntryModel[] SelectEntries()
        {
            return _users.Select(x => new UserListEntryModel
            {
                FirstName = x.info.FirstName,
                LastName = x.info.LastName,
                UserName = x.user.UserName,
                Email = x.user.Email,
                Type = _model.OfType
            }).ToArray();
        }


        private class UserAndInfo
        {
            public User user { get; set; } = null!;
            public IPerson info { get; set; } = null!;
        }
    }
}
