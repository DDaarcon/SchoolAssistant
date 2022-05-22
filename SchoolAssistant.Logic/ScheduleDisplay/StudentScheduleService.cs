using Cronos;
using SchoolAssistant.DAL.Models.Lessons;
using SchoolAssistant.DAL.Models.StudentsParents;
using SchoolAssistant.DAL.Repositories;
using SchoolAssistant.Infrastructure.Models.ScheduleDisplay;
using SchoolAssistant.Logic.Help;

namespace SchoolAssistant.Logic.Schedule
{
    public interface IStudentScheduleService
    {
        Task<ScheduleEventJson[]?> GetModelAsync(long studentId);
        ScheduleEventJson[]? GetModel(Student student);
    }

    [Injectable]
    public class StudentScheduleService : IStudentScheduleService
    {
        private const int DEFAULT_LESSON_DURATION = 45;

        private readonly IRepository<Student> _studentRepo;

        private Student _student = null!;
        private IList<PeriodicLesson>? _periodic;
        private DateTime _from;
        private DateTime _to;

        private ScheduleModel? _model;

        public StudentScheduleService(
            IRepository<Student> studentRepo)
        {
            _studentRepo = studentRepo;
        }

        public async Task<ScheduleEventJson[]?> GetModelAsync(long studentId)
        {
            var student = await _studentRepo.GetByIdAsync(studentId);
            if (student is null)
                return null;

            return GetModel(student);
        }

        public ScheduleEventJson[]? GetModel(Student student)
        {
            _student = student;

            FetchLessons();

            _model = new ScheduleModel();

            CalculateBorderDates();

            FillModel();

            return null;
        }

        private void FetchLessons()
        {
            var periodicLessons = _student.OrganizationalClass?.Schedule.ToList() ?? new List<PeriodicLesson>();

            if (_student.SubjectClasses is not null)
            {
                periodicLessons.AddRange(_student.SubjectClasses.SelectMany(x => x.Schedule));
            }
        }

        private void CalculateBorderDates()
        {
            (_from, _to) = DatesHelper.GetStartAndEndOfCurrentWeek();
        }

        private void FillModel()
        {
            _model!.Earliest = new TimeSpan(23, 59, 59);
            _model!.Latest = new TimeSpan(0, 0, 0);

            foreach (var periodic in _periodic!)
            {
                var cron = CronExpression.Parse(periodic.CronPeriodicity);

                var occurances = cron.GetOccurrences(_from, _to);

                foreach (var occurance in occurances)
                {
                    if (_model.Earliest > occurance.TimeOfDay) _model.Earliest = occurance.TimeOfDay;
                    if (_model.Latest < occurance.TimeOfDay) _model.Latest = occurance.TimeOfDay;

                    if (occurance.DayOfWeek == DayOfWeek.Sunday) _model.AnyInSunday = true;
                    if (occurance.DayOfWeek == DayOfWeek.Saturday) _model.AnyInSaturday = true;

                    var lesson = new ScheduleItemModel
                    {
                        Name = periodic.Subject.Name,
                        Room = periodic.Room.Name,
                        Start = occurance,
                        End = occurance.AddMinutes(periodic.CustomDuration ?? DEFAULT_LESSON_DURATION),
                        TeacherName = periodic.Lecturer.GetFullName()
                    };

                    _model.Lessons.Add(lesson);
                }
            }
        }
    }
}
