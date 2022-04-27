using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bogus;
using SchoolAssistant.DAL.Models.SchoolYears;
using SchoolAssistant.DAL.Models.StudentsOrganization;
using SchoolAssistant.DAL.Models.StudentsParents;
using SchoolAssistant.DAL.Repositories;

namespace SchoolAssistans.Tests.DbEntities
{
    public class FakeData
    {
        private static Faker<ParentRegisterSubrecord> _ParentRegisterSubrecordFaker => new Faker<ParentRegisterSubrecord>()
            .RuleFor(x => x.FirstName, f => f.Name.FirstName())
            .RuleFor(x => x.SecondName, f => f.Name.FirstName())
            .RuleFor(x => x.LastName, f => f.Name.LastName())
            .RuleFor(x => x.PhoneNumber, f => f.Phone.PhoneNumber())
            .RuleFor(x => x.Address, f => f.Address.FullAddress())
            .RuleFor(x => x.Email, (f, reg) => f.Internet.Email(reg.FirstName, reg.LastName));

        private static Faker<StudentRegisterRecord> _StudentRegisterRecordFaker => new Faker<StudentRegisterRecord>()
            .RuleFor(x => x.FirstName, f => f.Name.FirstName())
            .RuleFor(x => x.SecondName, f => f.Name.FirstName())
            .RuleFor(x => x.LastName, f => f.Name.LastName())
            .RuleFor(x => x.PlaceOfBirth, f => f.Address.City())
            .RuleFor(x => x.PersonalID, f => f.UniqueIndex.ToString())
            .RuleFor(x => x.Address, f => f.Address.FullAddress())
            .RuleFor(x => x.DateOfBirth, f => f.Date.BetweenDateOnly(new DateOnly(2002, 1, 1), new DateOnly(2010, 12, 31)).ToDateTime(new TimeOnly()))
            .RuleFor(x => x.FirstParent, (f, reg) =>
            {
                var parentFaker = _ParentRegisterSubrecordFaker
                    .RuleFor(x => x.Address, _ => reg.Address);

                return parentFaker.Generate();
            })
            .RuleFor(x => x.SecondParent, (f, reg) =>
            {
                var parentFaker = _ParentRegisterSubrecordFaker
                    .RuleFor(x => x.Address, f => f.PickRandom(reg.Address, f.Address.FullAddress()));

                return f.PickRandom(null, parentFaker.Generate());
            });

        private static Faker<Student> _StudentFaker => new Faker<Student>()
            .RuleFor(x => x.NumberInJournal, f => f.Random.Int(1, 31))
            .RuleFor(x => x.Info, (f, s) =>
            {
                var recordFaker = _StudentRegisterRecordFaker;

                return recordFaker.Generate();
            });


        #region OrganizationalClasses

        public static async Task<OrganizationalClass> Class_3b_27Students(
            SchoolYear year,
            IRepository<OrganizationalClass> orgClassRepo)
        {
            var orgClass = new OrganizationalClass
            {
                SchoolYearId = year.Id,
                Grade = 3,
                Distinction = "b"
            };

            int i = 1;
            orgClass.Students = _StudentFaker
                .RuleFor(x => x.SchoolYearId, _ => year.Id)
                .RuleFor(x => x.NumberInJournal, _ => i++)
                .Generate(27);

            await orgClassRepo.AddAsync(orgClass);
            await orgClassRepo.SaveAsync();

            return orgClass;
        }

        public static async Task<OrganizationalClass> Class_2a_Mechanics_15Students(
            SchoolYear year,
            IRepository<OrganizationalClass> orgClassRepo)
        {
            var orgClass = new OrganizationalClass
            {
                SchoolYearId = year.Id,
                Grade = 2,
                Distinction = "a",
                Specialization = "Mechanics"
            };

            int i = 1;
            orgClass.Students = _StudentFaker
                .RuleFor(x => x.SchoolYearId, _ => year.Id)
                .RuleFor(x => x.NumberInJournal, _ => i++)
                .Generate(15);

            await orgClassRepo.AddAsync(orgClass);
            await orgClassRepo.SaveAsync();

            return orgClass;
        }

        public static async Task<OrganizationalClass> Class_1e_0Students(
            SchoolYear year,
            IRepository<OrganizationalClass> orgClassRepo)
        {
            var orgClass = new OrganizationalClass
            {
                SchoolYearId = year.Id,
                Grade = 1,
                Distinction = "e",
            };

            await orgClassRepo.AddAsync(orgClass);
            await orgClassRepo.SaveAsync();

            return orgClass;
        }

        #endregion


        #region Students


        public static async Task<StudentRegisterRecord> StudentRegisterRecord(
            IRepository<StudentRegisterRecord> registerRepo,
            string? firstName = null,
            string? lastName = null,
            string? address = null)
        {
            var student = _StudentRegisterRecordFaker.Generate();

            if (firstName is not null) student.FirstName = firstName;
            if (lastName is not null) student.LastName = lastName;
            if (address is not null) student.Address = address;

            await registerRepo.AddAsync(student);
            await registerRepo.SaveAsync();

            return student;
        }

        public static async Task<Student> Student(
            SchoolYear year,
            IRepository<Student> studentRepo,
            string? firstName = null,
            string? lastName = null,
            string? address = null,
            long? organizationalClassId = null,
            int? numberInJournal = null)
        {
            var student = _StudentFaker.Generate();

            student.SchoolYearId = year.Id;
            if (firstName is not null) student.Info.FirstName = firstName;
            if (lastName is not null) student.Info.LastName = lastName;
            if (address is not null) student.Info.Address = address;
            if (organizationalClassId is not null) student.OrganizationalClassId = organizationalClassId.Value;
            if (numberInJournal is not null) student.NumberInJournal = numberInJournal.Value;

            await studentRepo.AddAsync(student);
            await studentRepo.SaveAsync();

            return student;
        }


        #endregion
    }
}
