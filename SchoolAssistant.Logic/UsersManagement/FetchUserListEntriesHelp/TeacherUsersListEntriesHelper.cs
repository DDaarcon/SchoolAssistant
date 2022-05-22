using SchoolAssistant.DAL.Enums;
using SchoolAssistant.DAL.Interfaces;
using SchoolAssistant.DAL.Models.AppStructure;
using SchoolAssistant.Infrastructure.Models.UsersManagement;

namespace SchoolAssistant.Logic.UsersManagement.FetchUserListEntriesHelp
{
    public class TeacherUsersListEntriesHelper : IUserListEntriesHelper
    {
        public UserType DbType => UserType.Teacher;

        public Func<UserPersonModel, UserListEntryJson> CreateEntry => model =>
        {
            return new UserListEntryJson
            {
                id = model.user.Id,
                userName = model.user.UserName,
                firstName = model.info.FirstName,
                lastName = model.info.LastName,
                email = model.user.Email
            };
        };

        public IPerson GetInfo(User user)
        {
            return user.Teacher!;
        }
    }
}
