using Microsoft.EntityFrameworkCore;
using SchoolAssistant.DAL.Models.Attendance;
using SchoolAssistant.DAL.Models.Lessons;
using SchoolAssistant.DAL.Repositories;
using SchoolAssistant.Infrastructure.Enums.Attendance;
using SchoolAssistant.Infrastructure.Models.ConductingClasses.ConductLesson;
using SchoolAssistant.Infrastructure.Models.Shared.Json;

namespace SchoolAssistant.Logic.ConductingClasses.ConductLesson
{
    public interface IEditAttendanceService
    {
        Task<ResponseJson> EditAsync(AttendanceEditJson model);
    }

    [Injectable]
    public class EditAttendanceService : IEditAttendanceService
    {
        private readonly IRepositoryBySchoolYear<Lesson> _lessonRepo;


        private AttendanceEditJson _model = null!;
        private Lesson _lesson = null!;
        private IEnumerable<StudentPresenceEditJson> _validatedStudentEditModels = null!;
        private ResponseJson _response = null!;

        public EditAttendanceService(IRepositoryBySchoolYear<Lesson> lessonRepo)
        {
            _lessonRepo = lessonRepo;
        }


        public async Task<ResponseJson> EditAsync(AttendanceEditJson model)
        {
            _model = model;
            _response = new ResponseJson();

            if (!await FetchAndValidateAsync().ConfigureAwait(false))
                return _response;

            await ModifyAttendanceAsync().ConfigureAwait(false);

            return _response;
        }

        private async Task<bool> FetchAndValidateAsync()
        {
            _model.students ??= Array.Empty<StudentPresenceEditJson>();

            _lesson = (await _lessonRepo.AsQueryableByYear.ByCurrent()
                .Include(x => x.FromSchedule).ThenInclude(x => x.ParticipatingOrganizationalClass).ThenInclude(x => x.Students)
                .Include(x => x.PresenceOfStudents)
                .FirstOrDefaultAsync(x => x.Id == _model.id).ConfigureAwait(false))!;

            if (_lesson is null)
            {
                _response.message = "Nie odnaleziono zajęć o padanym Id";
                return false;
            }

            var validatedStudents = new List<StudentPresenceEditJson>();
            foreach (var student in _model.students)
            {
                if (!_lesson.FromSchedule.ParticipatingOrganizationalClass?.Students.Any(x => x.Id == student.id) ?? true)
                    continue;

                if (student.presence is null || !Enum.IsDefined(typeof(PresenceStatus), student.presence))
                    continue;

                validatedStudents.Add(student);
            }
            _validatedStudentEditModels = validatedStudents;

            return true;
        }

        private async Task ModifyAttendanceAsync()
        {
            foreach (var studentEditModel in _validatedStudentEditModels)
            {
                var presenceEntity = _lesson.PresenceOfStudents.FirstOrDefault(x => x.Id == studentEditModel.id);

                if (presenceEntity is null)
                {
                    presenceEntity = new Presence
                    {
                        LessonId = _lesson.Id,
                        SchoolYearId = _lesson.SchoolYearId,
                        StudentId = studentEditModel.id
                    };
                    _lesson.PresenceOfStudents.Add(presenceEntity);
                }

                presenceEntity.Status = studentEditModel.presence!.Value;
            }

            await _lessonRepo.SaveAsync().ConfigureAwait(false);
        }
    }
}
