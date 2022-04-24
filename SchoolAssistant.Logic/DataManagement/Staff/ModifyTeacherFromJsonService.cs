using SchoolAssistant.DAL.Models.LinkingTables;
using SchoolAssistant.DAL.Models.Staff;
using SchoolAssistant.DAL.Models.Subjects;
using SchoolAssistant.DAL.Repositories;
using SchoolAssistant.Infrastructure.Models.DataManagement.Staff;
using SchoolAssistant.Infrastructure.Models.Shared.Json;

namespace SchoolAssistant.Logic.DataManagement.Staff
{
    public interface IModifyTeacherFromJsonService
    {
        Task<ResponseJson> CreateOrUpdateAsync(StaffPersonDetailsJson model);
    }

    [Injectable]
    public class ModifyTeacherFromJsonService : IModifyTeacherFromJsonService
    {
        private readonly IRepository<Teacher> _teacherRepo;
        private readonly IRepository<Subject> _subjectRepo;

        private ResponseJson _response = null!;
        private StaffPersonDetailsJson _model = null!;
        private Teacher _teacher = null!;

        public ModifyTeacherFromJsonService(
            IRepository<Teacher> teacherRepo,
            IRepository<Subject> subjectRepo)
        {
            _teacherRepo = teacherRepo;
            _subjectRepo = subjectRepo;
        }

        public async Task<ResponseJson> CreateOrUpdateAsync(StaffPersonDetailsJson model)
        {
            _model = model;
            _response = new ResponseJson();

            if (!await ValidateAsync())
                return _response;

            if (_model.id.HasValue)
                await UpdateAsync();
            else
                await CreateAsync();

            return _response;
        }

        private async Task<bool> ValidateAsync()
        {
            if (_model.id.HasValue && !await _teacherRepo.ExistsAsync(_model.id!.Value))
            {
                _response.message = "Błąd! Modyfikowany członek personelu nie istnieje";
                return false;
            }

            if (String.IsNullOrWhiteSpace(_model.firstName))
            {
                _response.message = "Należy podać imię";
                return false;
            }

            if (String.IsNullOrWhiteSpace(_model.lastName))
            {
                _response.message = "Należy podać imię";
                return false;
            }

            if ((_model.mainSubjectsIds?.Any(x => !_subjectRepo.Exists(x)) ?? false)
                || (_model.additionalSubjectsIds?.Any(x => !_subjectRepo.Exists(x)) ?? false))
            {
                _response.message = "Błąd! Nie odnaleziono wskazanego przedmiotu";
                return false;
            }

            return true;
        }

        private async Task UpdateAsync()
        {
            _teacher = (await _teacherRepo.GetByIdAsync(_model.id!.Value))!;

            _teacher.FirstName = _model.firstName;
            _teacher.SecondName = _model.secondName;
            _teacher.LastName = _model.lastName;

            RemoveAllOldSubjects();

            await AddNewSubjectsAsync();

            _teacherRepo.Update(_teacher);

            await _teacherRepo.SaveAsync();
        }

        private async Task CreateAsync()
        {
            _teacher = new Teacher
            {
                FirstName = _model.firstName,
                SecondName = _model.secondName,
                LastName = _model.lastName
            };

            await AddNewSubjectsAsync();

            await _teacherRepo.AddAsync(_teacher);

            await _teacherRepo.SaveAsync();
        }

        private void RemoveAllOldSubjects()
        {
            var mainSubjects = new TeacherToMainSubject[_teacher.MainSubjects.Count];
            _teacher.MainSubjects.CopyTo(mainSubjects, 0);
            foreach (var mainSubject in mainSubjects)
                _teacher.SubjectOperations.RemoveMain(mainSubject.Subject);

            var additionalSubjects = new TeacherToAdditionalSubject[_teacher.AdditionalSubjects.Count];
            _teacher.AdditionalSubjects.CopyTo(additionalSubjects, 0);
            foreach (var addSubject in additionalSubjects)
                _teacher.SubjectOperations.RemoveAdditional(addSubject.Subject);
        }

        private async Task AddNewSubjectsAsync()
        {
            await foreach (var subject in GetSubjects(_model.mainSubjectsIds))
                _teacher.SubjectOperations.AddMain(subject);
            await foreach (var subject in GetSubjects(_model.additionalSubjectsIds))
                _teacher.SubjectOperations.AddAdditional(subject);
        }

        private async IAsyncEnumerable<Subject?> GetSubjects(long[]? ids)
        {
            if (ids is null) yield break;
            foreach (long id in ids)
            {
                yield return await _subjectRepo.GetByIdAsync(id);
            }
        }
    }
}
