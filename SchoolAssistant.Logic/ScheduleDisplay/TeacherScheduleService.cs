using Microsoft.EntityFrameworkCore;
using SchoolAssistant.DAL.Models.Lessons;
using SchoolAssistant.DAL.Models.SchoolYears;
using SchoolAssistant.DAL.Models.Staff;
using SchoolAssistant.DAL.Repositories;
using SchoolAssistant.Infrastructure.Models.ScheduleShared;
using SchoolAssistant.Logic.Help;

namespace SchoolAssistant.Logic.ScheduleDisplay
{
    public interface ITeacherScheduleService
    {
        Task<ScheduleDayLessonsJson<LessonJson>[]?> GetModelAsync(long teacherId, long schoolYearId);
        Task<ScheduleDayLessonsJson<LessonJson>[]?> GetModelAsync(long teacherId, SchoolYear schoolYear);
        Task<ScheduleDayLessonsJson<LessonJson>[]?> GetModelForCurrentYearAsync(long teacherId);
    }

    [Injectable]
    public class TeacherScheduleService : ITeacherScheduleService
    {
        private readonly ISchoolYearRepository _schoolYearRepo;
        private readonly IRepository<Teacher> _teacherRepo;
        private readonly IRepositoryBySchoolYear<PeriodicLesson> _lessonRepo;

        private long _teacherId;
        private SchoolYear? _schoolYear;
        private bool _forCurrentYear;
        private IEnumerable<PeriodicLesson>? _periodic;
        private DateTime _from;
        private DateTime _to;

        private ScheduleDayLessonsTempModel[] _tempModels = null!;

        public TeacherScheduleService(
            ISchoolYearRepository schoolYearRepo,
            IRepository<Teacher> teacherRepo,
            IRepositoryBySchoolYear<PeriodicLesson> lessonRepo)
        {
            _schoolYearRepo = schoolYearRepo;
            _teacherRepo = teacherRepo;
            _lessonRepo = lessonRepo;
        }

        public Task<ScheduleDayLessonsJson<LessonJson>[]?> GetModelForCurrentYearAsync(long teacherId)
        {
            _forCurrentYear = true;
            _teacherId = teacherId;

            return ExecuteAsync();
        }

        public async Task<ScheduleDayLessonsJson<LessonJson>[]?> GetModelAsync(long teacherId, long schoolYearId)
        {
            return await GetModelAsync(teacherId, (await _schoolYearRepo.GetByIdAsync(schoolYearId))!);
        }

        public Task<ScheduleDayLessonsJson<LessonJson>[]?> GetModelAsync(long teacherId, SchoolYear schoolYear)
        {
            _forCurrentYear = false;
            _teacherId = teacherId;
            _schoolYear = schoolYear;

            return ExecuteAsync();
        }

        private async Task<ScheduleDayLessonsJson<LessonJson>[]?> ExecuteAsync()
        {
            if (!await ValidateAsync())
                return null;

            await FetchLessonsAsync();

            CreateModelsArray();

            CalculateBorderDates();

            FillTempModels();

            return GetJsonModels();
        }

        private async Task<bool> ValidateAsync()
        {
            if (!await _teacherRepo.ExistsAsync(_teacherId))
                return false;

            if (!_forCurrentYear && _schoolYear is null)
                return false;

            return true;
        }

        private async Task FetchLessonsAsync()
        {
            var query = _schoolYear is null
                ? _lessonRepo.AsQueryableByYear.ByCurrent()
                : _lessonRepo.AsQueryableByYear.By(_schoolYear);

            _periodic = await query.Where(x => x.LecturerId == _teacherId)
                .ToListAsync();
        }

        private void CreateModelsArray()
        {
            _tempModels = Enum.GetValues<DayOfWeek>().Select(day => new ScheduleDayLessonsTempModel
            {
                Day = day
            }).ToArray();
        }

        private void CalculateBorderDates()
        {
            (_from, _to) = DatesHelper.GetStartAndEndOfCurrentWeek();
        }

        private void FillTempModels()
        {
            foreach (var periodic in _periodic!)
            {
                var occurances = periodic.GetOccurrences(_from, _to);

                foreach (var occurance in occurances)
                {
                    var time = occurance.TimeOfDay;

                    var lesson = new LessonJson
                    {
                        id = periodic.Id,
                        customDuration = periodic.CustomDuration,
                        time = new TimeJson { hour = time.Hours, minutes = time.Minutes },
                        subject = new IdNameJson { id = periodic.Subject.Id, name = periodic.Subject.Name },
                        room = new IdNameJson { id = periodic.Room.Id, name = periodic.Room.Name },
                        lecturer = new IdNameJson { id = periodic.Lecturer.Id, name = periodic.Lecturer.GetShortenedName() },
                        orgClass = periodic.ParticipatingOrganizationalClass is not null
                            ? new IdNameJson { id = periodic.ParticipatingOrganizationalClass.Id, name = periodic.ParticipatingOrganizationalClass.Name }
                            : null,
                        subjClass = periodic.ParticipatingSubjectClass is not null
                            ? new IdNameJson { id = periodic.ParticipatingSubjectClass.Id, name = periodic.ParticipatingSubjectClass.Name }
                            : null,

                    };

                    _tempModels.First(x => x.Day == occurance.DayOfWeek).Lessons.Add(lesson);
                }
            }
        }

        private ScheduleDayLessonsJson<LessonJson>[] GetJsonModels()
        {
            return _tempModels.Select(x => new ScheduleDayLessonsJson<LessonJson>
            {
                dayIndicator = x.Day,
                lessons = x.Lessons.ToArray()
            }).ToArray();
        }

        private class ScheduleDayLessonsTempModel
        {
            public DayOfWeek Day { get; set; }
            public IList<LessonJson> Lessons { get; set; } = new List<LessonJson>();
        }
    }
}
