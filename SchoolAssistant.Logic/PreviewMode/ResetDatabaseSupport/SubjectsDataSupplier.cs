using SchoolAssistant.DAL.Models.Subjects;

namespace SchoolAssistant.Logic.PreviewMode.ResetDatabaseSupport
{
    public interface ISubjectsDataSupplier
    {
        Subject[] All { get; }
        Subject[] SampleTeacherMain { get; }
        Subject[] SampleTeacherAdditional { get; }
        Subject Math { get; }
        Subject Polish { get; }
        Subject English { get; }
        Subject German { get; }
        Subject Biology { get; }
        Subject History { get; }
        Subject ComputerScience { get; }
        Subject Physics { get; }
        Subject Chemistry { get; }
    }

    [Injectable]
    public class SubjectsDataSupplier : ISubjectsDataSupplier
    {

        public Subject[] All { get; }

        public Subject[] SampleTeacherMain { get; }
        public Subject[] SampleTeacherAdditional { get; }

        public Subject Math { get; } = new()
        {
            Name = "Matematyka"
        };
        public Subject Polish { get; } = new()
        {
            Name = "Język polski"
        };
        public Subject English { get; } = new()
        {
            Name = "Język angielski"
        };
        public Subject German { get; } = new()
        {
            Name = "Język niemiecki"
        };
        public Subject Biology { get; } = new()
        {
            Name = "Biologia"
        };
        public Subject History { get; } = new()
        {
            Name = "Historia"
        };
        public Subject ComputerScience { get; } = new()
        {
            Name = "Informatyka"
        };
        public Subject Physics { get; } = new()
        {
            Name = "Fizyka"
        };
        public Subject Chemistry { get; } = new()
        {
            Name = "Chemia"
        };


        public SubjectsDataSupplier()
        {
            All = new[]
            {
                Math,
                Polish,
                English,
                German,
                Biology,
                History,
                ComputerScience,
                Physics,
                Chemistry
            };

            SampleTeacherMain = new[]
            {
                Math,
                ComputerScience
            };
            SampleTeacherAdditional = new[]
            {
                Physics
            };
        }
    }
}
