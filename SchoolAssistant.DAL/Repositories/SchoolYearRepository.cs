using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SchoolAssistant.DAL.Models.SchoolYears;

namespace SchoolAssistant.DAL.Repositories
{
    public interface ISchoolYearRepository : IRepository<SchoolYear>
    {
        SchoolYear GetOrCreate(int year);
        Task<SchoolYear> GetOrCreateAsync(int year);
        SchoolYear GetOrCreateCurrent();
        Task<SchoolYear> GetOrCreateCurrentAsync();
    }

    [Injectable]
    public class SchoolYearRepository : Repository<SchoolYear>, ISchoolYearRepository
    {
        public SchoolYearRepository(SADbContext context, IServiceScopeFactory? scopeFactory) : base(context, scopeFactory)
        {
        }



        public SchoolYear GetOrCreateCurrent()
        {
            int year = GetStartYearForCurrent();

            return FindOrCreate(year, true);
        }
        public SchoolYear GetOrCreate(int year)
            => FindOrCreate(year, false);

        private SchoolYear FindOrCreate(int year, bool isCurrent)
        {
            var schoolYear = _Repo.AsQueryable().FirstOrDefault(
                x => x.Year == year
                    || isCurrent ? x.Current : false);

            if (schoolYear is not null)
                return schoolYear;

            return Create(year, isCurrent);
        }

        private SchoolYear Create(int year, bool isCurrent)
        {
            var current = new SchoolYear
            {
                Year = (short)year,
                Current = isCurrent
            };

            Add(current);
            Save();

            return current;
        }



        public Task<SchoolYear> GetOrCreateCurrentAsync()
        {
            int year = GetStartYearForCurrent();

            return FindOrCreateAsync(year, true);
        }
        public Task<SchoolYear> GetOrCreateAsync(int year)
            => FindOrCreateAsync(year, false);

        private async Task<SchoolYear> FindOrCreateAsync(int year, bool isCurrent)
        {
            var schoolYear = await _Repo.AsQueryable().FirstOrDefaultAsync(
                x => x.Year == year
                    || isCurrent ? x.Current : false);

            if (schoolYear is not null)
                return schoolYear;

            return await CreateAsync(year, isCurrent);
        }
        private async Task<SchoolYear> CreateAsync(int year, bool isCurrent)
        {
            var current = new SchoolYear
            {
                Year = (short)year,
                Current = isCurrent
            };

            await AddAsync(current);
            await SaveAsync();

            return current;
        }



        public override void Add(SchoolYear entity)
        {
            if (entity.Current)
            {
                var existingCurrents = _Repo.Where(x => x.Current).ToList();
                foreach (var existingCurrent in existingCurrents)
                    existingCurrent.Current = false;

            }
            base.Add(entity);
        }

        public override async Task AddAsync(SchoolYear entity)
        {
            if (entity.Current)
            {
                var existingCurrents = await _Repo.Where(x => x.Current).ToListAsync();
                foreach (var existingCurrent in existingCurrents)
                    existingCurrent.Current = false;
            }
            await base.AddAsync(entity);
        }



        private int GetStartYearForCurrent()
        {
            var today = DateTime.Today;

            return today.DayOfYear < 365 / 2
                ? today.Year - 1
                : today.Year;
        }
    }
}
