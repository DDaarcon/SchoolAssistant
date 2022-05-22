using SchoolAssistant.DAL.Enums;
using SchoolAssistant.DAL.Interfaces;
using SchoolAssistant.DAL.Models.AppStructure;
using SchoolAssistant.Infrastructure.Models.UsersManagement;

namespace SchoolAssistant.Logic.UsersManagement.FetchUserListEntriesHelp
{
    internal interface IUserListEntriesHelper
    {
        IPerson GetInfo(User user);
        UserType DbType { get; }
        Func<UserPersonModel, UserListEntryJson> CreateEntry { get; }
    }
}
