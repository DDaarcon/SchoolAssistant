using SchoolAssistant.DAL.Models.Staff;
using SchoolAssistant.DAL.Models.Subjects;

namespace SchoolAssistant.Logic.PreviewMode.ResetDatabaseSupport
{
    public interface ITeachersDataSupplier
    {
        Teacher SampleTeacher { get; }
        IList<Teacher> AllExceptSample { get; }
    }

    [Injectable]
    public class TeachersDataSupplier : ITeachersDataSupplier
    {
        private readonly ISubjectsDataSupplier _subjectsDataSupplier;

        public TeachersDataSupplier(
            ISubjectsDataSupplier subjectsDataSupplier)
        {
            _subjectsDataSupplier = subjectsDataSupplier;

            foreach (var subject in _subjectsDataSupplier.SampleTeacherMain)
                SampleTeacher.SubjectOperations.AddNewlyCreatedMain(subject);

            foreach (var subject in _subjectsDataSupplier.SampleTeacherAdditional)
                SampleTeacher.SubjectOperations.AddNewlyCreatedAdditional(subject);

            CreateAll();
        }


        public Teacher SampleTeacher { get; } = new Teacher
        {
            FirstName = "Jan",
            SecondName = "Krzysztof",
            LastName = "Kowalski",
            PhoneNumber = "+48111111111",
            Email = "sample.teacher@mail.com",
        };



        public IList<Teacher> AllExceptSample { get; } = new List<Teacher>();

        private void CreateAll()
        {
            CreateOneWithOnlyMain(
                "Mariusz",
                "Radosław",
                "Nowak",
                "+48312321343",
                _subjectsDataSupplier.Math);

            CreateOneWithOnlyMain(
                "Tomasz",
                null,
                "Wolak",
                "+48839039532",
                _subjectsDataSupplier.History);

            CreateOneWithMainAndAdditional(
                "Grzegorz",
                "Maciej",
                "Bieniek",
                "+48904839245",
                _subjectsDataSupplier.Polish,
                _subjectsDataSupplier.History);

            CreateOneWithOnlyMain(
                "Joanna",
                null,
                "Toczek",
                "+48398402934",
                _subjectsDataSupplier.Biology,
                _subjectsDataSupplier.Chemistry);

            CreateOneWithOnlyMain(
                "Elżbieta",
                "Maria",
                "Krawczyk",
                "+48309485930",
                _subjectsDataSupplier.English);
        }

        private void CreateOneWithOnlyMain(
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
        }

        private void CreateOne(
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
        }

        private void CreateOneWithMainAndAdditional(
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
