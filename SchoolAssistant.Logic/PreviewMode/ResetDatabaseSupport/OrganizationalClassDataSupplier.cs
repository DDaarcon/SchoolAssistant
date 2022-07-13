using SchoolAssistant.DAL.Models.StudentsOrganization;

namespace SchoolAssistant.Logic.PreviewMode.ResetDatabaseSupport
{
    public interface IOrganizationalClassDataSupplier
    {
        OrganizationalClass[] All { get; }
        OrganizationalClass Class1a { get; }
        OrganizationalClass Class1b { get; }
        OrganizationalClass Class2 { get; }
        OrganizationalClass Class3a { get; }
        OrganizationalClass Class3b { get; }
        OrganizationalClass Class3c { get; }

        void InitializeData();
    }

    [Injectable]
    public class OrganizationalClassDataSupplier : IOrganizationalClassDataSupplier
    {
        private readonly ITeachersDataSupplier _teachersDataSupplier;
        private readonly ISchoolYearDataSupplier _schoolYearDataSupplier;


        public OrganizationalClassDataSupplier(
            ITeachersDataSupplier teachersDataSupplier,
            ISchoolYearDataSupplier schoolYearDataSupplier)
        {
            _teachersDataSupplier = teachersDataSupplier;
            _schoolYearDataSupplier = schoolYearDataSupplier;
        }


        public OrganizationalClass[] All { get; private set; } = null!;

        public OrganizationalClass Class1a { get; private set; } = null!;
        public OrganizationalClass Class1b { get; private set; } = null!;
        public OrganizationalClass Class2 { get; private set; } = null!;
        public OrganizationalClass Class3a { get; private set; } = null!;
        public OrganizationalClass Class3b { get; private set; } = null!;
        public OrganizationalClass Class3c { get; private set; } = null!;


        private bool _areInitialized = false;
        public void InitializeData()
        {
            if (_areInitialized) return;

            Class1a = new()
            {
                Grade = 1,
                Distinction = "a",
                SchoolYearId = _schoolYearDataSupplier.Current.Id,
                SupervisorId = _teachersDataSupplier.AllExceptSample.FirstOrDefault()?.Id
            };

            Class1b = new()
            {
                Grade = 1,
                Distinction = "b",
                SchoolYearId = _schoolYearDataSupplier.Current.Id,
                SupervisorId = _teachersDataSupplier.AllExceptSample.Skip(1).FirstOrDefault()?.Id
            };

            Class2 = new()
            {
                Grade = 2,
                SchoolYearId = _schoolYearDataSupplier.Current.Id,
                SupervisorId = _teachersDataSupplier.AllExceptSample.Skip(2).FirstOrDefault()?.Id
            };

            Class3a = new()
            {
                Grade = 3,
                Distinction = "a",
                SchoolYearId = _schoolYearDataSupplier.Current.Id,
                SupervisorId = _teachersDataSupplier.AllExceptSample.Skip(3).FirstOrDefault()?.Id
            };

            Class3b = new()
            {
                Grade = 3,
                Distinction = "b",
                SchoolYearId = _schoolYearDataSupplier.Current.Id,
                SupervisorId = _teachersDataSupplier.AllExceptSample.Skip(4).FirstOrDefault()?.Id
            };

            Class3c = new()
            {
                Grade = 3,
                Distinction = "c",
                Specialization = "Informatyk",
                SchoolYearId = _schoolYearDataSupplier.Current.Id,
                SupervisorId = _teachersDataSupplier.AllExceptSample.Skip(5).FirstOrDefault()?.Id
            };


            All = new[]
            {
                Class1a,
                Class1b,
                Class2,
                Class3a,
                Class3b,
                Class3c,
            };

            _areInitialized = true;
        }
    }
}
