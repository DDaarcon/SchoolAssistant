using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using SchoolAssistant.DAL.Enums;
using SchoolAssistant.DAL.Attributes;

namespace SchoolAssistans.Tests.Attributes
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
            else {
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
