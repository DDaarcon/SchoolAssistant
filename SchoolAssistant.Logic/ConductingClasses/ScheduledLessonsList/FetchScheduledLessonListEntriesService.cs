using AppConfigurationEFCore;
using Microsoft.EntityFrameworkCore;
using SchoolAssistant.DAL.Help.AppConfiguration;
using SchoolAssistant.DAL.Models.Lessons;
using SchoolAssistant.DAL.Models.Staff;
using SchoolAssistant.DAL.Repositories;
using SchoolAssistant.Infrastructure.Enums.Attendance;
using SchoolAssistant.Infrastructure.Models.ConductingClasses.ScheduledLessonsList;
using SchoolAssistant.Logic.Help;

namespace SchoolAssistant.Logic.ConductingClasses.ScheduledLessonsList
{
    public interface IFetchScheduledLessonListEntriesService
    {
        Task<ScheduledLessonListEntriesJson> GetModelForTeacherAsync(long teacherId, FetchScheduledLessonsRequestModel model);
        Task<ScheduledLessonListEntriesJson> GetModelForTeacherAsync(long teacherId, FetchScheduledLessonsRequestJson model);
    }

    [Injectable]
    public class FetchScheduledLessonListEntriesService : IFetchScheduledLessonListEntriesService
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

        public FetchScheduledLessonListEntriesService(
            IRepository<Teacher> teacherRepo,
            IRepositoryBySchoolYear<PeriodicLesson> perioLessonRepo,
            IAppConfiguration<AppConfigRecords> configRepo)
        {
            _teacherRepo = teacherRepo;
            _perioLessonRepo = perioLessonRepo;
            _configRepo = configRepo;
        }

        public Task<ScheduledLessonListEntriesJson> GetModelForTeacherAsync(long teacherId, FetchScheduledLessonsRequestJson model)
            => GetModelForTeacherAsync(teacherId, new FetchScheduledLessonsRequestModel
            {
                From = DatesHelper.FromTicksJs(model.fromTk),
                To = DatesHelper.FromTicksJs(model.toTk),
                LimitTo = model.limitTo,
                OnlyUpcoming = model.onlyUpcoming
            });

        public async Task<ScheduledLessonListEntriesJson> GetModelForTeacherAsync(long teacherId, FetchScheduledLessonsRequestModel model)
        {
            _model = model;
            _teacherId = teacherId;
            _defaultDuration = await _configRepo.Records.DefaultLessonDuration.GetAsync() ?? 45;

            if (!await ValidateAndFetchAsync(teacherId))
                return new ScheduledLessonListEntriesJson { entries = Array.Empty<ScheduledLessonListEntryJson>() };

            await FetchPeriodicLessonsAsync();

            CreateListItems();
            FilterListItems();

            return GetListModel();
        }

        private async Task<bool> ValidateAndFetchAsync(long teacherId)
        {
            if (_model is null) return false;

            if (!await _teacherRepo.ExistsAsync(teacherId))
                return false;

            if (!_model.From.HasValue && !_model.To.HasValue)
                return false;

            return true;
        }

        private async Task FetchPeriodicLessonsAsync()
        {
            var scheduled = await _perioLessonRepo.AsQueryableByYear
                .ByCurrent()
                .Where(x => x.LecturerId == _teacherId)
                .Include(x => x.TakenLessons.Where(x =>
                    _model.From.HasValue ? x.Date >= _model.From.Value : true
                    && _model.To.HasValue ? x.Date <= _model.To.Value : true))
                .ToListAsync();

            _lessonWithOccurances = scheduled.Select(x => new PeriodicLessonWithOccurances
            {
                ScheduleLesson = x,
                Occurances = GetRangedAndLimitedLessonOccurrences(x)
            })
                .Where(x => x.Occurances.Any());
        }

        private IEnumerable<DateTime> GetRangedAndLimitedLessonOccurrences(PeriodicLesson lesson)
        {
            if (_model.From.HasValue && _model.To.HasValue)
                return lesson.GetOccurrences(_model.From.Value, _model.To.Value);
            if (_model.From.HasValue)
                return lesson.GetNextOccurrences(_model.From.Value, _Limit);
            if (_model.To.HasValue)
                return lesson.GetPreviousOccurrences(_model.To.Value, _Limit);

            throw new Exception("Model validation failed. Model has to have From, To, or both");
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
                        startTimeTk = takenLesson?.ActualDate?.GetTicksJsFakeLocal() ?? date.GetTicksJsFakeLocal(),
                        heldClasses = ToHeldClassesModel(takenLesson)
                    };
                })).OrderBy(x => x.startTimeTk);
        }

        private HeldClassesJson? ToHeldClassesModel(Lesson? lesson)
        {
            // TODO: in case of missing presence entity count all users anyway
            if (lesson is null) return null;
            return new HeldClassesJson
            {
                topic = lesson.Topic ?? "",
                amountOfPresentStudents = lesson.PresenceOfStudents.Count(x => x.Status == PresenceStatus.Present),
                amountOfAllStudents = lesson.FromSchedule.ParticipatingOrganizationalClass?.Students.Count ?? 0
            };
        }

        private void FilterListItems()
        {
            if (_model.OnlyUpcoming)
                _listItems = _listItems.Where(x => x.heldClasses == null);

            if (_TakeNearEndDate)
                _listItems = _listItems.TakeLast(_Limit);
            else
                _listItems = _listItems.Take(_Limit);
        }

        private ScheduledLessonListEntriesJson GetListModel()
        {
            var nowTk = DateTime.Now.GetTicksJs();
            return new ScheduledLessonListEntriesJson
            {
                entries = _listItems.ToArray(),
                incomingAtTk = _listItems.Where(x => x.startTimeTk >= nowTk).FirstOrDefault()?.startTimeTk
            };
        }

        private bool _TakeNearEndDate => !_model.From.HasValue;
        private int _Limit => _model.LimitTo ?? MAX;

        private class PeriodicLessonWithOccurances
        {
            public PeriodicLesson ScheduleLesson { get; set; } = null!;
            public IEnumerable<DateTime> Occurances { get; set; } = null!;
        }
    }
}
