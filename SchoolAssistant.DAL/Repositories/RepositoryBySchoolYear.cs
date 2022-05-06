using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SchoolAssistant.DAL.Models.SchoolYears;
using SchoolAssistant.DAL.Models.Shared;

namespace SchoolAssistant.DAL.Repositories
{
    public interface IRepositoryBySchoolYear<TSchoolYearDbEntity> : IRepository<TSchoolYearDbEntity>
        where TSchoolYearDbEntity : SchoolYearDbEntity
    {
        RepositoryBySchoolYear<TSchoolYearDbEntity>.SchoolYearOperations<List<TSchoolYearDbEntity>> AsListByYear { get; init; }
        RepositoryBySchoolYear<TSchoolYearDbEntity>.SchoolYearOperations<IQueryable<TSchoolYearDbEntity>> AsQueryableByYear { get; init; }

        void SelectYear(SchoolYear year);
    }

    [Injectable(typeof(IRepositoryBySchoolYear<>))]
    public class RepositoryBySchoolYear<TSchoolYearDbEntity> : Repository<TSchoolYearDbEntity>, IRepositoryBySchoolYear<TSchoolYearDbEntity>
        where TSchoolYearDbEntity : SchoolYearDbEntity
    {
        protected readonly ISchoolYearRepository _schoolYearSvc;

        protected SchoolYear? _selectedYear;

        public RepositoryBySchoolYear(
            SADbContext context,
            IServiceScopeFactory? scopeFactory,
            ISchoolYearRepository semesterSvc) : base(context, scopeFactory)
        {
            _schoolYearSvc = semesterSvc;

            AsListByYear = new SchoolYearOperations<List<TSchoolYearDbEntity>>(
                _schoolYearSvc,
                () => _Repo,
                () => _selectedYear,
                (query) => query.ToList(),
                (query) => query.ToListAsync());

            AsQueryableByYear = new SchoolYearOperations<IQueryable<TSchoolYearDbEntity>>(
                _schoolYearSvc,
                () => _Repo,
                () => _selectedYear,
                (query) => query,
                (query) => Task.FromResult(query));
        }


        public void SelectYear(SchoolYear year)
            => _selectedYear = year;


        public SchoolYearOperations<List<TSchoolYearDbEntity>> AsListByYear { get; init; }

        public SchoolYearOperations<IQueryable<TSchoolYearDbEntity>> AsQueryableByYear { get; init; }



        public class SchoolYearOperations<TResult>
        {
            readonly ISchoolYearRepository _schoolYearSvc;

            readonly Func<DbSet<TSchoolYearDbEntity>> _getRepo;
            readonly Func<SchoolYear?> _getSelectedYear;

            readonly Func<IQueryable<TSchoolYearDbEntity>, TResult> _resolver;
            readonly Func<IQueryable<TSchoolYearDbEntity>, Task<TResult>> _asyncResolver;


            private DbSet<TSchoolYearDbEntity> _Repo => _getRepo();
            private SchoolYear? _SelectedYear => _getSelectedYear();

            public SchoolYearOperations(
                ISchoolYearRepository schoolYearSvc,
                Func<DbSet<TSchoolYearDbEntity>> getRepo,
                Func<SchoolYear?> getSelectedYear,
                Func<IQueryable<TSchoolYearDbEntity>, TResult> resolver,
                Func<IQueryable<TSchoolYearDbEntity>, Task<TResult>> asyncResolver)
            {
                _getRepo = getRepo;
                _getSelectedYear = getSelectedYear;
                _resolver = resolver;
                _asyncResolver = asyncResolver;
                _schoolYearSvc = schoolYearSvc;
            }

            public TResult ByCurrent()
            {
                var curSemester = _schoolYearSvc.GetOrCreateCurrent();
                return By(curSemester);
            }
            public TResult ByYearOf(SchoolYearDbEntity entity)
                => By(entity.SchoolYear?.Id ?? entity.SchoolYearId);
            public TResult By(SchoolYear semester)
                => By(semester.Id);
            public TResult By(long semesterId)
                => _resolver(_Repo.Where(x => x.SchoolYearId == semesterId));


            public async Task<TResult> ByCurrentAsync()
            {
                var curSemester = await _schoolYearSvc.GetOrCreateCurrentAsync();
                return await ByAsync(curSemester);
            }
            public Task<TResult> ByYearOfAsync(SchoolYearDbEntity entity)
                => ByAsync(entity.SchoolYear?.Id ?? entity.SchoolYearId);
            public Task<TResult> ByAsync(SchoolYear semester)
                => ByAsync(semester.Id);
            public Task<TResult> ByAsync(long semesterId)
                => _asyncResolver(_Repo.Where(x => x.SchoolYearId == semesterId));


            public TResult BySelected()
                => By(_SelectedYear?.Id ?? 0);
            public Task<TResult> BySelectedAsync()
                => ByAsync(_SelectedYear?.Id ?? 0);
        }
    }
}
