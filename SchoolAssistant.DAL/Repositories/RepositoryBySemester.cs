using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SchoolAssistant.DAL.Models.Semesters;
using SchoolAssistant.DAL.Models.Shared;

namespace SchoolAssistant.DAL.Repositories
{
    public interface IRepositoryBySemester<TSemesterDbEntity> : IRepository<TSemesterDbEntity>
        where TSemesterDbEntity : SemesterDbEntity
    {
        IList<TSemesterDbEntity> AsListByCurrentSemester();
        Task<List<TSemesterDbEntity>> AsListByCurrentSemesterAsync();
        IList<TSemesterDbEntity> AsListBySemester(Semester semester);
        IList<TSemesterDbEntity> AsListBySemester(long semesterId);
        Task<List<TSemesterDbEntity>> AsListBySemesterAsync(Semester semester);
        Task<List<TSemesterDbEntity>> AsListBySemesterAsync(long semesterId);
    }

    [Injectable(typeof(IRepositoryBySemester<>))]
    public class RepositoryBySemester<TSemesterDbEntity> : Repository<TSemesterDbEntity>, IRepositoryBySemester<TSemesterDbEntity>
        where TSemesterDbEntity : SemesterDbEntity
    {
        protected readonly IRepository<Semester> _semesterRepo;

        public RepositoryBySemester(
            SADbContext context,
            IServiceScopeFactory scopeFactory,
            IRepository<Semester> semesterRepo) : base(context, scopeFactory)
        {
            _semesterRepo = semesterRepo;
        }

        public IList<TSemesterDbEntity> AsListByCurrentSemester()
        {
            var curSemester = _semesterRepo.AsQueryable().First(x => x.Current);
            return AsListBySemester(curSemester);
        }
        public IList<TSemesterDbEntity> AsListBySemester(Semester semester)
        {
            return AsListBySemester(semester.Id);
        }
        public IList<TSemesterDbEntity> AsListBySemester(long semesterId)
        {
            return _Repo.Where(x => x.SemesterId == semesterId).ToList();
        }


        public async Task<List<TSemesterDbEntity>> AsListByCurrentSemesterAsync()
        {
            var curSemester = await _semesterRepo.AsQueryable().FirstAsync(x => x.Current);
            return await AsListBySemesterAsync(curSemester);
        }
        public Task<List<TSemesterDbEntity>> AsListBySemesterAsync(Semester semester)
        {
            return AsListBySemesterAsync(semester.Id);
        }
        public Task<List<TSemesterDbEntity>> AsListBySemesterAsync(long semesterId)
        {
            return _Repo.Where(x => x.SemesterId == semesterId).ToListAsync();
        }
    }
}
