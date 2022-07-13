using SchoolAssistant.Logic.PreviewMode.ResetDatabaseSupport.StudentsData;

namespace SchoolAssistant.Logic.PreviewMode.ResetDatabaseSupport
{
    public interface IStudentsDataSupplier
    {
        IEnumerable<IStudentDataSupplier> AllExceptSample { get; }
        IStudentDataSupplier SampleStudent { get; }

        void InitializeData();
    }

    [Injectable]
    public class StudentsDataSupplier : IStudentsDataSupplier
    {
        private readonly IOrganizationalClassDataSupplier _orgClassDataSupplier;
        private readonly StudentsDataFaker _faker;

        public StudentsDataSupplier(
            IOrganizationalClassDataSupplier orgClassDataSupplier)
        {
            _orgClassDataSupplier = orgClassDataSupplier;
            _faker = new();
        }

        public IEnumerable<IStudentDataSupplier> AllExceptSample { get; private set; } = null!;

        public IStudentDataSupplier SampleStudent { get; private set; } = null!;


        private bool _areInitialized = false;
        public void InitializeData()
        {
            if (_areInitialized)
                return;

            var schoolYearId = _orgClassDataSupplier.Class1a.SchoolYear.Id;
            _faker.SchoolYearId = schoolYearId;

            var allExceptSample = new List<IStudentDataSupplier>();

            _faker.AddRandom(10);
            _faker.ClassId = _orgClassDataSupplier.Class1a.Id;
            _faker.BirthdaysFrom = new DateOnly(2004, 7, 1);
            _faker.BirthdaysTo = new DateOnly(2005, 6, 10);
            allExceptSample.AddRange(_faker.GetGeneratedStudents());

            _faker.AddRandom(15);
            _faker.ClassId = _orgClassDataSupplier.Class1b.Id;
            _faker.BirthdaysFrom = new DateOnly(2004, 7, 1);
            _faker.BirthdaysTo = new DateOnly(2005, 6, 10);
            allExceptSample.AddRange(_faker.GetGeneratedStudents());

            _faker.AddRandom(10);
            _faker.ClassId = _orgClassDataSupplier.Class2.Id;
            _faker.BirthdaysFrom = new DateOnly(2003, 7, 1);
            _faker.BirthdaysTo = new DateOnly(2004, 6, 10);
            allExceptSample.AddRange(_faker.GetGeneratedStudents());

            _faker.Add(new StudentsDataFakerInput(
                Student: new StudentDataFakerInput(
                    FirstName: "Maciej",
                    LastName: "Nowak"),
                FirstParent: new ParentDataFakerInput(
                    FirstName: "Izabela",
                    LastName: "Nowak",
                    AddressLikeChilds: true),
                SecondParent: new SecondParentDataFakerInput(
                    FirstName: "Maria",
                    LastName: "Maciaszek")));
            SampleStudent = _faker.GetGeneratedStudents().First();

            _faker.AddRandom(11);
            _faker.ClassId = _orgClassDataSupplier.Class3a.Id;
            _faker.BirthdaysFrom = new DateOnly(2002, 7, 1);
            _faker.BirthdaysTo = new DateOnly(2003, 6, 10);
            allExceptSample.AddRange(_faker.GetGeneratedStudents());

            _faker.AddRandom(10);
            _faker.ClassId = _orgClassDataSupplier.Class3b.Id;
            _faker.BirthdaysFrom = new DateOnly(2002, 7, 1);
            _faker.BirthdaysTo = new DateOnly(2003, 6, 10);
            allExceptSample.AddRange(_faker.GetGeneratedStudents());

            _faker.AddRandom(14);
            _faker.ClassId = _orgClassDataSupplier.Class3c.Id;
            _faker.BirthdaysFrom = new DateOnly(2002, 7, 1);
            _faker.BirthdaysTo = new DateOnly(2003, 6, 10);
            allExceptSample.AddRange(_faker.GetGeneratedStudents());

            AllExceptSample = allExceptSample;

            _areInitialized = true;
        }
    }
}
