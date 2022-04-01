using NUnit.Framework;
using SchoolAssistant.DAL.Attributes;
using SchoolAssistant.DAL.Enums;
using System;
using System.Linq;

namespace SchoolAssistans.Tests.Attributes.UserTypeAttr
{
    public class UserTypeAttributeTests
    {
        [Test]
        public void CanGetUserTypeAttributeFromValue()
        {
            var student = UserType.Student;

            var attribute = student.GetUserTypeAttribute();

            Assert.IsNotNull(attribute);
            Assert.IsInstanceOf<UserTypeAttribute>(attribute);
            Assert.IsTrue(attribute?.RoleName == "Student");
        }

        [Test]
        public void CanGetAllUserTypeAttributes()
        {
            var attributes = UserTypeHelper.GetUserTypeDescriptions();
            var enums = Enum.GetValues<UserType>();

            Assert.IsNotNull(attributes);
            if (enums.Any())
                Assert.IsNotEmpty(attributes);
            else
            {
                Assert.Pass();
                return;
            }

            var oneAttribute = enums.First().GetUserTypeAttribute();

            Assert.IsTrue(attributes.Any(x => x.RoleName == oneAttribute.RoleName));
        }

        [Test]
        public void CanGetUserTypeAttributeByName()
        {
            var enumVal = UserType.Student;
            var attribute = enumVal.GetUserTypeAttribute();
            var secAttribute = UserTypeHelper.GetUserTypeAttribute(enumVal.ToString());

            Assert.AreEqual(attribute, secAttribute);
        }
    }
}
