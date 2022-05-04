using Microsoft.EntityFrameworkCore;
using SchoolAssistant.DAL.Models.StudentsOrganization;
using SchoolAssistant.DAL.Repositories;
using SchoolAssistant.Infrastructure.Models.ScheduleArranger;

namespace SchoolAssistant.Logic.ScheduleArranger
{
    public interface IFetchClassesForScheduleArrangerService
    {
        Task<ScheduleClassSelectorEntryJson[]> FetchForCurrentYearAsync();
    }

    [Injectable]
    public class FetchClassesForScheduleArrangerService : IFetchClassesForScheduleArrangerService
    {
        private readonly IRepositoryBySchoolYear<OrganizationalClass> _orgClassRepo;

        public FetchClassesForScheduleArrangerService(
            IRepositoryBySchoolYear<OrganizationalClass> orgClassRepo)
        {
            _orgClassRepo = orgClassRepo;
        }

        public Task<ScheduleClassSelectorEntryJson[]> FetchForCurrentYearAsync()
        {
            return _orgClassRepo.AsQueryableByYear.ByCurrent()
                .Select(x => new ScheduleClassSelectorEntryJson
                {
                    id = x.Id,
                    name = x.Name,
                    specialization = x.Specialization
                })
                .ToArrayAsync();
        }
    }
}
