using Cronos;
using SchoolAssistant.DAL.Models.Lessons;
using SchoolAssistant.DAL.Models.StudentsParents;
using SchoolAssistant.DAL.Repositories;
using SchoolAssistant.Infrastructure.Models.ScheduleShared;
using SchoolAssistant.Logic.Help;

namespace SchoolAssistant.Logic.ScheduleDisplay
{
    public interface IStudentScheduleService
    {
        Task<ScheduleDayLessonsJson[]?> GetModelAsync(long studentId);
        ScheduleDayLessonsJson[]? GetModel(Student student);
    }

    [Injectable]
    public class StudentScheduleService : IStudentScheduleService
    {
        private readonly IRepository<Student> _studentRepo;

        private Student _student = null!;
        private IEnumerable<PeriodicLesson>? _periodic;
        private DateTime _from;
        private DateTime _to;

        private ScheduleDayLessonsTempModel[] _tempModels = null!;

        public StudentScheduleService(
            IRepository<Student> studentRepo)
        {
            _studentRepo = studentRepo;
        }

        public async Task<ScheduleDayLessonsJson[]?> GetModelAsync(long studentId)
        {
            var student = await _studentRepo.GetByIdAsync(studentId);
            if (student is null)
                return null;

            return GetModel(student);
        }

        public ScheduleDayLessonsJson[]? GetModel(Student student)
        {
            _student = student;

            FetchLessons();

            CreateModelsArray();

            CalculateBorderDates();

            FillTempModels();

            return GetJsonModels();
        }

        private void CreateModelsArray()
        {
            _tempModels = Enum.GetValues<DayOfWeek>().Select(day => new ScheduleDayLessonsTempModel
            {
                Day = day
            }).ToArray();
        }

        private void FetchLessons()
        {
            _periodic = _student.OrganizationalClass?.Schedule.ToList() ?? new List<PeriodicLesson>();

            if (_student.SubjectClasses is not null)
            {
                _periodic = _periodic.Concat(_student.SubjectClasses.SelectMany(x => x.Schedule));
            }
        }

        private void CalculateBorderDates()
        {
            (_from, _to) = DatesHelper.GetStartAndEndOfCurrentWeek();
        }

        private void FillTempModels()
        {
            foreach (var periodic in _periodic!)
            {
                var cron = CronExpression.Parse(periodic.CronPeriodicity);

                var occurances = cron.GetOccurrences(_from, _to);

                foreach (var occurance in occurances)
                {
                    var time = occurance.TimeOfDay;

                    var lesson = new LessonTimetableEntryJson
                    {
                        id = periodic.Id,
                        customDuration = periodic.CustomDuration,
                        time = new TimeJson { hour = time.Hours, minutes = time.Minutes },
                        subject = new IdNameJson { id = periodic.Subject.Id, name = periodic.Subject.Name },
                        room = new IdNameJson { id = periodic.Room.Id, name = periodic.Room.Name },
                        lecturer = new IdNameJson { id = periodic.Lecturer.Id, name = periodic.Lecturer.GetShortenedName() }
                    };

                    _tempModels.First(x => x.Day == occurance.DayOfWeek).Lessons.Add(lesson);
                }
            }
        }

        private ScheduleDayLessonsJson[] GetJsonModels()
        {
            return _tempModels.Select(x => new ScheduleDayLessonsJson
            {
                dayIndicator = x.Day,
                lessons = x.Lessons.ToArray()
            }).ToArray();
        }

        private class ScheduleDayLessonsTempModel
        {
            public DayOfWeek Day { get; set; }
            public IList<LessonTimetableEntryJson> Lessons { get; set; } = new List<LessonTimetableEntryJson>();
        }
    }
}
