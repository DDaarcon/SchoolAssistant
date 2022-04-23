using NUnit.Framework;
using SchoolAssistant.DAL.Models.SchoolYears;
using SchoolAssistant.DAL.Models.StudentsOrganization;
using SchoolAssistant.DAL.Models.StudentsParents;
using SchoolAssistant.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolAssistans.Tests.DbEntities
{
    public class OrganizationalClassTests
    {
        private IRepository<OrganizationalClass> _classRepo;


        [OneTimeSetUp]
        public async Task Setup()
        {
            TestDatabase.CreateContext(TestServices.Collection);

            var semesterRepo = new Repository<SchoolYear>(TestDatabase.Context, null);
            _classRepo = new Repository<OrganizationalClass>(TestDatabase.Context, null);

            var semester = new SchoolYear
            {
                Year = 2010,
                Current = true
            };
            semesterRepo.Add(semester);
            semesterRepo.Save();


            var studentsClass = new OrganizationalClass
            {
                SchoolYearId = semester.Id,
                Supervisor = new SchoolAssistant.DAL.Models.Staff.Teacher
                {
                    FirstName = "dasdasd",
                    LastName = "dasdsad",
                },
                Students = new List<Student>
                {
                    new Student
                    {
                        SchoolYearId = semester.Id,
                        Info = new StudentRegisterRecord
                        {
                            FirstName = "kokoa",
                            LastName = "dajsdiaudna",
                            Address = "dadawd",
                            DateOfBirth = DateTime.Now,
                            PersonalID = "dasdasdas",
                            PlaceOfBirth = "dadasdasd",
                            FirstParent = new ParentRegisterSubrecord
                            {
                                Address = "dadawd",
                                FirstName = "dasdasdas",
                                LastName = "dawdawda",
                                Email = " dawdadawd",
                                PhoneNumber = "dawdada"
                            }
                        }
                    }
                }
            };

            await _classRepo.AddAsync(studentsClass);

            await _classRepo.SaveAsync();
        }


        [OneTimeTearDown]
        public void TearDown()
        {
            TestDatabase.DisposeContext();
        }

        [Test]
        public void AccessStudentInQuery()
        {
            var res = _classRepo.AsQueryable().Where(x => x.Students.Any())
                .SelectMany(x => x.Students)
                .Select(x => x.Info.FirstName)
                .ToList();

            Assert.IsTrue(res.Any(x => x == "kokoa"));
        }
    }
}
