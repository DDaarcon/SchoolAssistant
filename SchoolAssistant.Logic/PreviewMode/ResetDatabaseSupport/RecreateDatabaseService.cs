using Microsoft.Extensions.Configuration;
using SchoolAssistant.DAL;
using SchoolAssistant.DAL.Models.Rooms;
using SchoolAssistant.DAL.Models.Staff;
using SchoolAssistant.DAL.Models.StudentsOrganization;
using SchoolAssistant.DAL.Models.StudentsParents;
using SchoolAssistant.DAL.Models.Subjects;
using SchoolAssistant.DAL.Repositories;
using SchoolAssistant.DAL.Seeding;

namespace SchoolAssistant.Logic.PreviewMode.ResetDatabaseSupport
{
    public interface IRecreateDatabaseService
    {
        Task RecreateAsync();
    }

    [Injectable]
    public class RecreateDatabaseService : IRecreateDatabaseService
    {
        private readonly IConfiguration _config;
        private readonly IDefaultDataSeeder _seeder;
        private readonly SADbContext _dbContext;

        private readonly IRepository<Teacher> _teacherRepo;
        private readonly IRepository<Subject> _subjectRepo;
        private readonly IRepository<Room> _roomRepo;
        private readonly ISchoolYearRepository _schoolYearRepo;
        private readonly IRepository<OrganizationalClass> _orgClassRepo;
        private readonly IRepository<Student> _studentRepo;
        private readonly IRepository<StudentRegisterRecord> _studentRegRecRepo;

        private readonly ITeachersDataSupplier _teacherDataSupplier;
        private readonly ISubjectsDataSupplier _subjectDataSupplier;
        private readonly IRoomsDataSupplier _roomsDataSupplier;
        private readonly ISchoolYearDataSupplier _schoolYearDataSupplier;
        private readonly IOrganizationalClassDataSupplier _orgClassDataSupplier;
        private readonly IStudentsDataSupplier _studentDataSupplier;

        public RecreateDatabaseService(
            IDefaultDataSeeder seeder,
            SADbContext dbContext,
            IConfiguration config,
            ITeachersDataSupplier teacherDataSupplier,
            IRepository<Teacher> teacherRepo,
            IRepository<Subject> subjectRepo,
            ISubjectsDataSupplier subjectDataSupplier,
            IRoomsDataSupplier roomsDataSupplier,
            IRepository<Room> roomRepo,
            ISchoolYearDataSupplier schoolYearDataSupplier,
            ISchoolYearRepository schoolYearRepo,
            IOrganizationalClassDataSupplier orgClassDataSupplier,
            IRepository<OrganizationalClass> orgClassRepo,
            IStudentsDataSupplier studentDataSupplier,
            IRepository<Student> studentRepo,
            IRepository<StudentRegisterRecord> studentRegRecRepo)
        {
            _seeder = seeder;
            _dbContext = dbContext;
            _config = config;
            _teacherDataSupplier = teacherDataSupplier;
            _teacherRepo = teacherRepo;
            _subjectRepo = subjectRepo;
            _subjectDataSupplier = subjectDataSupplier;
            _roomsDataSupplier = roomsDataSupplier;
            _roomRepo = roomRepo;
            _schoolYearDataSupplier = schoolYearDataSupplier;
            _schoolYearRepo = schoolYearRepo;
            _orgClassDataSupplier = orgClassDataSupplier;
            _orgClassRepo = orgClassRepo;
            _studentDataSupplier = studentDataSupplier;
            _studentRepo = studentRepo;
            _studentRegRecRepo = studentRegRecRepo;
        }

        public async Task RecreateAsync()
        {
            await RecreateSchoolYearAndSaveAsync().ConfigureAwait(false);

            RecreateSubjects();
            RecreateRooms();

            await RecreateTeachersAndSaveAsync().ConfigureAwait(false);

            await RecreateOrgClassesAndSaveAsync().ConfigureAwait(false);

            await RecreateStudentsAndSaveAsync().ConfigureAwait(false);

            await _seeder.SeedAppConfigAsync().ConfigureAwait(false);
            await _seeder.SeedRolesAndUsersAsync().ConfigureAwait(false);
        }

        private Task RecreateSchoolYearAndSaveAsync()
        {
            _schoolYearRepo.Add(_schoolYearDataSupplier.Current);

            return _schoolYearRepo.SaveAsync();
        }

        private void RecreateSubjects()
        {
            _subjectRepo.AddRange(_subjectDataSupplier.All);
        }

        private void RecreateRooms()
        {
            _roomRepo.AddRange(_roomsDataSupplier.All);
        }

        private async Task RecreateTeachersAndSaveAsync()
        {
            _teacherDataSupplier.InitializeData();

            var sampleTeacherEntityId = long.TryParse(_config["PreviewMode:Logins:Teacher:RelatedEntityId"], out var id)
                ? id
                : 0;
            if (!await _teacherRepo.ExistsAsync(x => x.Id == sampleTeacherEntityId).ConfigureAwait(false))
            {
                var teacher = _teacherDataSupplier.SampleTeacher;
                teacher.Id = sampleTeacherEntityId;

                _teacherRepo.Add(teacher);
                await _teacherRepo.SaveWithIdentityInsertAsync().ConfigureAwait(false);
            }

            _teacherRepo.AddRange(_teacherDataSupplier.AllExceptSample);
            await _teacherRepo.SaveAsync().ConfigureAwait(false);
        }

        private Task RecreateOrgClassesAndSaveAsync()
        {
            _orgClassDataSupplier.InitializeData();
            _orgClassRepo.AddRange(_orgClassDataSupplier.All);

            return _orgClassRepo.SaveAsync();
        }

        private async Task RecreateStudentsAndSaveAsync()
        {
            _studentDataSupplier.InitializeData();

            var sampleStudentEntityId = long.TryParse(_config["PreviewMode:Logins:Student:RelatedEntityId"], out var id)
                ? id
                : 0;
            if (!await _studentRegRecRepo.ExistsAsync(x => x.Id == sampleStudentEntityId).ConfigureAwait(false))
            {
                var studentInfo = _studentDataSupplier.SampleStudent;
                studentInfo.Record.Id = sampleStudentEntityId;

                _studentRegRecRepo.Add(studentInfo.Record);
                await _studentRegRecRepo.SaveWithIdentityInsertAsync().ConfigureAwait(false);

                studentInfo.Yearly.Info = studentInfo.Record;

                _studentRepo.Add(studentInfo.Yearly);
                await _studentRepo.SaveAsync().ConfigureAwait(false);
            }

            _studentRepo.AddRange(_studentDataSupplier.AllExceptSample.Select(x => x.Yearly));
            await _studentRepo.SaveAsync().ConfigureAwait(false);
        }


    }
}
