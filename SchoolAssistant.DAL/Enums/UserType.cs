using SchoolAssistant.DAL.Attributes;

namespace SchoolAssistant.DAL.Enums
{
    public enum UserType
    {
        [UserType(RoleName = "Student")]
        Student,
        [UserType(RoleName = "Teacher")]
        Teacher,
        [UserType(RoleName = "Administration")]
        Administration,
        [UserType(RoleName = "Headmaster")]
        Headmaster,
        [UserType(RoleName = "SystemAdmin")]
        SystemAdmin
    }
}
