using Microsoft.EntityFrameworkCore;
using SchoolAssistant.DAL.Models.StudentsParents;
using SchoolAssistant.DAL.Repositories;
using SchoolAssistant.Infrastructure.Models.UsersManagement;

namespace SchoolAssistant.Logic.UsersManagement.FetchUserRelatedObjectsHelp
{
    public interface IFetchStudentUserRelatedObjectsService
    {
        Task<StudentUserRelatedObjectJson[]> GetAsync(FetchRelatedObjectsRequestJson model);
    }

    [Injectable]
    public class FetchStudentUserRelatedObjectsService : IFetchStudentUserRelatedObjectsService
    {
        private readonly IRepository<StudentRegisterRecord> _regRecRepo;

        private FetchRelatedObjectsRequestJson _model = null!;
        private IQueryable<StudentUserRelatedObjectJson> _query = null!;

        public FetchStudentUserRelatedObjectsService(
            IRepository<StudentRegisterRecord> regRecRepo)
        {
            _regRecRepo = regRecRepo;
        }

        public Task<StudentUserRelatedObjectJson[]> GetAsync(FetchRelatedObjectsRequestJson model)
        {
            _model = model;

            PrepareQuery();

            return FetchArrayAsync();
        }

        private void PrepareQuery()
        {
            _query = _regRecRepo.AsQueryable()
                .Where(x => x.User == null)
                .Select(x => new
                {
                    record = x,
                    student = x.StudentInstances.AsQueryable()
                        .OrderBy(x => x.SchoolYear.Current)
                        .FirstOrDefault()
                })
                .Select(x => new StudentUserRelatedObjectJson
                {
                    id = x.record.Id,
                    firstName = x.record.FirstName,
                    lastName = x.record.LastName,
                    email = x.record.Email,
                    orgClass = x.student != null
                        ? x.student.OrganizationalClass != null
                            ? x.student.OrganizationalClass.Name
                            : null
                        : null
                });
        }

        private Task<StudentUserRelatedObjectJson[]> FetchArrayAsync()
        {
            return _query.ToArrayAsync();
        }
    }
}
