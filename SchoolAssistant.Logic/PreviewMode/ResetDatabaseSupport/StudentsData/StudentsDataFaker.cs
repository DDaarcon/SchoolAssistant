using Bogus;
using SchoolAssistant.DAL.Models.StudentsParents;

namespace SchoolAssistant.Logic.PreviewMode.ResetDatabaseSupport.StudentsData
{
    public class StudentsDataFaker : List<StudentsDataFakerInput?>, IEnumerable<StudentsDataFakerInput?>
    {
        public long SchoolYearId { get; set; }
        public long ClassId { get; set; }
        public DateOnly BirthdaysFrom { get; set; }
        public DateOnly BirthdaysTo { get; set; }


        private StudentsDataFakerInput? _current = null!;
        private int _index;

        private Student _fakedStudent = null!;
        private StudentRegisterRecord _fakedStudentRegRec = null!;

        private Faker<Student> _studentFaker = null!;
        private Faker<StudentRegisterRecord> _studentRegRecFaker = null!;
        private Faker<ParentRegisterSubrecord> _parentRegSubrecFaker = null!;


        private string? _sharedAddress;

        public void AddRandom(int amount = 1)
        {
            for (int i = 0; i < amount; i++)
                Add(null);
        }

        public IEnumerable<IStudentDataSupplier> GetGeneratedStudents()
        {
            PrepareFakers();

            var generatedAll = new List<IStudentDataSupplier>();
            foreach (var (info, index)
                in this.Select((x, index) => (x, index)))
            {
                _current = info;
                _index = index;

                FakeStudent();
                FakeStudentRegisterRecord();
                FakeFirstParentRegisterSubrecord();
                FakeSecondParentRegisterSubrecord();

                generatedAll.Add(new StudentDataSupplier(
                    _fakedStudent,
                    _fakedStudentRegRec));
            }

            var generatedAllOrdered = generatedAll.OrderBy(x => x.Record.LastName)
                .ThenBy(x => x.Record.FirstName)
                .ThenBy(x => x.Record.DateOfBirth);

            foreach (var (generated, number) in generatedAllOrdered.Select((x, index) => (x, index + 1)))
                generated.Yearly.NumberInJournal = number;

            Clear();

            return generatedAll;
        }

        private void PrepareFakers()
        {
            _studentFaker ??= new Faker<Student>("pl");
            _studentFaker
                .RuleFor(o => o.SchoolYearId, () => SchoolYearId)
                .RuleFor(o => o.OrganizationalClassId, () => ClassId);

            _studentRegRecFaker ??= new Faker<StudentRegisterRecord>("pl");
            _parentRegSubrecFaker ??= new Faker<ParentRegisterSubrecord>("pl");
        }

        private void FakeStudent()
        {
            _studentFaker
                    .RuleFor(o => o.NumberInJournal, f => _index + 1);
            _fakedStudent = _studentFaker.Generate();
        }

        private void FakeStudentRegisterRecord()
        {
            var toAssign = _current?.StudentToOverride;
            if (toAssign is null)
            {
                _studentRegRecFaker
                    .RuleFor(o => o.FirstName, f => _current?.Student?.FirstName ?? f.Name.FirstName())
                    .RuleFor(o => o.SecondName, f => !String.IsNullOrEmpty(_current?.Student?.SecondName)
                        ? _current?.Student?.SecondName
                        : _current?.Student?.SecondName == String.Empty
                            ? f.Name.FirstName()
                            : null)
                    .RuleFor(o => o.LastName, f => _current?.Student?.LastName ?? f.Name.LastName())
                    .RuleFor(o => o.Address, f =>
                    {
                        _sharedAddress = f.Address.FullAddress();

                        return _sharedAddress;
                    })
                    .RuleFor(o => o.DateOfBirth, f => f.Date.BetweenDateOnly(BirthdaysFrom, BirthdaysTo))
                    .RuleFor(o => o.Email, (f, o) => $"{o.FirstName}.{o.LastName}@mail.com")
                    .RuleFor(o => o.PlaceOfBirth, f => f.Address.City())
                    .RuleFor(o => o.PersonalID, f => f.Random.Int().ToString() + f.Random.Int().ToString());

                toAssign = _studentRegRecFaker.Generate();
            }

            _fakedStudent.Info = toAssign;
            _fakedStudentRegRec = toAssign;
        }

        private void FakeFirstParentRegisterSubrecord()
        {
            var toAssign = _current?.FirstParentToOverride;
            if (toAssign is null)
                toAssign = GetFakedParentRegisterSubrecord(_current?.FirstParent);

            _fakedStudentRegRec.FirstParent = toAssign;
        }
        private void FakeSecondParentRegisterSubrecord()
        {
            var toAssign = _current?.SecondParentToOverride;
            if (toAssign is null)
            {
                bool create = _current?.SecondParent?.IsPresent ?? true;
                toAssign = GetFakedParentRegisterSubrecord(_current?.FirstParent);
            }

            _fakedStudentRegRec.SecondParent = toAssign;
        }

        private ParentRegisterSubrecord GetFakedParentRegisterSubrecord(ParentDataFakerInput? info)
        {
            var x = new Faker();
            _parentRegSubrecFaker
                .RuleFor(o => o.FirstName, f => info?.FirstName ?? f.Name.FirstName())
                .RuleFor(o => o.SecondName, f => !String.IsNullOrEmpty(info?.SecondName)
                    ? info?.SecondName
                    : info?.SecondName == String.Empty
                        ? f.Name.FirstName()
                        : null)
                .RuleFor(o => o.LastName, f => info?.LastName ?? f.Name.LastName())
                .RuleFor(o => o.Address, f =>
                {
                    if ((info?.AddressLikeChilds ?? false) || !String.IsNullOrEmpty(_sharedAddress))
                        return f.Address.FullAddress();

                    return _sharedAddress;
                })
                .RuleFor(o => o.Email, (f, o) => $"{o.FirstName}.{o.LastName}@mail.com")
                .RuleFor(o => o.PhoneNumber, f => f.Phone.PhoneNumber());

            return _parentRegSubrecFaker.Generate();
        }
    }

}
