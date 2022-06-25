using SchoolAssistant.DAL.Enums;
using SchoolAssistant.DAL.Models.AppStructure;

namespace SchoolAssistant.Logic
{
    public static class UserExt
    {

        public static bool IsOfType(this User? user, UserType type)
        {
            if (user is null || user.Type != type)
                return false;

            return type switch
            {
                UserType.Student => user.Student is not null,
                UserType.Teacher => user.Teacher is not null,
                UserType.Administration => true,
                UserType.Headmaster => true,
                UserType.SystemAdmin => true,
                UserType.Parent => user.Parent is not null,
                _ => false
            };
        }
    }
}
