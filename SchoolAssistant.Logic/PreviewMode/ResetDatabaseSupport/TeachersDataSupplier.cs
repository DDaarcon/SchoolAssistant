using SchoolAssistant.DAL.Models.Staff;
using SchoolAssistant.DAL.Models.Subjects;

namespace SchoolAssistant.Logic.PreviewMode.ResetDatabaseSupport
{
    public interface ITeachersDataSupplier
    {
        Teacher SampleTeacher { get; }
        IList<Teacher> AllExceptSample { get; }

        Teacher Physics2 { get; }
        Teacher Physics1 { get; }
        Teacher PolishEnglish1 { get; }
        Teacher German1 { get; }
        Teacher Math1 { get; }
        Teacher History1 { get; }
        Teacher PolishHistory1 { get; }
        Teacher BiologyChemistry1 { get; }
        Teacher English1 { get; }
        Teacher Math2 { get; }
        Teacher Biology1 { get; }
        Teacher Chemistry1 { get; }

        void InitializeData();
    }

    [Injectable]
    public class TeachersDataSupplier : ITeachersDataSupplier
    {
        private readonly ISubjectsDataSupplier _subjectsDataSupplier;

        public TeachersDataSupplier(
            ISubjectsDataSupplier subjectsDataSupplier)
        {
            _subjectsDataSupplier = subjectsDataSupplier;

            InitializeData();
        }



        public IList<Teacher> AllExceptSample { get; private set; } = null!;
        public Teacher SampleTeacher { get; private set; } = null!;

        public Teacher Math1 { get; private set; } = null!;
        public Teacher History1 { get; private set; } = null!;
        public Teacher PolishHistory1 { get; private set; } = null!;
        public Teacher BiologyChemistry1 { get; private set; } = null!;
        public Teacher English1 { get; private set; } = null!;
        public Teacher Math2 { get; private set; } = null!;
        public Teacher German1 { get; private set; } = null!;
        public Teacher PolishEnglish1 { get; private set; } = null!;
        public Teacher Physics1 { get; private set; } = null!;
        public Teacher Physics2 { get; private set; } = null!;
        public Teacher Biology1 { get; private set; } = null!;
        public Teacher Chemistry1 { get; private set; } = null!;



        private bool _areInitialized = false;
        public void InitializeData()
        {
            if (_areInitialized)
                return;

            AllExceptSample = new List<Teacher>();

            Math1 = CreateOneWithOnlyMain(
                "Mariusz",
                "Radosław",
                "Nowak",
                "+48312321343",
                _subjectsDataSupplier.Math);

            History1 = CreateOneWithOnlyMain(
                "Tomasz",
                null,
                "Wolak",
                "+48839039532",
                _subjectsDataSupplier.History);

            PolishHistory1 = CreateOneWithMainAndAdditional(
                "Grzegorz",
                "Maciej",
                "Bieniek",
                "+48904839245",
                _subjectsDataSupplier.Polish,
                _subjectsDataSupplier.History);

            BiologyChemistry1 = CreateOneWithOnlyMain(
                "Joanna",
                null,
                "Toczek",
                "+48398402934",
                _subjectsDataSupplier.Biology,
                _subjectsDataSupplier.Chemistry);

            English1 = CreateOneWithOnlyMain(
                "Elżbieta",
                "Maria",
                "Krawczyk",
                "+48309132330",
                _subjectsDataSupplier.English);

            Math2 = CreateOneWithOnlyMain(
                "Marzena",
                null,
                "Borowik",
                "+48304343525",
                _subjectsDataSupplier.Math);

            German1 = CreateOneWithOnlyMain(
                "Katarzyna",
                null,
                "Wróżba",
                "+48304723893",
                _subjectsDataSupplier.German);

            PolishEnglish1 = CreateOneWithOnlyMain(
                "Tomasz",
                null,
                "Siedlarz",
                "+48354345930",
                _subjectsDataSupplier.Polish,
                _subjectsDataSupplier.English);

            Physics1 = CreateOneWithOnlyMain(
                "Renata",
                null,
                "Kuc",
                "+48345485930",
                _subjectsDataSupplier.Physics);

            Physics2 = CreateOneWithOnlyMain(
                "Grzegorz",
                null,
                "Cierkowski",
                "+48309485478",
                _subjectsDataSupplier.Physics);

            Biology1 = CreateOneWithOnlyMain(
                "Jolanta",
                null,
                "Karczyk",
                "+48544585930",
                _subjectsDataSupplier.Biology);

            Chemistry1 = CreateOneWithOnlyMain(
                "Urszula",
                "Maria",
                "Kamyk",
                "+48309486546",
                _subjectsDataSupplier.Chemistry);

            SampleTeacher = new Teacher
            {
                FirstName = "Jan",
                SecondName = "Krzysztof",
                LastName = "Kowalski",
                PhoneNumber = "+48111111111",
                Email = "sample.teacher@mail.com",
            };

            foreach (var subject in _subjectsDataSupplier.SampleTeacherMain)
                SampleTeacher.SubjectOperations.AddNewlyCreatedMain(subject);

            foreach (var subject in _subjectsDataSupplier.SampleTeacherAdditional)
                SampleTeacher.SubjectOperations.AddNewlyCreatedAdditional(subject);

            _areInitialized = true;
        }

        private Teacher CreateOneWithOnlyMain(
            string firstName,
            string? secondName,
            string lastName,
            string phoneNumber,
            params Subject[] mainSubjects)
        {
            Teacher tempTeacher = TeacherBaseInfo(firstName, secondName, lastName, phoneNumber);

            foreach (var subject in mainSubjects)
                tempTeacher.SubjectOperations.AddNewlyCreatedMain(subject);

            AllExceptSample.Add(tempTeacher);
            return tempTeacher;
        }

        private Teacher CreateOne(
            string firstName,
            string? secondName,
            string lastName,
            string phoneNumber,
            Subject[] mainSubjects,
            Subject[] additionalSubjects)
        {
            Teacher tempTeacher = TeacherBaseInfo(firstName, secondName, lastName, phoneNumber);

            foreach (var subject in mainSubjects)
                tempTeacher.SubjectOperations.AddNewlyCreatedMain(subject);

            foreach (var subject in additionalSubjects)
                tempTeacher.SubjectOperations.AddNewlyCreatedAdditional(subject);

            AllExceptSample.Add(tempTeacher);
            return tempTeacher;
        }

        private Teacher CreateOneWithMainAndAdditional(
            string firstName,
            string? secondName,
            string lastName,
            string phoneNumber,
            Subject mainSubject,
            Subject additionalSubject)
        {
            Teacher tempTeacher = TeacherBaseInfo(firstName, secondName, lastName, phoneNumber);

            tempTeacher.SubjectOperations.AddNewlyCreatedMain(mainSubject);
            tempTeacher.SubjectOperations.AddNewlyCreatedAdditional(additionalSubject);

            AllExceptSample.Add(tempTeacher);
            return tempTeacher;
        }

        private Teacher TeacherBaseInfo(
            string firstName,
            string? secondName,
            string lastName,
            string phoneNumber)
        {
            return new()
            {
                FirstName = firstName,
                SecondName = secondName,
                LastName = lastName,
                PhoneNumber = phoneNumber,
                Email = $"{firstName}.{lastName}@mail.com"
            };
        }
    }
}
