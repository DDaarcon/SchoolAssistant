using SchoolAssistant.DAL.Models.Lessons;
using SchoolAssistant.DAL.Repositories;

namespace SchoolAssistant.Logic.General.PeriodicLessons
{
    public interface IRemovePeriodicLessonService
    {
        Task<bool> ValidateIdAndRemoveAsync(long id);
    }

    [Injectable]
    public class RemovePeriodicLessonService : IRemovePeriodicLessonService
    {
        readonly IRepository<PeriodicLesson> _lessonRepo;

        public RemovePeriodicLessonService(
            IRepository<PeriodicLesson> lessonRepo)
        {
            _lessonRepo = lessonRepo;
        }

        public async Task<bool> ValidateIdAndRemoveAsync(long id)
        {
            if (!await _lessonRepo.ExistsAsync(id))
                return false;

            _lessonRepo.RemoveById(id);
            await _lessonRepo.SaveAsync();

            return true;
        }
    }
}
