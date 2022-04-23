using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SchoolAssistant.DAL.Models.SchoolYears;
using SchoolAssistant.DAL.Models.Shared;

namespace SchoolAssistant.DAL.Repositories
{
    public interface IRepositoryBySchoolYear<TSchoolYearDbEntity> : IRepository<TSchoolYearDbEntity>
        where TSchoolYearDbEntity : SchoolYearDbEntity
    {
        IList<TSchoolYearDbEntity> AsListByCurrentSchoolYear();
        Task<List<TSchoolYearDbEntity>> AsListByCurrentSchoolYearAsync();
        IList<TSchoolYearDbEntity> AsListBySchoolYear(SchoolYear semester);
        IList<TSchoolYearDbEntity> AsListBySchoolYear(long semesterId);
        Task<List<TSchoolYearDbEntity>> AsListBySchoolYearAsync(SchoolYear semester);
        Task<List<TSchoolYearDbEntity>> AsListBySchoolYearAsync(long semesterId);
    }

    [Injectable(typeof(IRepositoryBySchoolYear<>))]
    public class RepositoryBySchoolYear<TSchoolYearDbEntity> : Repository<TSchoolYearDbEntity>, IRepositoryBySchoolYear<TSchoolYearDbEntity>
        where TSchoolYearDbEntity : SchoolYearDbEntity
    {
        protected readonly ISchoolYearService _semesterSvc;

        public RepositoryBySchoolYear(
            SADbContext context,
            IServiceScopeFactory scopeFactory,
            ISchoolYearService semesterSvc) : base(context, scopeFactory)
        {
            _semesterSvc = semesterSvc;
        }

        public IList<TSchoolYearDbEntity> AsListByCurrentSchoolYear()
        {
            var curSemester = _semesterSvc.GetOrCreateCurrent();
            return AsListBySchoolYear(curSemester);
        }
        public IList<TSchoolYearDbEntity> AsListBySchoolYear(SchoolYear semester)
        {
            return AsListBySchoolYear(semester.Id);
        }
        public IList<TSchoolYearDbEntity> AsListBySchoolYear(long semesterId)
        {
            return _Repo.Where(x => x.SchoolYearId == semesterId).ToList();
        }


        public async Task<List<TSchoolYearDbEntity>> AsListByCurrentSchoolYearAsync()
        {
            var curSemester = await _semesterSvc.GetOrCreateCurrentAsync();
            return await AsListBySchoolYearAsync(curSemester);
        }
        public Task<List<TSchoolYearDbEntity>> AsListBySchoolYearAsync(SchoolYear semester)
        {
            return AsListBySchoolYearAsync(semester.Id);
        }
        public Task<List<TSchoolYearDbEntity>> AsListBySchoolYearAsync(long semesterId)
        {
            return _Repo.Where(x => x.SchoolYearId == semesterId).ToListAsync();
        }
    }
}
