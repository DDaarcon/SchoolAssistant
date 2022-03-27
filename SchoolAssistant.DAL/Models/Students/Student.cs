using SchoolAssistant.DAL.Interfaces;
using SchoolAssistant.DAL.Models.Marks;
using SchoolAssistant.DAL.Models.Shared;

namespace SchoolAssistant.DAL.Models.Students
{
    public class Student : SemesterDbEntity, IPerson
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public ICollection<Mark> Marks { get; set; } = new List<Mark>();
    }
}
