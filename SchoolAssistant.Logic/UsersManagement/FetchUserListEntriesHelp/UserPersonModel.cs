using SchoolAssistant.DAL.Interfaces;
using SchoolAssistant.DAL.Models.AppStructure;

namespace SchoolAssistant.Logic.UsersManagement.FetchUserListEntriesHelp
{
    public record UserPersonModel(User user, IPerson info);
}
