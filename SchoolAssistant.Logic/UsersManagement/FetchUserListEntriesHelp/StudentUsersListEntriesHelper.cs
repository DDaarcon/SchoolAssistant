using SchoolAssistant.DAL.Enums;
using SchoolAssistant.DAL.Interfaces;
using SchoolAssistant.DAL.Models.AppStructure;
using SchoolAssistant.DAL.Models.StudentsParents;
using SchoolAssistant.Infrastructure.Models.UsersManagement;

namespace SchoolAssistant.Logic.UsersManagement.FetchUserListEntriesHelp
{
    public class StudentUsersListEntriesHelper : IUserListEntriesHelper
    {
        public UserType DbType => UserType.Student;

        public Func<UserPersonModel, UserListEntryJson> CreateEntry => model =>
        {
            string orgClassName = null!;

            var registerRecord = model.info as StudentRegisterRecord;
            if (registerRecord is not null)
            {
                var currentYearStudent = registerRecord.StudentInstances.FirstOrDefault(x => x.SchoolYear.Current);
                orgClassName = currentYearStudent?.OrganizationalClass?.Name!;
            }

            return new StudentUsersListEntryJson
            {
                id = model.user.Id,
                userName = model.user.UserName,
                firstName = model.info.FirstName,
                lastName = model.info.LastName,
                email = model.user.Email,
                orgClass = orgClassName
            };
        };

        public IPerson GetInfo(User user)
        {
            return user.Student!;
        }
    }
}
