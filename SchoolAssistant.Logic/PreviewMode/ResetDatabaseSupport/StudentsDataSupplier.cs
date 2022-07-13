using SchoolAssistant.Logic.PreviewMode.ResetDatabaseSupport.StudentsData;

namespace SchoolAssistant.Logic.PreviewMode.ResetDatabaseSupport
{
    public interface IStudentsDataSupplier
    {

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

        private List<IStudentDataSupplier>? _allExceptSampleBF;
        public IEnumerable<IStudentDataSupplier> AllExceptSample
        {
            get
            {
                if (_allExceptSampleBF is null)
                    GenerateAllExceptSample();
                return _allExceptSampleBF!;
            }
        }

        private IStudentDataSupplier? _sampleStudentBF;
        public IStudentDataSupplier SampleStudent
        {
            get
            {
                if (_sampleStudentBF is null)
                    GenerateAllExceptSample();
                return _sampleStudentBF!;
            }
        }


        private void GenerateAllExceptSample()
        {

            var schoolYearId = _orgClassDataSupplier.Class1a.SchoolYear.Id;
            _faker.SchoolYearId = schoolYearId;

            _allExceptSampleBF = new List<IStudentDataSupplier>();

            _faker.AddRandom(10);
            _faker.ClassId = _orgClassDataSupplier.Class1a.Id;
            _faker.BirthdaysFrom = new DateOnly(2004, 7, 1);
            _faker.BirthdaysTo = new DateOnly(2005, 6, 10);
            _allExceptSampleBF.AddRange(_faker.GetGeneratedStudents());

            _faker.AddRandom(15);
            _faker.ClassId = _orgClassDataSupplier.Class1b.Id;
            _faker.BirthdaysFrom = new DateOnly(2004, 7, 1);
            _faker.BirthdaysTo = new DateOnly(2005, 6, 10);
            _allExceptSampleBF.AddRange(_faker.GetGeneratedStudents());

            _faker.AddRandom(10);
            _faker.ClassId = _orgClassDataSupplier.Class2.Id;
            _faker.BirthdaysFrom = new DateOnly(2003, 7, 1);
            _faker.BirthdaysTo = new DateOnly(2004, 6, 10);
            _allExceptSampleBF.AddRange(_faker.GetGeneratedStudents());

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
            _sampleStudentBF = _faker.GetGeneratedStudents().First();

            _faker.AddRandom(11);
            _faker.ClassId = _orgClassDataSupplier.Class3a.Id;
            _faker.BirthdaysFrom = new DateOnly(2002, 7, 1);
            _faker.BirthdaysTo = new DateOnly(2003, 6, 10);
            _allExceptSampleBF.AddRange(_faker.GetGeneratedStudents());

            _faker.AddRandom(10);
            _faker.ClassId = _orgClassDataSupplier.Class3b.Id;
            _faker.BirthdaysFrom = new DateOnly(2002, 7, 1);
            _faker.BirthdaysTo = new DateOnly(2003, 6, 10);
            _allExceptSampleBF.AddRange(_faker.GetGeneratedStudents());

            _faker.AddRandom(14);
            _faker.ClassId = _orgClassDataSupplier.Class3c.Id;
            _faker.BirthdaysFrom = new DateOnly(2002, 7, 1);
            _faker.BirthdaysTo = new DateOnly(2003, 6, 10);
            _allExceptSampleBF.AddRange(_faker.GetGeneratedStudents());
        }
    }
}
