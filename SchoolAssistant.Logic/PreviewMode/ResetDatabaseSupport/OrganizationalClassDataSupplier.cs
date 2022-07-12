using SchoolAssistant.DAL.Models.StudentsOrganization;
using SchoolAssistant.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        }


        public OrganizationalClass[] All { get; }

        public OrganizationalClass Class1a { get; }
        public OrganizationalClass Class1b { get; }
        public OrganizationalClass Class2 { get; }
        public OrganizationalClass Class3a { get; }
        public OrganizationalClass Class3b { get; }
        public OrganizationalClass Class3c { get; }
    }
}
