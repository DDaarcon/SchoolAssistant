using SchoolAssistant.DAL.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAssistant.DAL.Attributes
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    internal class UserTypeAttribute : Attribute
    {
        public string? RoleName { get; init; }
    }

    internal static class UserTypeHelper
    {
        public static UserTypeAttribute? GetUserTypeAttribute(string name)
        {
            var type = Enum.Parse(typeof(UserType), name) as UserType?;

            return type?.GetUserTypeAttribute();
        }

        public static UserTypeAttribute? GetUserTypeAttribute(this UserType userType)
        {
            var type = typeof(UserType);
            var memInfo = type.GetMember(userType.ToString());
            var attributes = memInfo[0].GetCustomAttributes(typeof(UserTypeAttribute), false);
            return (attributes.Length > 0) ? (UserTypeAttribute)attributes[0] : null;
        }

        public static IEnumerable<UserTypeAttribute> GetUserTypeDescriptions()
        {
            var enumType = typeof(UserType);
            var members = enumType.GetMembers();

            return members.Select(member => member.GetCustomAttributes(typeof(UserTypeAttribute), true).OfType<UserTypeAttribute>().First());
        }
    }
}
