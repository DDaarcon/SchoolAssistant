using SchoolAssistant.DAL.Enums;
using SchoolAssistant.Infrastructure.Interfaces.Permissions;
using System.Reflection;

namespace SchoolAssistant.DAL.Attributes
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class UserTypeAttribute : Attribute, IPermissions
    {
        public string? RoleName { get; init; }
        public bool CanAccessConfiguration { get; set; }
        public bool CanViewAllClassesData { get; set; } = false;

        public void CopyPermissionsTo(IPermissions dest)
        {
            Type sourceType = typeof(IPermissions);

            var properties = sourceType.GetProperties();
            foreach (PropertyInfo property in properties)
            {
                property.SetValue(dest, property.GetValue(this));
            }
        }
    }

    public static class UserTypeHelper
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

            var attributes = members
                .Select(member => member
                    .GetCustomAttributes(typeof(UserTypeAttribute), false)
                    .OfType<UserTypeAttribute?>()
                    .FirstOrDefault());
            return attributes.Where(x => x is not null)!;
        }
    }
}
