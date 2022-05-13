using SchoolAssistant.DAL.Models.StudentsOrganization;
using SchoolAssistant.DAL.Repositories;
using SchoolAssistant.Infrastructure.Models.ScheduleArranger;

namespace SchoolAssistant.Logic.ScheduleArranger
{
    public interface IFetchClassLessonsForSchedArrService
    {
        Task<ScheduleClassLessonsJson?> ForAsync(long classId);
    }

    [Injectable]
    public class FetchClassLessonsForSchedArrService : IFetchClassLessonsForSchedArrService
    {
        private readonly IRepository<OrganizationalClass> _orgClassRepo;

        public FetchClassLessonsForSchedArrService(
            IRepository<OrganizationalClass> orgClassRepo)
        {
            _orgClassRepo = orgClassRepo;
        }


        public async Task<ScheduleClassLessonsJson?> ForAsync(long classId)
        {
            var orgClass = await _orgClassRepo.GetByIdAsync(classId);
            if (orgClass is null)
                return null;

            return new ScheduleClassLessonsJson
            {
                data = orgClass.Schedule.GroupBy(g => g.GetDayOfWeek())
                    .Select(x => new ScheduleDayLessonsJson
                    {
                        dayIndicator = x.Key,
                        lessons = x.Select(l => new ScheduleLessonTimetableEntryJson
                        {
                            id = l.Id,
                            time = new TimeJson(l.GetTime() ?? TimeOnly.MinValue),
                            customDuration = l.CustomDuration,
                            lecturer = new IdNameJson
                            {
                                id = l.LecturerId,
                                name = l.Lecturer.GetShortenedName()
                            },
                            subject = new IdNameJson
                            {
                                id = l.SubjectId,
                                name = l.Subject.Name
                            },
                            room = new IdNameJson
                            {
                                id = l.RoomId,
                                name = l.Room.DisplayName
                            },
                        }).ToArray()
                    }).ToArray()
            };
        }
    }
}
