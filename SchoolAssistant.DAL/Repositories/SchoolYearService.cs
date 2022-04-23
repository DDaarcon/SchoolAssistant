using Microsoft.EntityFrameworkCore;
using SchoolAssistant.DAL.Models.SchoolYears;

namespace SchoolAssistant.DAL.Repositories
{
    public interface ISchoolYearService
    {
        SchoolYear GetOrCreateCurrent();
        Task<SchoolYear> GetOrCreateCurrentAsync();
    }

    [Injectable]
    public class SchoolYearService : ISchoolYearService
    {
        private readonly IRepository<SchoolYear> _repo;

        public SchoolYearService(
            IRepository<SchoolYear> repo)
        {
            _repo = repo;
        }

        public SchoolYear GetOrCreateCurrent()
        {
            var current = _repo.AsQueryable().FirstOrDefault(x => x.Current);
            if (current is not null)
                return current;

            return FindOrCreateCurrent();
        }
        public async Task<SchoolYear> GetOrCreateCurrentAsync()
        {
            var current = await _repo.AsQueryable().FirstOrDefaultAsync(x => x.Current);
            if (current is not null)
                return current;

            return await FindOrCreateCurrentAsync();
        }

        private SchoolYear FindOrCreateCurrent()
        {
            int year = GetStartYearForCurrent();

            var current = _repo.AsQueryable().FirstOrDefault(x => x.Year == year);
            if (current is not null)
                return current;

            return CreateCurrent();
        }
        private async Task<SchoolYear> FindOrCreateCurrentAsync()
        {
            int year = GetStartYearForCurrent();

            var current = await _repo.AsQueryable().FirstOrDefaultAsync(x => x.Year == year);
            if (current is not null)
                return current;

            return await CreateCurrentAsync();
        }

        private SchoolYear CreateCurrent()
        {
            var current = new SchoolYear
            {
                Year = (short)GetStartYearForCurrent(),
                Current = true
            };

            _repo.Add(current);
            _repo.Save();

            return current;
        }
        private async Task<SchoolYear> CreateCurrentAsync()
        {
            var current = new SchoolYear
            {
                Year = (short)GetStartYearForCurrent(),
                Current = true
            };

            await _repo.AddAsync(current);
            await _repo.SaveAsync();

            return current;
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
