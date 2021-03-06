using Microsoft.EntityFrameworkCore;
using SchoolAssistant.DAL.Models.StudentsParents;
using SchoolAssistant.DAL.Repositories;
using SchoolAssistant.Infrastructure.Models.DataManagement.Students;
using SchoolAssistant.Infrastructure.Models.Shared.Json;
using System.ComponentModel.DataAnnotations;

namespace SchoolAssistant.Logic.DataManagement.Students
{
    public interface IModifyStudentRegisterRecordFromJsonService
    {
        Task<SaveResponseJson> CreateOrUpdateAsync(StudentRegisterRecordDetailsJson model);
    }

    [Injectable]
    public class ModifyStudentRegisterRecordFromJsonService : IModifyStudentRegisterRecordFromJsonService
    {
        private readonly IRepository<StudentRegisterRecord> _repo;

        private readonly EmailAddressAttribute _emailValidator = new();

        private StudentRegisterRecordDetailsJson _model = null!;
        private StudentRegisterRecord _entity = null!;
        private SaveResponseJson _response = null!;

        public ModifyStudentRegisterRecordFromJsonService(
            IRepository<StudentRegisterRecord> repo)
        {
            _repo = repo;
        }


        public async Task<SaveResponseJson> CreateOrUpdateAsync(StudentRegisterRecordDetailsJson model)
        {
            _model = model;
            _response = new SaveResponseJson();

            if (!await ValidateAsync())
                return _response;

            if (_model.id.HasValue)
                await UpdateAsync();
            else
                await CreateAsync();

            _response.id = _entity.Id;
            return _response;
        }

        private async Task<bool> ValidateAsync()
        {
            if (_model is null)
            {
                _response.message = "Błąd! Brakuje modelu";
                return false;
            }

            if (_model.id.HasValue && !await _repo.ExistsAsync(_model.id.Value))
            {
                _response.message = "Błąd! Modyfikowany uczeń nie istnieje";
                return false;
            }

            if (String.IsNullOrWhiteSpace(_model.firstName))
            {
                _response.message = "Brakuje imienia";
                return false;
            }

            if (String.IsNullOrWhiteSpace(_model.lastName))
            {
                _response.message = "Brakuje nazwiska";
                return false;
            }


            if (String.IsNullOrWhiteSpace(_model.placeOfBirth))
            {
                _response.message = "Brakuje miejsca urodzenia";
                return false;
            }

            if (String.IsNullOrEmpty(_model.dateOfBirth)
                || !DateOnly.TryParse(_model.dateOfBirth, out _))
            {
                _response.message = "Nieprawidłowa data urodzenia";
                return false;
            }

            if (String.IsNullOrWhiteSpace(_model.personalId))
            {
                _response.message = "Brakuje numeru identyfikacyjnego";
                return false;
            }

            if (await _repo.AsQueryable().AnyAsync(x => x.PersonalID == _model.personalId))
            {
                _response.message = "Istnieje już uczeń z tym numerem identyfikacyjnym";
                return false;
            }

            if (String.IsNullOrWhiteSpace(_model.address))
            {
                _response.message = "Brakuje adresu zamieszkania";
                return false;
            }

            if (_model.firstParent is null)
            {
                _response.message = "Brakuje pierwszego rodzica";
                return false;
            }

            if (!ValidateParent(_model.firstParent))
                return false;

            if (_model.secondParent is not null
                && !ValidateParent(_model.secondParent))
                return false;

            return true;
        }

        private bool ValidateParent(ParentRegisterSubrecordDetailsJson model)
        {
            if (String.IsNullOrWhiteSpace(model.firstName))
            {
                _response.message = "Brakuje imienia rodzica";
                return false;
            }

            if (String.IsNullOrWhiteSpace(model.lastName))
            {
                _response.message = "Brakuje nazwiska rodzica";
                return false;
            }

            if (String.IsNullOrWhiteSpace(model.phoneNumber))
            {
                _response.message = "Brakuje numeru telefonu rodzica";
                return false;
            }

            if (String.IsNullOrWhiteSpace(model.address))
            {
                _response.message = "Brakuje adresu rodzica";
                return false;
            }

            if (!_emailValidator.IsValid(model.email))
            {
                _response.message = "Nieprawidłowy adres email rodzica";
                return false;
            }
            return true;
        }

        private async Task UpdateAsync()
        {
            _entity = (await _repo.GetByIdAsync(_model.id!.Value))!;

            _entity.FirstName = _model.firstName;
            _entity.SecondName = _model.secondName;
            _entity.LastName = _model.lastName;

            _entity.PlaceOfBirth = _model.placeOfBirth;
            _entity.DateOfBirth = DateOnly.Parse(_model.dateOfBirth);

            _entity.Address = _model.address;
            _entity.PersonalID = _model.personalId;

            _entity.FirstParent = CreateParent(_model.firstParent);
            if (_model.secondParent is not null)
                _entity.SecondParent = CreateParent(_model.secondParent);

            _repo.Update(_entity);
            await _repo.SaveAsync();
        }

        private async Task CreateAsync()
        {
            _entity = new StudentRegisterRecord
            {
                FirstName = _model.firstName,
                SecondName = _model.secondName,
                LastName = _model.lastName,
                PlaceOfBirth = _model.placeOfBirth,
                DateOfBirth = DateOnly.Parse(_model.dateOfBirth),
                Address = _model.address,
                PersonalID = _model.personalId,
                FirstParent = CreateParent(_model.firstParent)
            };

            if (_model.secondParent is not null)
                _entity.SecondParent = CreateParent(_model.secondParent);

            _repo.Add(_entity);
            await _repo.SaveAsync();
        }

        private ParentRegisterSubrecord CreateParent(ParentRegisterSubrecordDetailsJson model)
            => new ParentRegisterSubrecord
            {
                FirstName = model.firstName,
                SecondName = model.secondName,
                LastName = model.lastName,
                Address = model.address,
                Email = model.email,
                PhoneNumber = model.phoneNumber
            };
    }
}
