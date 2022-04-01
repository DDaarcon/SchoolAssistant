using NUnit.Framework;
using SchoolAssistant.DAL.Attributes;
using SchoolAssistant.DAL.Models.AppStructure;

namespace SchoolAssistans.Tests.Attributes.UserTypeAttr
{
    public class CopyingPermissionsTests
    {
        private readonly UserTypeAttribute _typeAttribute = new UserTypeAttribute()
        {
            RoleName = "Test",
            CanViewAllClassesData = false,
            CanAccessConfiguration = true
        };

        [Test]
        public void CopyPermissionsFromAttributeToRole()
        {
            var role = new Role
            {
                Name = "RoleName",
                CanViewAllClassesData = true,
                CanAccessConfiguration = false
            };

            Assert.IsTrue(role.Name == "RoleName");
            Assert.IsTrue(role.CanViewAllClassesData == true);
            Assert.IsTrue(role.CanAccessConfiguration == false);

            _typeAttribute.CopyPermissionsTo(role);

            Assert.IsTrue(role.Name == "RoleName");
            Assert.IsTrue(role.CanViewAllClassesData == false);
            Assert.IsTrue(role.CanAccessConfiguration == true);
        }
    }
}
