using SchoolAssistant.DAL.Models.Subjects;
using SchoolAssistant.DAL.Repositories;
using SchoolAssistant.Infrastructure.Models.DataManagement.Subjects;
using SchoolAssistant.Infrastructure.Models.Shared.Json;

namespace SchoolAssistant.Logic.DataManagement.Subjects
{
    public interface IModifySubjectFromJsonService
    {
        Task<ResponseJson> CreateOrUpdateAsync(SubjectDetailsJson model);
    }

    [Injectable]
    public class ModifySubjectFromJsonService : IModifySubjectFromJsonService
    {
        private readonly IRepository<Subject> _repo;

        private ResponseJson _response = null!;
        private SubjectDetailsJson _model = null!;

        public ModifySubjectFromJsonService(
            IRepository<Subject> repo)
        {
            _repo = repo;
        }

        public async Task<ResponseJson> CreateOrUpdateAsync(SubjectDetailsJson model)
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
            if (String.IsNullOrWhiteSpace(_model.name))
            {
                _response.message = "Nie podano nazwy przdmiotu";
                return false;
            }
            if (_model.id.HasValue && !await _repo.ExistsAsync(_model.id!.Value))
            {
                _response.message = "Błąd! Modyfikowany przedmiot nie istnieje";
                return false;
            }

            return true;
        }

        private async Task UpdateAsync()
        {
            var subject = (await _repo.GetByIdAsync(_model.id!.Value))!;

            subject.Name = _model.name!;

            _repo.Update(subject);

            await _repo.SaveAsync();
        }

        private async Task CreateAsync()
        {
            var subject = new Subject
            {
                Name = _model.name!
            };

            _repo.Add(subject);

            await _repo.SaveAsync();
        }
    }
}
