using Microsoft.EntityFrameworkCore;
using SchoolAssistant.DAL.Models.StudentsParents;
using SchoolAssistant.DAL.Repositories;
using SchoolAssistant.Infrastructure.Models.DataManagement.Students;
using SchoolAssistant.Infrastructure.Models.Shared.Json;

namespace SchoolAssistant.Logic.DataManagement.Students
{
    public interface IStudentRegisterRecordsDataManagementService
    {
        Task<SaveResponseJson> CreateOrUpdateAsync(StudentRegisterRecordDetailsJson model);
        Task<StudentRegisterRecordListEntryJson[]> GetEntriesJsonAsync();
        Task<StudentRegisterRecordModificationDataJson?> GetModificationDataJsonAsync(long id);
    }

    [Injectable]
    public class StudentRegisterRecordsDataManagementService : IStudentRegisterRecordsDataManagementService
    {
        private readonly ISchoolYearRepository _schoolYearService;
        private readonly IModifyStudentRegisterRecordFromJsonService _modifyFromJsonService;
        private readonly IRepository<StudentRegisterRecord> _repo;

        public StudentRegisterRecordsDataManagementService(
            ISchoolYearRepository schoolYearService,
            IModifyStudentRegisterRecordFromJsonService modifyFromJsonService,
            IRepository<StudentRegisterRecord> repo)
        {
            _schoolYearService = schoolYearService;
            _modifyFromJsonService = modifyFromJsonService;
            _repo = repo;
        }


        public async Task<StudentRegisterRecordListEntryJson[]> GetEntriesJsonAsync()
        {
            var year = await _schoolYearService.GetOrCreateCurrentAsync();

            var query = _repo.AsQueryable()
                .Select(i => new
                {
                    record = i,
                    thisYearIns = i.StudentInstances.SingleOrDefault(s => s.SchoolYearId == year.Id)
                })
                .OrderByDescending(x => x.thisYearIns == null)
                .ThenBy(x => x.record.LastName);

            return await query.Select(x => new StudentRegisterRecordListEntryJson
            {
                id = x.record.Id,
                name = x.record.GetFullName(),
                className = x.thisYearIns == null ? null : x.thisYearIns.OrganizationalClass == null ? null
                    : x.thisYearIns.OrganizationalClass.Name
            }).ToArrayAsync();
        }

        public async Task<StudentRegisterRecordModificationDataJson?> GetModificationDataJsonAsync(long id)
        {
            var record = await _repo.GetByIdAsync(id);
            if (record is null)
                return null;

            return new StudentRegisterRecordModificationDataJson
            {
                data = new StudentRegisterRecordDetailsJson
                {
                    id = record.Id,
                    firstName = record.FirstName,
                    secondName = record.LastName,
                    lastName = record.LastName,
                    personalId = record.PersonalID,
                    address = record.Address,
                    dateOfBirth = record.DateOfBirth.ToString(),
                    placeOfBirth = record.PlaceOfBirth,
                    firstParent = CreateParentRegisterSubrecordDetailsJson(record.FirstParent),
                    secondParent = record.SecondParent is not null
                        ? CreateParentRegisterSubrecordDetailsJson(record.SecondParent)
                        : null
                }
            };
        }

        public Task<SaveResponseJson> CreateOrUpdateAsync(StudentRegisterRecordDetailsJson model)
        {
            return _modifyFromJsonService.CreateOrUpdateAsync(model);
        }

        private ParentRegisterSubrecordDetailsJson CreateParentRegisterSubrecordDetailsJson(ParentRegisterSubrecord entity)
            => new ParentRegisterSubrecordDetailsJson
            {
                firstName = entity.FirstName,
                secondName = entity.SecondName,
                lastName = entity.LastName,
                phoneNumber = entity.PhoneNumber,
                address = entity.Address,
                email = entity.Email
            };
    }
}
