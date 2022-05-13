using Microsoft.EntityFrameworkCore;
using SchoolAssistant.DAL.Models.Rooms;
using SchoolAssistant.DAL.Models.Staff;
using SchoolAssistant.DAL.Models.StudentsOrganization;
using SchoolAssistant.DAL.Models.Subjects;
using SchoolAssistant.DAL.Repositories;
using SchoolAssistant.Infrastructure.Models.ScheduleArranger.PageModelToReact;

namespace SchoolAssistant.Logic.ScheduleArranger
{
    public interface IFetchSchedArrDataService
    {
        Task<ScheduleClassSelectorEntryJson[]> FetchClassesForCurrentYearAsync();
        Task<ScheduleRoomEntryJson[]> FetchRoomsAsync();
        Task<ScheduleSubjectEntryJson[]> FetchSubjectsAsync();
        Task<ScheduleTeacherEntryJson[]> FetchTeachersAsync();
    }

    [Injectable]
    public class FetchSchedArrDataService : IFetchSchedArrDataService
    {
        private readonly IRepositoryBySchoolYear<OrganizationalClass> _orgClassRepo;
        private readonly IRepository<Subject> _subjectRepo;
        private readonly IRepository<Teacher> _teacherRepo;
        private readonly IRepository<Room> _roomRepo;

        public FetchSchedArrDataService(
            IRepositoryBySchoolYear<OrganizationalClass> orgClassRepo,
            IRepository<Subject> subjectRepo,
            IRepository<Teacher> teacherRepo,
            IRepository<Room> roomRepo)
        {
            _orgClassRepo = orgClassRepo;
            _subjectRepo = subjectRepo;
            _teacherRepo = teacherRepo;
            _roomRepo = roomRepo;
        }

        public Task<ScheduleClassSelectorEntryJson[]> FetchClassesForCurrentYearAsync()
        {
            return _orgClassRepo.AsQueryableByYear.ByCurrent()
                .Select(x => new ScheduleClassSelectorEntryJson
                {
                    id = x.Id,
                    name = x.Name,
                    specialization = x.Specialization
                })
                .ToArrayAsync();
        }

        public Task<ScheduleSubjectEntryJson[]> FetchSubjectsAsync()
        {
            return _subjectRepo.AsQueryable()
                .Select(x => new ScheduleSubjectEntryJson
                {
                    id = x.Id,
                    name = x.Name
                })
                .ToArrayAsync();
        }

        public Task<ScheduleTeacherEntryJson[]> FetchTeachersAsync()
        {
            return _teacherRepo.AsQueryable()
                .Select(x => new ScheduleTeacherEntryJson
                {
                    id = x.Id,
                    name = x.GetFullName(),
                    shortName = x.GetShortenedName(),
                    mainSubjectIds = x.MainSubjects.Select(x => x.SubjectId).ToArray(),
                    additionalSubjectIds = x.AdditionalSubjects.Select(x => x.SubjectId).ToArray()
                })
                .ToArrayAsync();
        }

        public Task<ScheduleRoomEntryJson[]> FetchRoomsAsync()
        {
            return _roomRepo.AsQueryable()
                .Select(x => new ScheduleRoomEntryJson
                {
                    id = x.Id,
                    name = x.DisplayName,
                    floor = x.Floor
                })
                .ToArrayAsync();
        }
    }
}
