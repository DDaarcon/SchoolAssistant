using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using SchoolAssistant.DAL.Models.Staff;
using SchoolAssistant.DAL.Repositories;
using SchoolAssistant.Logic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolAssistans.Tests.DbEntities
{
    public class UnsortedTests
    {
        [OneTimeSetUp]
        public void Setup()
        {
            TestDatabase.CreateContext(TestServices.Collection);
        }

        [OneTimeTearDown]
        public void TearUp()
        {
            TestDatabase.DisposeContext();
        }

        [Test]
        public async Task Should_build_full_name_of_person()
        {
            var teacherRepo = new Repository<Teacher>(TestDatabase.Context, null);

            teacherRepo.Add(new Teacher
            {
                FirstName = "Jonasz",
                LastName = "Monasz"
            });
            await teacherRepo.SaveAsync();


            var fullName = await teacherRepo.AsQueryable().Select(x => x.GetFullName()).FirstOrDefaultAsync();

            Assert.IsNotNull(fullName);
            Assert.AreEqual(fullName, "Monasz Jonasz");
        }
    }
}
