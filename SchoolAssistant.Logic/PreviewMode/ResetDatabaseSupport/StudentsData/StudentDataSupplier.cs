using SchoolAssistant.DAL.Models.StudentsParents;

namespace SchoolAssistant.Logic.PreviewMode.ResetDatabaseSupport.StudentsData
{
    public interface IStudentDataSupplier
    {
        Student Yearly { get; }
        StudentRegisterRecord Record { get; }
    }

    public class StudentDataSupplier : IStudentDataSupplier
    {
        public Student Yearly { get; }
        public StudentRegisterRecord Record { get; }

        public StudentDataSupplier(
            Student yearly,
            StudentRegisterRecord record)
        {
            Yearly = yearly;
            Record = record;
        }
    }
}
