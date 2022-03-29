using SchoolAssistant.DAL.Models.Shared;

namespace SchoolAssistant.DAL.Models.Semesters
{
    public class Semester : DbEntity
    {
        public short Year { get; set; }

        public bool Current { get; set; }

    }
}
