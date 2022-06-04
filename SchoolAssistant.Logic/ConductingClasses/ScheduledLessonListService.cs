using SchoolAssistant.Infrastructure.Models.ConductingClasses.ScheduledLessonsList;

namespace SchoolAssistant.Logic.ConductingClasses
{
    public interface IScheduledLessonListService
    {
        Task<ScheduledLessonListModel> GetModelForTeacherAsync(long teacherId, FetchScheduledLessonListModel model);
    }

    [Injectable]
    public class ScheduledLessonListService : IScheduledLessonListService
    {


        public async Task<ScheduledLessonListModel> GetModelForTeacherAsync(long teacherId, FetchScheduledLessonListModel model)
        {
            return null;
        }

    }
}
