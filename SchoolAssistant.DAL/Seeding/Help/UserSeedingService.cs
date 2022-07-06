using Microsoft.Extensions.Configuration;
using SchoolAssistant.DAL.Attributes;
using SchoolAssistant.DAL.Enums;
using SchoolAssistant.DAL.Models.AppStructure;
using SchoolAssistant.DAL.Models.Staff;
using SchoolAssistant.DAL.Models.StudentsParents;
using SchoolAssistant.DAL.Repositories;

namespace SchoolAssistant.DAL.Seeding.Help
{
    public interface IUserSeedingService
    {
        Task SeedAllAsync();
        Task SeedStudentAsync();
        Task SeedSystemAdminAsync();
        Task SeedTeacherAsync();
    }

    [Injectable]
    internal class UserSeedingService : IUserSeedingService
    {
        private readonly IConfiguration _config;
        private readonly IUserRepository _userRepo;
        private readonly IRepository<Teacher> _teacherRepo;
        private readonly IRepository<StudentRegisterRecord> _studentRepo;

        public UserSeedingService(
            IConfiguration config,
            IUserRepository userRepo,
            IRepository<Teacher> teacherRepo,
            IRepository<StudentRegisterRecord> studentRepo)
        {
            _config = config;
            _userRepo = userRepo;
            _teacherRepo = teacherRepo;
            _studentRepo = studentRepo;
        }

        public async Task SeedAllAsync()
        {
            await SeedSystemAdminAsync().ConfigureAwait(false);
            await SeedTeacherAsync().ConfigureAwait(false);
            await SeedStudentAsync().ConfigureAwait(false);
        }

        public async Task SeedSystemAdminAsync()
        {
            CreateUserTemplateFor(UserType.SystemAdmin);
            await CheckAndCreateAsync().ConfigureAwait(false);
        }

        public async Task SeedTeacherAsync()
        {
            CreateUserTemplateFor(UserType.Teacher);
            await CheckAndCreateAsync().ConfigureAwait(false);
        }

        public async Task SeedStudentAsync()
        {
            CreateUserTemplateFor(UserType.Student);
            await CheckAndCreateAsync().ConfigureAwait(false);
        }

        private async Task CheckAndCreateAsync()
        {
            if (!await ValidateTemplateDataAsync().ConfigureAwait(false))
                return;
            await SeedOrChangeUserAsync().ConfigureAwait(false);
        }


        private User? _userTemplate;
        private string? _password;

        private void CreateUserTemplateFor(UserType type)
        {
            _userTemplate = type switch
            {
                UserType.SystemAdmin => new()
                {
                    Email = "system.admin@mail.com",
                    NormalizedEmail = "SYSTEM.ADMIN@MAIL.COM",
                    UserName = _config["PreviewMode:Logins:Administrator:UserName"],
                    NormalizedUserName = _config["PreviewMode:Logins:Administrator:UserName"]?.ToUpper(),
                    EmailConfirmed = true,
                    PhoneNumber = "+48111111111",
                    PhoneNumberConfirmed = true,
                    Type = type,
                    SecurityStamp = Guid.NewGuid().ToString("D")
                },
                UserType.Teacher => new()
                {
                    Email = "sample.teacher@mail.com",
                    NormalizedEmail = "SAMPLE.TEACHER@MAIL.COM",
                    UserName = _config["PreviewMode:Logins:Teacher:UserName"],
                    NormalizedUserName = _config["PreviewMode:Logins:Teacher:UserName"]?.ToUpper(),
                    EmailConfirmed = true,
                    PhoneNumber = "+48111111111",
                    PhoneNumberConfirmed = true,
                    Type = type,
                    SecurityStamp = Guid.NewGuid().ToString("D"),
                    TeacherId = long.TryParse(_config["PreviewMode:Logins:Teacher:RelatedEntityId"], out var teacherId) ? teacherId : null
                },
                UserType.Student => new()
                {
                    Email = "sample.student@mail.com",
                    NormalizedEmail = "SAMPLE.STUDENT@MAIL.COM",
                    UserName = _config["PreviewMode:Logins:Student:UserName"],
                    NormalizedUserName = _config["PreviewMode:Logins:Student:UserName"]?.ToUpper(),
                    EmailConfirmed = true,
                    PhoneNumber = "+48111111111",
                    PhoneNumberConfirmed = true,
                    Type = type,
                    SecurityStamp = Guid.NewGuid().ToString("D"),
                    TeacherId = long.TryParse(_config["PreviewMode:Logins:Student:RelatedEntityId"], out var studentId) ? studentId : null
                },
                _ => null,
            };

            _password = type switch
            {
                UserType.SystemAdmin => _config["PreviewMode:Logins:Administrator:Password"],
                UserType.Teacher => _config["PreviewMode:Logins:Teacher:Password"],
                UserType.Student => _config["PreviewMode:Logins:Student:Password"],
                _ => null,
            };
        }

        private async Task<bool> ValidateTemplateDataAsync()
        {
            if (_userTemplate is null)
                return false;

            if (String.IsNullOrWhiteSpace(_userTemplate.UserName)
                || String.IsNullOrWhiteSpace(_userTemplate.NormalizedUserName)
                || String.IsNullOrWhiteSpace(_userTemplate.Email)
                || String.IsNullOrWhiteSpace(_userTemplate.NormalizedEmail)
                || !_userTemplate.EmailConfirmed
                || !_userTemplate.PhoneNumberConfirmed)
                return false;

            if (String.IsNullOrWhiteSpace(_password))
                return false;

            switch (_userTemplate.Type)
            {
                case UserType.SystemAdmin:
                    return true;
                case UserType.Teacher:
                    return _userTemplate.TeacherId.HasValue
                        && await _teacherRepo.ExistsAsync(_userTemplate.TeacherId.Value).ConfigureAwait(false)
                        && !await _userRepo.ExistsAsync(x => x.UserName != _userTemplate.UserName && x.TeacherId == _userTemplate.TeacherId).ConfigureAwait(false);
                case UserType.Student:
                    return _userTemplate.StudentId.HasValue
                        && await _studentRepo.ExistsAsync(_userTemplate.StudentId.Value).ConfigureAwait(false)
                        && !await _userRepo.ExistsAsync(x => x.UserName != _userTemplate.UserName && x.StudentId == _userTemplate.StudentId).ConfigureAwait(false);
                default:
                    return false;
            }
        }

        private async Task SeedOrChangeUserAsync()
        {
            if (_userTemplate is null || _password is null)
                return;

            var userInDb = await _userRepo.Manager.FindByNameAsync(_userTemplate.UserName).ConfigureAwait(false);

            if (userInDb is null)
            {
                await _userRepo.Manager.CreateAsync(_userTemplate).ConfigureAwait(false);
                await _userRepo.Manager.AddPasswordAsync(_userTemplate, _password).ConfigureAwait(false);

                await _userRepo.Manager.AddToRoleAsync(_userTemplate, _userTemplate.Type.GetUserTypeAttribute()!.RoleName).ConfigureAwait(false);
                return;
            }

            if (await _userRepo.Manager.CheckPasswordAsync(userInDb, _password).ConfigureAwait(false))
            {
                await _userRepo.Manager.RemovePasswordAsync(userInDb).ConfigureAwait(false);
                await _userRepo.Manager.AddPasswordAsync(userInDb, _password).ConfigureAwait(false);
                return;
            }
        }

    }
}
