using Microsoft.EntityFrameworkCore;
using SchoolAssistant.DAL.Models.Lessons;
using SchoolAssistant.DAL.Models.StudentsOrganization;
using SchoolAssistant.DAL.Repositories;
using SchoolAssistant.Infrastructure.Models.ScheduleArranger;

namespace SchoolAssistant.Logic.ScheduleArranger
{
    public interface IFetchOtherLessonsForScheduleArrangerService
    {
        Task<ScheduleOtherLessonsJson?> ForAsync(long classId, long? teacherId, long? roomId);
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

        public async Task<ScheduleOtherLessonsJson?> ForAsync(long classId, long? teacherId, long? roomId)
        {
            var orgClass = await _orgClassRepo.GetByIdAsync(classId);
            if (orgClass is null)
                return null;

            var query = _lessonRepo.AsQueryableByYear.ByYearOf(orgClass);

            var queryTeacher = query
                .Where(x => x.ParticipatingOrganizationalClassId != classId
                    && x.LecturerId == teacherId);

            var queryRoom = query
                .Where(x => x.ParticipatingOrganizationalClassId != classId
                    && x.RoomId == roomId);

            return new ScheduleOtherLessonsJson
            {
                teacher = await QueryToJsonArrayAsync(queryTeacher),
                room = await QueryToJsonArrayAsync(queryRoom)
            };
        }

        private async Task<ScheduleDayLessonsJson[]> QueryToJsonArrayAsync(IQueryable<PeriodicLesson> query)
        {
            var lessons = await query.ToListAsync();

            return lessons.GroupBy(g => g.GetDayOfWeek())
                    .Select(x => new ScheduleDayLessonsJson
                    {
                        dayIndicator = x.Key,
                        lessons = x.Select(l => new PeriodicLessonTimetableEntryJson
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
                    }).ToArray();
        }


    }
}
