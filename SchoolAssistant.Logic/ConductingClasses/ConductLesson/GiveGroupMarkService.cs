using Microsoft.EntityFrameworkCore;
using SchoolAssistant.DAL.Models.Lessons;
using SchoolAssistant.DAL.Models.Marks;
using SchoolAssistant.DAL.Repositories;
using SchoolAssistant.Infrastructure.Enums.Marks;
using SchoolAssistant.Infrastructure.Models.ConductingClasses.ConductLesson;
using SchoolAssistant.Infrastructure.Models.Shared.Json;

namespace SchoolAssistant.Logic.ConductingClasses.ConductLesson
{
    public interface IGiveGroupMarkService
    {
        Task<ResponseJson> GiveAsync(GiveGroupMarkJson model);
    }

    [Injectable]
    public class GiveGroupMarkService : IGiveGroupMarkService
    {
        private readonly IRepositoryBySchoolYear<Lesson> _lessonRepo;
        private readonly IRepositoryBySchoolYear<MarksOfClass> _marksCollectionRepo;


        private GiveGroupMarkJson _model = null!;
        private Lesson _lesson = null!;
        private IEnumerable<StudentMarkJson> _validatedStudentMarks = null!;
        private ResponseJson _response = null!;

        public GiveGroupMarkService(
            IRepositoryBySchoolYear<Lesson> lessonRepo,
            IRepositoryBySchoolYear<MarksOfClass> marksCollectionRepo)
        {
            _lessonRepo = lessonRepo;
            _marksCollectionRepo = marksCollectionRepo;
        }

        public async Task<ResponseJson> GiveAsync(GiveGroupMarkJson model)
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

            if (String.IsNullOrEmpty(_model.description))
            {
                _response.message = "Brakuje opisu grupy ocen";
                return false;
            }

            _lesson = (await _lessonRepo.AsQueryableByYear.ByCurrent()
                .Include(x => x.FromSchedule)
                .FirstOrDefaultAsync(x => x.Id == _model.lessonId).ConfigureAwait(false))!;

            if (_lesson is null)
            {
                _response.message = "Nieprawidłowy Id zajęć";
                return false;
            }

            var validatedStudentMarks = new List<StudentMarkJson>();
            foreach (var mark in _model.marks)
            {
                if (!_lesson.FromSchedule.ParticipatingOrganizationalClass?.Students.Any(x => x.Id == mark.studentId) ?? true)
                    continue;

                if (!Enum.IsDefined(typeof(MarkValue), mark.mark.value))
                    continue;

                validatedStudentMarks.Add(mark);
            }

            if (!validatedStudentMarks.Any())
            {
                _response.message = "Przekazane oceny są nieprawidłowe";
                return false;
            }

            _validatedStudentMarks = validatedStudentMarks;

            return true;
        }

        private async Task AddAsync()
        {
            var collection = new MarksOfClass
            {
                Description = _model.description,
                OrganizationalClassId = _lesson.FromSchedule.ParticipatingOrganizationalClassId,
                SchoolYearId = _lesson.SchoolYearId,
                Weight = _model.weight,
                Marks = _validatedStudentMarks.Select(x => new Mark
                {
                    Weight = _model.weight,
                    IssuerId = _lesson.FromSchedule.LecturerId,
                    IssueDate = DateTime.Now,
                    SchoolYearId = _lesson.SchoolYearId,
                    StudentId = x.studentId,
                    SubjectId = _lesson.FromSchedule.SubjectId,
                    Main = x.mark.value,
                    Prefix = ConvertPrefix(x.mark.prefix)
                }).ToHashSet()
            };

            _marksCollectionRepo.Add(collection);
            await _marksCollectionRepo.SaveAsync().ConfigureAwait(false);
        }

        private MarkPrefix? ConvertPrefix(string? value)
        {
            return value switch
            {
                "+" => MarkPrefix.Plus,
                "-" => MarkPrefix.Minus,
                _ => null
            };
        }
    }
}
