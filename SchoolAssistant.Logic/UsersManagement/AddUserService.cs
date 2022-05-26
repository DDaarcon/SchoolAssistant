using Microsoft.AspNetCore.Identity;
using SchoolAssistant.DAL.Enums;
using SchoolAssistant.DAL.Interfaces;
using SchoolAssistant.DAL.Models.AppStructure;
using SchoolAssistant.DAL.Models.Shared;
using SchoolAssistant.DAL.Models.Staff;
using SchoolAssistant.DAL.Models.StudentsParents;
using SchoolAssistant.DAL.Repositories;
using SchoolAssistant.Infrastructure.Enums.Users;
using SchoolAssistant.Infrastructure.Models.UsersManagement;
using SchoolAssistant.Logic.Help;
using System.ComponentModel.DataAnnotations;

namespace SchoolAssistant.Logic.UsersManagement
{
    public interface IAddUserService
    {
        Task<AddUserResponseJson> AddAsync(AddUserRequestJson model);
    }

    [Injectable]
    public class AddUserService : IAddUserService
    {
        private readonly IUserRepository _userRepo;
        private readonly IRepository<StudentRegisterRecord> _studentRegRecRepo;
        private readonly IRepository<Teacher> _teacherRepo;
        private readonly IRepository<Parent> _parentRepo;

        private readonly EmailAddressAttribute _emailValidator = new();

        private AddUserRequestJson _model = null!;
        private AddUserResponseJson _response = null!;

        private IHasUser? _related;
        private User? _user;
        private string? _temporaryPassword;

        public AddUserService(
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

        public async Task<AddUserResponseJson> AddAsync(AddUserRequestJson model)
        {
            _model = model;
            _response = new AddUserResponseJson();

            if (!await ValidateAsync())
                return _response;

            await CreateUserAsync();

            if (_response.success)
                _response.temporaryPassword = _temporaryPassword!;

            return _response;
        }

        private async Task<bool> ValidateAsync()
        {
            if (_model is null)
                return ValidationFail("Błąd! Brakuje modelu danych");

            if (String.IsNullOrWhiteSpace(_model.userName))
                return ValidationFail("Brakuje nazwy użytkownika");

            if (_model.userName.Contains(' '))
                return ValidationFail("Nazwa użytkownika nie powinna zawierać odstępów");

            if (await _userRepo.ExistsAsync(x => x.NormalizedUserName == _model.userName.ToUpper()))
                return ValidationFail("Istnieje już użytkownik o tej nazwie");

            if (String.IsNullOrWhiteSpace(_model.email))
                return ValidationFail("Brakuje adresu email");

            if (!_emailValidator.IsValid(_model.email))
                return ValidationFail("Nieprawidłowy adres email");

            if (await _userRepo.ExistsAsync(x => x.NormalizedEmail == _model.email.ToUpper()))
                return ValidationFail("Istnieje już użytkownik z tym adresem email");

            if (!await ValidateRelatedEntity())
                return false;

            return true;
        }

        private async Task<bool> ValidateRelatedEntity()
        {
            if (!Enum.IsDefined(_model.relatedType))
                return ValidationFail("Błąd! Nieprawidłowy typ powiązanego obiektu");

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

            if (_related is null)
                return ValidationFail("Błąd! Powiązany obiekt nie istenieje");

            if (_related.User is not null)
                return ValidationFail("Błąd! Powiązany obiekt ma już użytkownika");

            return true;
        }

        private async Task FetchRelatedEntityAsync<TRelated>(IRepository<TRelated> repo)
            where TRelated : DbEntity, IHasUser
        {
            _related = await repo.GetByIdAsync(_model.relatedId);
        }

        /// <returns> <c>false</c> </returns>
        private bool ValidationFail(string msg)
        {
            _response.message = msg;
            return false;
        }


        private async Task CreateUserAsync()
        {
            _user = new User
            {
                UserName = _model.userName,
                NormalizedUserName = _model.userName.ToUpper(),
                Email = _model.email,
                EmailConfirmed = false,
                NormalizedEmail = _model.email.ToUpper(),
                PhoneNumber = _model.phoneNumber
            };

            SetUsersRelatedEntity();

            if (!await CreateUserEntityAsync())
                return;

            if (!await AddTemporaryPasswordToUser())
            {
                _userRepo.Remove(_user);
                await _userRepo.SaveAsync();
            }
        }

        private void SetUsersRelatedEntity()
        {
            switch (_model.relatedType)
            {
                case UserTypeForManagement.Student:
                    _user!.StudentId = _related!.Id;
                    _user.Type = UserType.Student;
                    break;
                case UserTypeForManagement.Teacher:
                    _user!.TeacherId = _related!.Id;
                    _user.Type = UserType.Teacher;
                    break;
                case UserTypeForManagement.Parent:
                    _user!.ParentId = _related!.Id;
                    _user.Type = UserType.Parent;
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        private async Task<bool> CreateUserEntityAsync()
        {
            var result = await _userRepo.Manager.CreateAsync(_user!);
            if (!result.Succeeded)
                return ValidationFail(ParseIdentityErrors(result));

            return true;
        }

        private async Task<bool> AddTemporaryPasswordToUser()
        {
            _temporaryPassword = PasswordHelper.GenerateRandom();

            var result = await _userRepo.Manager.AddPasswordAsync(_user!, _temporaryPassword);
            if (!result.Succeeded)
                return ValidationFail(ParseIdentityErrors(result));

            return true;
        }

        private string ParseIdentityErrors(IdentityResult result) => String.Join(" ", result.Errors.Select(x => x.Description));
    }
}
