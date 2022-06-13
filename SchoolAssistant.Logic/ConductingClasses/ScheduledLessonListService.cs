using AppConfigurationEFCore;
using Microsoft.EntityFrameworkCore;
using SchoolAssistant.DAL.Help.AppConfiguration;
using SchoolAssistant.DAL.Models.Lessons;
using SchoolAssistant.DAL.Models.Staff;
using SchoolAssistant.DAL.Repositories;
using SchoolAssistant.Infrastructure.Models.ConductingClasses.ScheduledLessonsList;
using SchoolAssistant.Logic.Help;

namespace SchoolAssistant.Logic.ConductingClasses
{
    public interface IScheduledLessonListService
    {
        Task<ScheduledLessonListJson?> GetModelForTeacherAsync(long teacherId, FetchScheduledLessonsRequestModel model);
    }

    [Injectable]
    public class ScheduledLessonListService : IScheduledLessonListService
    {
        private readonly IRepository<Teacher> _teacherRepo;
        private readonly IRepositoryBySchoolYear<PeriodicLesson> _perioLessonRepo;
        private readonly IAppConfiguration<AppConfigRecords> _configRepo;

        private const int MAX = 50;

        private long _teacherId;
        private FetchScheduledLessonsRequestModel _model = null!;
        private int _defaultDuration;

        private IEnumerable<PeriodicLessonWithOccurances> _lessonWithOccurances = null!;

        private IEnumerable<ScheduledLessonListEntryJson> _listItems = null!;

        public ScheduledLessonListService(
            IRepository<Teacher> teacherRepo,
            IRepositoryBySchoolYear<PeriodicLesson> perioLessonRepo,
            IAppConfiguration<AppConfigRecords> configRepo)
        {
            _teacherRepo = teacherRepo;
            _perioLessonRepo = perioLessonRepo;
            _configRepo = configRepo;
        }

        public async Task<ScheduledLessonListJson?> GetModelForTeacherAsync(long teacherId, FetchScheduledLessonsRequestModel model)
        {
            _model = model;
            _teacherId = teacherId;
            _defaultDuration = await _configRepo.Records.DefaultLessonDuration.GetAsync() ?? 45;

            if (!await ValidateAndFetchAsync(teacherId))
                return null;

            await FetchPeriodicLessonsAsync();

            CreateListItems();
            FilterListItems();

            return await GetListModelAsync();
        }

        private async Task<bool> ValidateAndFetchAsync(long teacherId)
        {
            if (_model is null) return false;

            if (!await _teacherRepo.ExistsAsync(teacherId))
                return false;

            return true;
        }

        private async Task FetchPeriodicLessonsAsync()
        {
            // TODO: Calculating occurances of PeriodicLesson in db, including TakenLessons only in range
            var scheduled = await _perioLessonRepo.AsQueryableByYear
                .ByCurrent()
                .Where(x => x.LecturerId == _teacherId)
                .Include(x => x.TakenLessons)
                .ToListAsync();

            _lessonWithOccurances = scheduled.Select(x => new PeriodicLessonWithOccurances
            {
                ScheduleLesson = x,
                Occurances = x.GetOccurrences(_model.From?.AddMinutes(-(x.CustomDuration ?? _defaultDuration)) ?? DateTime.MinValue,
                        _model.To ?? DateTime.MaxValue)
            })
                .Where(x => x.Occurances.Any());
        }

        private void CreateListItems()
        {
            _listItems = _lessonWithOccurances.SelectMany(lwo =>
                lwo.Occurances.Select(date =>
                {
                    var takenLesson = lwo.ScheduleLesson.TakenLessons.FirstOrDefault(x => x.Date == date);

                    return new ScheduledLessonListEntryJson
                    {
                        className = lwo.ScheduleLesson.ParticipatingOrganizationalClass!.Name,
                        duration = lwo.ScheduleLesson.CustomDuration ?? _defaultDuration,
                        subjectName = lwo.ScheduleLesson.Subject.Name,
                        startTimeTk = takenLesson?.ActualDate.GetTicksJs() ?? date.GetTicksJs(),
                        heldClasses = ToHeldClassesModel(takenLesson)
                    };
                })).OrderBy(x => x.startTimeTk);
        }

        private HeldClassesModel? ToHeldClassesModel(Lesson? lesson)
        {
            if (lesson is null) return null;
            return new HeldClassesModel
            {
                Topic = lesson.Topic,
                AmountOfPresentStudents = lesson.PresenceOfStudents.Count(x => x.Status == DAL.Enums.PresenceStatus.Present),
                AmountOfAllStudents = lesson.PresenceOfStudents.Count
            };
        }

        private void FilterListItems()
        {
            if (_model.OnlyUpcoming)
                _listItems = _listItems.Where(x => x.heldClasses == null);

            if (_model.From.HasValue)
                _listItems = _listItems.Take(_model.LimitTo ?? MAX);
            else
                _listItems = _listItems.TakeLast(_model.LimitTo ?? MAX);
        }

        private async Task<ScheduledLessonListJson> GetListModelAsync()
        {
            var nowTk = DateTime.Now.GetTicksJs();
            return new ScheduledLessonListJson
            {
                entries = _listItems.ToArray(),
                incomingAtTk = _listItems.Where(x => x.startTimeTk >= nowTk).FirstOrDefault()?.startTimeTk,
                minutesBeforeLessonIsSoon = await _configRepo.Records.MinutesBeforeLessonConsideredClose.GetAsync() ?? 5
            };
        }

        internal class PeriodicLessonWithOccurances
        {
            public PeriodicLesson ScheduleLesson { get; set; } = null!;
            public IEnumerable<DateTime> Occurances { get; set; } = null!;
        }
    }
}
