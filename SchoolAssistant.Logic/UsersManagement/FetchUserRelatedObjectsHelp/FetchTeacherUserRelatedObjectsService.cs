using Microsoft.EntityFrameworkCore;
using SchoolAssistant.DAL.Models.Staff;
using SchoolAssistant.DAL.Repositories;
using SchoolAssistant.Infrastructure.Models.UsersManagement;

namespace SchoolAssistant.Logic.UsersManagement.FetchUserRelatedObjectsHelp
{
    public interface IFetchTeacherUserRelatedObjectsService
    {
        Task<SimpleRelatedObjectJson[]> GetAsync(FetchRelatedObjectsRequestJson model);
    }

    [Injectable]
    public class FetchTeacherUserRelatedObjectsService : IFetchTeacherUserRelatedObjectsService
    {
        private readonly IRepository<Teacher> _teacherRepo;

        private FetchRelatedObjectsRequestJson _model = null!;
        private IQueryable<SimpleRelatedObjectJson> _query = null!;

        public FetchTeacherUserRelatedObjectsService(
            IRepository<Teacher> teacherRepo)
        {
            _teacherRepo = teacherRepo;
        }

        public Task<SimpleRelatedObjectJson[]> GetAsync(FetchRelatedObjectsRequestJson model)
        {
            _model = model;

            PrepareQuery();

            return FetchArrayAsync();
        }

        private void PrepareQuery()
        {
            _query = _teacherRepo.AsQueryable()
                .Where(x => x.User == null)
                .Select(x => new SimpleRelatedObjectJson
                {
                    id = x.Id,
                    firstName = x.FirstName,
                    lastName = x.LastName,
                    email = x.Email
                });
        }

        private Task<SimpleRelatedObjectJson[]> FetchArrayAsync()
        {
            return _query.ToArrayAsync();
        }
    }
}
