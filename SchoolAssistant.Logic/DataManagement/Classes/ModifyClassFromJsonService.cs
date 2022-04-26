using Microsoft.EntityFrameworkCore;
using SchoolAssistant.DAL.Models.StudentsOrganization;
using SchoolAssistant.DAL.Repositories;
using SchoolAssistant.Infrastructure.Models.DataManagement.Classes;
using SchoolAssistant.Infrastructure.Models.Shared.Json;

namespace SchoolAssistant.Logic.DataManagement.Classes
{
    public interface IModifyClassFromJsonService
    {
        Task<ResponseJson> CreateOrUpdateAsync(ClassDetailsJson model);
    }

    [Injectable]
    public class ModifyClassFromJsonService : IModifyClassFromJsonService
    {
        private readonly IRepository<OrganizationalClass> _repo;
        private readonly ISchoolYearService _yearSvc;

        private OrganizationalClass _entity = null!;
        private ClassDetailsJson _model = null!;
        private ResponseJson _response = null!;

        public ModifyClassFromJsonService(
            IRepository<OrganizationalClass> repo,
            ISchoolYearService yearSvc)
        {
            _repo = repo;
            _yearSvc = yearSvc;
        }


        public async Task<ResponseJson> CreateOrUpdateAsync(ClassDetailsJson model)
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
            if (_model is null)
            {
                _response.message = "Błąd! Brakuje modelu";
                return false;
            }

            if (_model.id.HasValue && !await _repo.ExistsAsync(_model.id!.Value))
            {
                _response.message = "Błąd! Modyfikowana klasa nie istnieje";
                return false;
            }

            if (_model.grade < 0)
            {
                _response.message = "Klasa nie może mieć wartości mniejszej niż zero";
                return false;
            }

            _model.distinction = _model.distinction?.Trim();

            if (await _repo.AsQueryable().AnyAsync(x =>
                x.Grade == _model.grade
                && x.Distinction == _model.distinction))
            {
                _response.message = "Klasa z takim numerem i identyfikatorem już istnieje";
                return false;
            }

            return true;
        }

        private async Task UpdateAsync()
        {
            _entity = (await _repo.GetByIdAsync(_model.id!.Value))!;

            _entity.Grade = _model.grade;
            _entity.Distinction = _model.distinction;
            _entity.Specialization = _model.specialization;

            _repo.Update(_entity);

            await _repo.SaveAsync();
        }

        private async Task CreateAsync()
        {
            var year = await _yearSvc.GetOrCreateCurrentAsync();

            _entity = new OrganizationalClass
            {
                SchoolYearId = year.Id,
                Grade = _model.grade,
                Distinction = _model.distinction,
                Specialization = _model.specialization
            };

            await _repo.AddAsync(_entity);

            await _repo.SaveAsync();
        }
    }
}
