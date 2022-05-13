using Microsoft.EntityFrameworkCore;
using SchoolAssistant.DAL.Models.Lessons;
using SchoolAssistant.DAL.Models.StudentsOrganization;
using SchoolAssistant.DAL.Repositories;
using SchoolAssistant.Infrastructure.Models.ScheduleArranger;

namespace SchoolAssistant.Logic.ScheduleArranger
{
    public interface IFetchOtherLessonsForSchedArrService
    {
        Task<ScheduleOtherLessonsJson?> ForAsync(long classId, long? teacherId, long? roomId);
    }

    [Injectable]
    public class FetchOtherLessonsForSchedArrService : IFetchOtherLessonsForSchedArrService
    {
        private readonly IRepositoryBySchoolYear<PeriodicLesson> _lessonRepo;
        private readonly IRepository<OrganizationalClass> _orgClassRepo;

        private OrganizationalClass? _orgClass;

        public FetchOtherLessonsForSchedArrService(
            IRepositoryBySchoolYear<PeriodicLesson> lessonRepo,
            IRepository<OrganizationalClass> orgClassRepo)
        {
            _lessonRepo = lessonRepo;
            _orgClassRepo = orgClassRepo;
        }

        public async Task<ScheduleOtherLessonsJson?> ForAsync(long classId, long? teacherId, long? roomId)
        {
            _orgClass = await _orgClassRepo.GetByIdAsync(classId);
            if (_orgClass is null)
                return null;

            var query = _lessonRepo.AsQueryableByYear.ByYearOf(_orgClass);

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

        private async Task<ScheduleDayLessonsJson<LessonJson>[]> QueryToJsonArrayAsync(IQueryable<PeriodicLesson> query)
        {
            var lessons = await query.ToListAsync();

            return lessons.GroupBy(g => g.GetDayOfWeek())
                    .Select(x => new ScheduleDayLessonsJson<LessonJson>
                    {
                        dayIndicator = x.Key,
                        lessons = x.Select(l => new LessonJson
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
                            orgClass = l.ParticipatingOrganizationalClass is not null ?new IdNameJson
                            {
                                id = l.ParticipatingOrganizationalClassId!.Value,
                                name = l.ParticipatingOrganizationalClass.Name
                            } : null,
                            subjClass = l.ParticipatingSubjectClass is not null ? new IdNameJson
                            {
                                id = l.ParticipatingSubjectClassId!.Value,
                                name = l.ParticipatingSubjectClass.Name
                            } : null,
                        }).ToArray()
                    }).ToArray();
        }


    }
}
