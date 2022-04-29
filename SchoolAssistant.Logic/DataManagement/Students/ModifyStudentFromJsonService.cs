using Microsoft.EntityFrameworkCore;
using SchoolAssistant.DAL.Models.StudentsOrganization;
using SchoolAssistant.DAL.Models.StudentsParents;
using SchoolAssistant.DAL.Repositories;
using SchoolAssistant.Infrastructure.Models.DataManagement.Students;
using SchoolAssistant.Infrastructure.Models.Shared.Json;

namespace SchoolAssistant.Logic.DataManagement.Students
{
    public interface IModifyStudentFromJsonService
    {
        Task<ResponseJson> CreateOrUpdateAsync(StudentDetailsJson model);
    }

    [Injectable]
    public class ModifyStudentFromJsonService : IModifyStudentFromJsonService
    {
        private readonly IRepositoryBySchoolYear<Student> _studentRepo;
        private readonly IRepository<StudentRegisterRecord> _registerRepo;
        private readonly IRepository<OrganizationalClass> _orgClassRepo;

        private StudentDetailsJson _model = null!;
        private Student _entity = null!;
        private ResponseJson _response = null!;
        private OrganizationalClass _classToAdd = null!;

        public ModifyStudentFromJsonService(
            IRepositoryBySchoolYear<Student> studentRepo,
            IRepository<StudentRegisterRecord> registerRepo,
            IRepository<OrganizationalClass> orgClassRepo)
        {
            _studentRepo = studentRepo;
            _registerRepo = registerRepo;
            _orgClassRepo = orgClassRepo;
        }


        public async Task<ResponseJson> CreateOrUpdateAsync(StudentDetailsJson model)
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

            if (_model.id.HasValue && !await _studentRepo.ExistsAsync(_model.id.Value))
            {
                _response.message = "Błąd! Modyfikowany student nie istnieje";
                return false;
            }

            if (!_model.registerRecordId.HasValue)
            {
                _response.message = "Brakuje odniesienia do rejestru uczniów";
                return false;
            }

            if (!await _registerRepo.ExistsAsync(_model.registerRecordId.Value))
            {
                _response.message = "Wskazany rekord z rejestru uczniów nie istnieje";
                return false;
            }

            if (!_model.organizationalClassId.HasValue)
            {
                _response.message = "Brakuje klasy";
                return false;
            }

            if (!await _orgClassRepo.ExistsAsync(_model.organizationalClassId.Value))
            {
                _response.message = "Wskazana klasa nie istnieje";
                return false;
            }

            if (!_model.numberInJournal.HasValue)
            {
                _response.message = "Nie podano numeru w dzienniku";
                return false;
            }

            return true;
        }

        private async Task UpdateAsync()
        {
            _entity = (await _studentRepo.GetByIdAsync(_model.id!.Value))!;
            _classToAdd = (await _orgClassRepo.GetByIdAsync(_model.organizationalClassId!.Value))!;

            await RemoveStudentWithSameInfoAtSameYear();
            await AssignNumberAndMoveOtherUpAsync(_entity, _model.numberInJournal!.Value);

            _entity.SchoolYearId = _classToAdd.SchoolYearId;

            _entity.InfoId = _model.registerRecordId!.Value;
            _entity.OrganizationalClassId = _classToAdd.Id;
            _entity.NumberInJournal = _model.numberInJournal!.Value;

            _studentRepo.Update(_entity);
            await _studentRepo.SaveAsync();
        }

        private async Task CreateAsync()
        {

        }

        private async Task AssignNumberAndMoveOtherUpAsync(Student student, int number)
        {
            var studentWithDesiredNumber = _classToAdd.Students.FirstOrDefault(x => x.NumberInJournal == number);

            if (studentWithDesiredNumber is not null)
                await AssignNumberAndMoveOtherUpAsync(studentWithDesiredNumber, number + 1);

            student.NumberInJournal = number;
        }

        private async Task RemoveStudentWithSameInfoAtSameYear()
        {
            var query = _studentRepo.AsQueryableByYear.ByYearOf(_classToAdd)
                .Where(x => x.InfoId == _model.registerRecordId!.Value && x.Id != _model.id);

            var list = await _studentRepo.AsListByYear.ByYearOfAsync(_classToAdd);

            if (!await query.AnyAsync()) return;

            _studentRepo.RemoveRange(query);
        }
    }
}
