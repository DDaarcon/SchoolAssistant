using Microsoft.EntityFrameworkCore;
using SchoolAssistant.DAL.Models.Lessons;
using SchoolAssistant.DAL.Models.Staff;
using SchoolAssistant.DAL.Repositories;

namespace SchoolAssistant.Logic.ConductingClasses.ConductLesson
{
    public interface IStartLessonService
    {
        Task<bool> TryStartLessonAtAsync(DateTime scheduledTime, long teacherId);
        Task<bool> TryStartLessonAtAsync(DateTime scheduledTime, Teacher teacher);
    }

    [Injectable]
    public class StartLessonService : IStartLessonService
    {
        // TODO: tests
        private readonly IRepository<Teacher> _teacherRepo;
        private readonly IRepositoryBySchoolYear<PeriodicLesson> _periodicLessonRepo;
        private readonly IRepositoryBySchoolYear<Lesson> _lessonRepo;

        private readonly IConductLessonSessionService _storeInSessionStateSvc;

        private Teacher? _teacher;
        private DateTime _scheduledTime;
        private Lesson? _lesson;

        public StartLessonService(
            IRepository<Teacher> teacherRepo,
            IRepositoryBySchoolYear<Lesson> lessonRepo,
            IRepositoryBySchoolYear<PeriodicLesson> periodicLessonRepo,
            IConductLessonSessionService storeInSessionStateSvc)
        {
            _teacherRepo = teacherRepo;
            _lessonRepo = lessonRepo;
            _periodicLessonRepo = periodicLessonRepo;
            _storeInSessionStateSvc = storeInSessionStateSvc;
        }

        public async Task<bool> TryStartLessonAtAsync(DateTime scheduledTime, long teacherId)
            => await TryStartLessonAtAsync(scheduledTime, (await _teacherRepo.GetByIdAsync(teacherId))!);

        public async Task<bool> TryStartLessonAtAsync(DateTime scheduledTime, Teacher teacher)
        {
            _teacher = teacher;
            _scheduledTime = scheduledTime;

            if (!Validate())
                return false;

            if (!await TryFindLessonAsync())
                return false;

            await _storeInSessionStateSvc.SetConductedLessonInSessionAsync(_lesson!.Id);

            return true;
        }

        private bool Validate()
        {
            if (_teacher is null)
                return false;

            return true;
        }

        private async Task<bool> TryFindLessonAsync()
        {
            await FindExistingLessonAsync();
            if (_lesson is not null)
                return true;

            await CreateLessonWithScheduleAsync();
            if (_lesson is not null)
                return true;

            return false;
        }

        private async Task FindExistingLessonAsync()
        {
            _lesson = await _lessonRepo.AsQueryableByYear.ByCurrent()
                .FirstOrDefaultAsync(x => x.FromSchedule.LecturerId == _teacher!.Id
                    && x.Date == _scheduledTime);
        }

        private async Task CreateLessonWithScheduleAsync()
        {
            var schedule = await _periodicLessonRepo.AsQueryableByYear.ByCurrent()
                .Where(x => x.LecturerId == _teacher!.Id)
                .ToListAsync();

            var scheduleLesson = schedule.FirstOrDefault(x => x.IsValidOccurrence(_scheduledTime));
            if (scheduleLesson is null)
                return;

            _lesson = new Lesson
            {
                Date = _scheduledTime,
                FromScheduleId = scheduleLesson.Id,
                SchoolYearId = scheduleLesson.SchoolYearId
            };

            _lessonRepo.Add(_lesson);
            await _lessonRepo.SaveAsync();
        }
    }
}
