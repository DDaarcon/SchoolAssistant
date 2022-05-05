using SchoolAssistant.DAL.Models.Lessons;
using SchoolAssistant.DAL.Models.StudentsOrganization;
using SchoolAssistant.DAL.Repositories;
using SchoolAssistant.Infrastructure.Models.ScheduleArranger;

namespace SchoolAssistant.Logic.ScheduleArranger
{
    public interface IFetchOtherLessonsForScheduleArrangerService
    {
        Task<ScheduleOtherLessonsJson> ForAsync(long classId, long? teacherid, long? roomId);
    }

    [Injectable]
    public class FetchOtherLessonsForScheduleArrangerService : IFetchOtherLessonsForScheduleArrangerService
    {
        private readonly IRepositoryBySchoolYear<PeriodicLesson> _lessonRepo;
        private readonly IRepository<OrganizationalClass> _orgClassRepo;


        public FetchOtherLessonsForScheduleArrangerService(
            IRepositoryBySchoolYear<PeriodicLesson> lessonRepo,
            IRepository<OrganizationalClass> orgClassRepo)
        {
            _lessonRepo = lessonRepo;
            _orgClassRepo = orgClassRepo;
        }


        public async Task<ScheduleOtherLessonsJson> ForAsync(long classId, long? teacherid, long? roomId)
        {
            return null;
        }
    }
}
