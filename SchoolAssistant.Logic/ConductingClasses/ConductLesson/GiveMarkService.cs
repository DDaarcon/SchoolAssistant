using Microsoft.EntityFrameworkCore;
using SchoolAssistant.DAL.Models.Lessons;
using SchoolAssistant.DAL.Models.Marks;
using SchoolAssistant.DAL.Models.StudentsParents;
using SchoolAssistant.DAL.Repositories;
using SchoolAssistant.Infrastructure.Enums.Marks;
using SchoolAssistant.Infrastructure.Models.ConductingClasses.ConductLesson;
using SchoolAssistant.Infrastructure.Models.Shared.Json;

namespace SchoolAssistant.Logic.ConductingClasses.ConductLesson
{
    public interface IGiveMarkService
    {
        Task<ResponseJson> GiveAsync(GiveMarkJson model);
    }

    [Injectable]
    public class GiveMarkService : IGiveMarkService
    {
        private readonly IRepositoryBySchoolYear<Lesson> _lessonRepo;
        private readonly IRepositoryBySchoolYear<Student> _studentRepo;
        private readonly IRepositoryBySchoolYear<Mark> _markRepo;

        private GiveMarkJson _model = null!;
        private Lesson _lesson = null!;
        private Student _student = null!;
        private ResponseJson _response = null!;

        public GiveMarkService(
            IRepositoryBySchoolYear<Lesson> lessonRepo,
            IRepositoryBySchoolYear<Student> studentRepo,
            IRepositoryBySchoolYear<Mark> markRepo)
        {
            _lessonRepo = lessonRepo;
            _studentRepo = studentRepo;
            _markRepo = markRepo;
        }

        public async Task<ResponseJson> GiveAsync(GiveMarkJson model)
        {
            _model = model;
            _response = new ResponseJson();

            if (!await FetchAndValidateAsync().ConfigureAwait(false))
                return _response;

            await AddAsync().ConfigureAwait(false);

            return _response;
        }

        private async Task<bool> FetchAndValidateAsync()
        {
            if (_model is null)
            {
                _response.message = "Brakuje modelu danych";
                return false;
            }

            if (_model.mark is null || !Enum.IsDefined(typeof(MarkValue), _model.mark.value))
            {
                _response.message = "Brakuje lub błędna ocena";
                return false;
            }

            _lesson = (await _lessonRepo.AsQueryableByYear.ByCurrent()
                .Include(x => x.FromSchedule)
                .FirstOrDefaultAsync(x => x.Id == _model.lessonId).ConfigureAwait(false))!;

            if (_lesson is null)
            {
                _response.message = "Błędne Id zajęć";
                return false;
            }

            if (String.IsNullOrEmpty(_model.description))
            {
                _response.message = "Brakuje opisu oceny";
                return false;
            }

            _student = (await _studentRepo.GetByIdAndCurrentYearAsync(_model.studentId).ConfigureAwait(false))!;

            if (_student is null)
            {
                _response.message = "Błędne Id ucznia";
                return false;
            }

            if (_model.weight <= 0)
                _model.weight = null;

            return true;
        }

        private async Task AddAsync()
        {
            var grade = new Mark
            {
                Description = _model.description,
                IssueDate = DateTime.Now,
                IssuerId = _lesson.FromSchedule.LecturerId,
                SchoolYearId = _lesson.SchoolYearId,
                StudentId = _student.Id,
                SubjectId = _lesson.FromSchedule.SubjectId,
                Main = _model.mark.value,
                Prefix = GetPrefix(),
                Weight = _model.weight
            };

            _markRepo.Add(grade);
            await _markRepo.SaveAsync().ConfigureAwait(false);
        }

        private MarkPrefix? GetPrefix()
        {
            return _model.mark.prefix switch
            {
                "-" => MarkPrefix.Minus,
                "+" => MarkPrefix.Plus,
                _ => null
            };
        }
    }
}
