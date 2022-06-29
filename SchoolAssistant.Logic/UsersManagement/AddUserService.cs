using Microsoft.AspNetCore.Identity;
using SchoolAssistant.DAL.Attributes;
using SchoolAssistant.DAL.Enums;
using SchoolAssistant.DAL.Interfaces;
using SchoolAssistant.DAL.Models.AppStructure;
using SchoolAssistant.DAL.Repositories;
using SchoolAssistant.Infrastructure.Enums.Users;
using SchoolAssistant.Infrastructure.Models.UsersManagement;
using SchoolAssistant.Logic.General.Other;
using SchoolAssistant.Logic.Help;
using SchoolAssistant.Logic.UsersManagement._Validation;

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
        private readonly ITextCryptographicService _deformationSvc;

        private readonly IAddUserRequestJsValidator _modelValidator;


        private AddUserRequestJson _model = null!;
        private AddUserResponseJson _response = null!;

        private IHasUser? _related;
        private User? _user;
        private string? _temporaryPassword;

        public AddUserService(
            IUserRepository userRepo,
            ITextCryptographicService deformationSvc,
            IAddUserRequestJsValidator modelValidator)
        {
            _userRepo = userRepo;
            _deformationSvc = deformationSvc;
            _modelValidator = modelValidator;
        }

        public async Task<AddUserResponseJson> AddAsync(AddUserRequestJson model)
        {
            _model = model;

            bool validationResult = await _modelValidator.ValidateAsync(_model).ConfigureAwait(false);
            _response = _modelValidator.Response;

            if (!validationResult)
                return _response;


            _related = _modelValidator.SideEffects.Related;

            await CreateUserAsync().ConfigureAwait(false);

            if (_response.success)
            {
                var currentUser = await _userRepo.GetCurrentUserCachedAsync().ConfigureAwait(false);
                (bool success, string? encrypted) = await _deformationSvc.GetEncryptedAsync(_temporaryPassword!, currentUser?.Id.ToString()).ConfigureAwait(false);

                if (success)
                    _response.passwordDeformed = encrypted;
            }

            return _response;
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

            SetTypeSpecific();

            if (!await CreateUserEntityAsync().ConfigureAwait(false))
                return;

            await AddRoleAsync().ConfigureAwait(false);

            if (!await AddTemporaryPasswordAsync().ConfigureAwait(false))
            {
                _userRepo.Remove(_user);
                await _userRepo.SaveAsync().ConfigureAwait(false);
            }
        }

        private void SetTypeSpecific()
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
            var result = await _userRepo.Manager.CreateAsync(_user!).ConfigureAwait(false);
            if (!result.Succeeded)
            {
                _response.message = ParseIdentityErrors(result);
                return false;
            }

            return true;
        }

        private async Task AddRoleAsync()
        {
            var attr = _user!.Type.GetUserTypeAttribute();

            await _userRepo.Manager.AddToRoleAsync(_user, attr.RoleName).ConfigureAwait(false);
        }

        private async Task<bool> AddTemporaryPasswordAsync()
        {
            _temporaryPassword = PasswordHelper.GenerateRandom();

            var result = await _userRepo.Manager.AddPasswordAsync(_user!, _temporaryPassword).ConfigureAwait(false);
            if (!result.Succeeded)
            {
                _response.message = ParseIdentityErrors(result);
                return false;
            }

            return true;
        }

        private string ParseIdentityErrors(IdentityResult result) => String.Join(" ", result.Errors.Select(x => x.Description));
    }
}
