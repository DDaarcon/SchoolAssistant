using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using SchoolAssistant.DAL.Models.Subjects;
using SchoolAssistant.DAL.Repositories;
using SchoolAssistant.Infrastructure.Models.DataManagement.Subjects;
using SchoolAssistant.Logic.DataManagement.Subjects;

namespace SchoolAssistans.Tests.DbEntities
{
    public class SubjectsTests
    {
        private IRepository<Subject> _repo;
        private IModifySubjectFromJsonService _modifyService;
        private ISubjectsDataManagementService _dataManagementService;


        [OneTimeSetUp]
        public void Setup()
        {
            TestDatabase.CreateContext(TestServices.Collection);

            _repo = new Repository<Subject>(TestDatabase.Context, null);
            _modifyService = new ModifySubjectFromJsonService(_repo);
            _dataManagementService = new SubjectsDataManagementService(_repo, _modifyService);
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            TestDatabase.DisposeContext();
        }

        [Test]
        public async Task Should_create_subject()
        {
            var model = new SubjectDetailsJson
            {
                name = "Test subject"
            };

            await _dataManagementService.CreateOrUpdateAsync(model);

            var subject = _repo.AsQueryable().FirstOrDefault(x => x.Name == "Test subject");

            Assert.IsNotNull(subject);
        }

        [Test]
        public async Task Should_update_subject()
        {
            var subject = new Subject
            {
                Name = "Test subject2"
            };
            _repo.Add(subject);
            await _repo.SaveAsync();

            Assert.IsFalse(subject.Id == default);

            var model = new SubjectDetailsJson
            {
                id = subject.Id,
                name = "Test subject2 updated"
            };

            await _dataManagementService.CreateOrUpdateAsync(model);

            var subjectUpdated = await _repo.GetByIdAsync(subject.Id);

            Assert.IsNotNull(subjectUpdated);
            Assert.AreEqual(subject.Id, subjectUpdated.Id);
            Assert.AreEqual(subjectUpdated.Name, "Test subject2 updated");
        }
    }
}
