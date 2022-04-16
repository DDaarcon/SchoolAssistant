using Microsoft.EntityFrameworkCore;
using SchoolAssistant.DAL.Models.Staff;
using SchoolAssistant.DAL.Repositories;
using SchoolAssistant.Infrastructure.Models.DataManagement.Staff;

namespace SchoolAssistant.Logic.DataManagement.Staff
{
    public interface ITeachersDataManagementService
    {
        Task<StaffListEntryJson[]> GetTeachersEntriesJsonAsync(IQueryable<Teacher>? query);
        Task<StaffListGroupJson> GetTeachersGroupJsonAsync(IQueryable<Teacher>? query);
    }

    [Injectable]
    public class TeachersDataManagementService : ITeachersDataManagementService
    {
        private readonly IRepository<Teacher> _repo;

        private readonly string TEACHERS_ID = "teachers";

        public TeachersDataManagementService(
            IRepository<Teacher> teacherRepo)
        {
            _repo = teacherRepo;
        }


        public async Task<StaffListGroupJson> GetTeachersGroupJsonAsync(IQueryable<Teacher>? query)
        {
            return new StaffListGroupJson
            {
                id = TEACHERS_ID,
                name = "Nauczyciele",
                entries = await GetTeachersEntriesJsonAsync(query)
            };
        }

        public Task<StaffListEntryJson[]> GetTeachersEntriesJsonAsync(IQueryable<Teacher>? query)
        {
            query ??= _repo.AsQueryable();
            return query.Select(x => new StaffListEntryJson
            {
                id = x.Id,
                name = x.GetFullName(),
                specialization = String.Join(", ", x.MainSubjects.Select(x => x.Name))
            }).ToArrayAsync();
        }

        public async Task<StaffPersonDetailsJson?> GetTeacherDetailsJsonAsync(long id)
        {
            var teacher = await _repo.GetByIdAsync(id);

            if (teacher is null) return null;

            return new StaffPersonDetailsJson
            {
                id = teacher.Id,
                firstName = teacher.FirstName,
                secondName = teacher.SecondName,
                lastName = teacher.LastName,
                mainSubjectsIds = teacher.MainSubjects.Select(x => x.Id).ToArray(),
                additionalSubjectsIds = teacher.AdditionalSubjects.Select(x => x.Id).ToArray()
            };
        }
    }
}
