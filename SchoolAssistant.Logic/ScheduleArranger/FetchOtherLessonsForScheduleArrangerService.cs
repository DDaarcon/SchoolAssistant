using Microsoft.EntityFrameworkCore;
using SchoolAssistant.DAL.Models.Lessons;
using SchoolAssistant.DAL.Models.StudentsOrganization;
using SchoolAssistant.DAL.Repositories;
using SchoolAssistant.Infrastructure.Models.ScheduleArranger;
using System.Linq.Expressions;

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

        private Expression<Func<PeriodicLesson, PeriodicLessonTimetableEntryJson>> _entityToJson = entity => new PeriodicLessonTimetableEntryJson
        {
            id = entity.Id,
            customDuration = entity.CustomDuration,
            time = new TimeJson
            {
                hour = int.Parse(entity.CronPeriodicity.Split(' ', StringSplitOptions.TrimEntries)[1]),
                minutes = int.Parse(entity.CronPeriodicity.Split(' ', StringSplitOptions.TrimEntries)[0]),
            },
            subject = new IdNameJson
            {
                id = entity.Subject.Id,
                name = entity.Subject.Name
            },
            lecturer = new IdNameJson
            {
                id = entity.Lecturer.Id,
                name = entity.Lecturer.GetShortenedName()
            },
            room = new IdNameJson
            {
                id = entity.Room.Id,
                name = entity.Room.DisplayName
            }
        };

        public async Task<ScheduleOtherLessonsJson?> ForAsync(long classId, long? teacherId, long? roomId)
        {
            var orgClass = await _orgClassRepo.GetByIdAsync(classId);
            if (orgClass is null)
                return null;

            var query = _lessonRepo.AsQueryableByYear.ByYearOf(orgClass);

            var teacherLessons = await query
                .Where(x => x.ParticipatingOrganizationalClassId != classId
                    && x.LecturerId == teacherId)
                .Select(_entityToJson).ToArrayAsync();

            var roomLessons = await query
                .Where(x => x.ParticipatingOrganizationalClassId != classId
                    && x.RoomId == roomId)
                .Select(_entityToJson).ToArrayAsync();

            return new ScheduleOtherLessonsJson
            {
                teacher = teacherLessons,
                room = roomLessons
            };
        }


    }
}
