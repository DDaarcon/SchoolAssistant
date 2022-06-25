using SchoolAssistant.DAL.Interfaces;
using SchoolAssistant.DAL.Models.Shared;
using SchoolAssistant.DAL.Models.Staff;
using SchoolAssistant.DAL.Models.StudentsParents;
using SchoolAssistant.DAL.Repositories;
using SchoolAssistant.Infrastructure.Enums.Users;
using SchoolAssistant.Infrastructure.Models.UsersManagement;
using SchoolAssistant.Infrastructure.ModelValidation;
using System.ComponentModel.DataAnnotations;

namespace SchoolAssistant.Logic.UsersManagement._Validation
{
    public interface IAddUserRequestJsValidator : IValidatorBaseWithResp<AddUserRequestJson, AddUserResponseJson>
    {
        AddUserRequestJsSideEffects SideEffects { get; set; }
    }

    public class AddUserRequestJsSideEffects
    {
        public IHasUser? Related { get; set; }
    }

    [Injectable]
    public class AddUserRequestJsValidator : ValidatorBaseWithResp<AddUserRequestJson, AddUserResponseJson>, IAddUserRequestJsValidator
    {
        private readonly IUserRepository _userRepo;
        private readonly IRepository<StudentRegisterRecord> _studentRegRecRepo;
        private readonly IRepository<Teacher> _teacherRepo;
        private readonly IRepository<Parent> _parentRepo;

        public AddUserRequestJsValidator(
            IUserRepository userRepo,
            IRepository<StudentRegisterRecord> studentRegRecRepo,
            IRepository<Teacher> teacherRepo,
            IRepository<Parent> parentRepo)
        {
            _userRepo = userRepo;
            _studentRegRecRepo = studentRegRecRepo;
            _teacherRepo = teacherRepo;
            _parentRepo = parentRepo;
        }

        public AddUserRequestJsSideEffects SideEffects { get; set; } = null!;


        protected override async Task<bool> ImplementationIIAsync(CancellationToken? cancellationToken = null)
        {
            SideEffects = new();

            if (!await BasicAsync().ConfigureAwait(false))
                return false;

            if (!await TypeRelatedAsync().ConfigureAwait(false))
                return false;

            return true;
        }


        private readonly EmailAddressAttribute _emailValidator = new();

        private async Task<bool> BasicAsync()
        {
            if (_model is null)
                return FalseWithMsgRegistration("Błąd! Brakuje modelu danych");

            if (!UserNameFormat()) return false;

            if (!await UserNameAvailabilityAsync().ConfigureAwait(false)) return false;

            if (!EmailFormat()) return false;

            if (!await EmailAvailabilityAsync().ConfigureAwait(false)) return false;

            return true;
        }

        private bool UserNameFormat()
        {
            if (string.IsNullOrWhiteSpace(_model.userName))
                return FalseWithMsgRegistration("Brakuje nazwy użytkownika");

            if (_model.userName.Contains(' '))
                return FalseWithMsgRegistration("Nazwa użytkownika nie powinna zawierać odstępów");

            return true;
        }

        private async Task<bool> UserNameAvailabilityAsync()
        {
            if (await _userRepo.ExistsAsync(x => x.NormalizedUserName == _model.userName.ToUpper()))
                return FalseWithMsgRegistration("Istnieje już użytkownik o tej nazwie");

            return true;
        }

        private bool EmailFormat()
        {
            if (string.IsNullOrWhiteSpace(_model.email))
                return FalseWithMsgRegistration("Brakuje adresu email");

            if (!_emailValidator.IsValid(_model.email))
                return FalseWithMsgRegistration("Nieprawidłowy adres email");

            return true;
        }

        private async Task<bool> EmailAvailabilityAsync()
        {
            if (await _userRepo.ExistsAsync(x => x.NormalizedEmail == _model.email.ToUpper()))
                return FalseWithMsgRegistration("Istnieje już użytkownik z tym adresem email");

            return true;
        }








        private async Task<bool> TypeRelatedAsync()
        {
            if (!Enum.IsDefined(_model.relatedType))
                return FalseWithMsgRegistration("Błąd! Nieprawidłowy typ powiązanego obiektu");

            if (!await RelatedEntityAsync().ConfigureAwait(false))
                return false;

            return true;
        }

        private async Task<bool> RelatedEntityAsync()
        {

            switch (_model.relatedType)
            {
                case UserTypeForManagement.Student:
                    await FetchRelatedEntityAsync(_studentRegRecRepo);
                    break;
                case UserTypeForManagement.Teacher:
                    await FetchRelatedEntityAsync(_teacherRepo);
                    break;
                case UserTypeForManagement.Parent:
                    await FetchRelatedEntityAsync(_parentRepo);
                    break;
                default:
                    throw new NotImplementedException();
            }

            if (SideEffects.Related is null)
                return FalseWithMsgRegistration("Błąd! Powiązany obiekt nie istenieje");

            if (SideEffects.Related.User is not null)
                return FalseWithMsgRegistration("Błąd! Powiązany obiekt ma już użytkownika");

            return true;
        }

        private async Task FetchRelatedEntityAsync<TRelated>(IRepository<TRelated> repo)
            where TRelated : DbEntity, IHasUser
        {
            SideEffects.Related = await repo.GetByIdAsync(_model.relatedId);
        }

    }
}
