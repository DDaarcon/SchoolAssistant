using AppConfigurationEFCore;
using Microsoft.EntityFrameworkCore;
using SchoolAssistant.DAL.Help.AppConfiguration;
using SchoolAssistant.DAL.Models.Lessons;
using SchoolAssistant.DAL.Repositories;
using SchoolAssistant.Infrastructure.Models.ConductingClasses.ConductLesson;
using SchoolAssistant.Logic.Help;

namespace SchoolAssistant.Logic.ConductingClasses.ConductLesson
{
    public interface IModelForLessonConductionPanelService
    {
        static string VIEW_DATA_KEY = "ConductedLessonModel";

        string ViewDataKey => VIEW_DATA_KEY;

        Task<bool> CheckSessionStateForLessonIdAsync();
        Task<LessonConductionPanelJson?> GetModelAsync();
    }

    [Injectable]
    public class ModelForLessonConductionPanelService : IModelForLessonConductionPanelService
    {
        private readonly IConductLessonSessionService _sessionSvc;
        private readonly IUserRepository _userRepo;
        private readonly IRepositoryBySchoolYear<Lesson> _lessonRepo;
        private readonly IAppConfiguration<AppConfigRecords> _configRepo;

        public ModelForLessonConductionPanelService(
            IConductLessonSessionService sessionSvc,
            IUserRepository userRepo,
            IRepositoryBySchoolYear<Lesson> lessonRepo,
            IAppConfiguration<AppConfigRecords> configRepo)
        {
            _sessionSvc = sessionSvc;
            _userRepo = userRepo;
            _lessonRepo = lessonRepo;
            _configRepo = configRepo;
        }

        private long? _conductedLessonId;

        public async Task<bool> CheckSessionStateForLessonIdAsync()
        {
            await TryGetConductedLessonIdFromSessionAsync().ConfigureAwait(false);

            return _conductedLessonId.HasValue;
        }




        private Lesson? _conductedLesson;

        public async Task<LessonConductionPanelJson?> GetModelAsync()
        {
            if (!await ValidateConductedLessonIdAsync().ConfigureAwait(false))
                return null;

            if (!await ValidateWithDatabaseAndFetchAsync().ConfigureAwait(false))
                return null;

            return await ConstructModelAsync().ConfigureAwait(false);
        }

        private async Task<bool> ValidateConductedLessonIdAsync()
        {
            await TryGetConductedLessonIdFromSessionAsync().ConfigureAwait(false);

            if (!_conductedLessonId.HasValue)
                return false;

            return true;
        }

        private async Task TryGetConductedLessonIdFromSessionAsync()
        {
            _conductedLessonId ??= await _sessionSvc.GetConductedLessonIdFromSessionAsync().ConfigureAwait(false);
        }

        private async Task<bool> ValidateWithDatabaseAndFetchAsync()
        {
            var user = await _userRepo.GetCurrentAsync().ConfigureAwait(false);
            if (user is null || user.Teacher is null)
                return false;

            _conductedLesson = await _lessonRepo.AsQueryableByYear.ByCurrent()
                .Include(x => x.PresenceOfStudents)
                .Include(x => x.SchoolYear)
                .Include(x => x.FromSchedule).ThenInclude(x => x.ParticipatingOrganizationalClass).ThenInclude(x => x.Students)
                .Include(x => x.FromSchedule).ThenInclude(x => x.Subject)
                .FirstOrDefaultAsync(x => x.Id == _conductedLessonId!.Value).ConfigureAwait(false);
            if (_conductedLesson is null)
                return false;

            if (_conductedLesson.FromSchedule.LecturerId != user.TeacherId)
                return false;

            return true;
        }

        private async Task<LessonConductionPanelJson> ConstructModelAsync()
        {
            return new LessonConductionPanelJson
            {
                lessonId = _conductedLesson!.Id,
                subjectName = _conductedLesson.FromSchedule.Subject.Name,
                className = _conductedLesson.FromSchedule.ParticipatingOrganizationalClass?.Name!,
                startTimeTk = _conductedLesson.ActualDate.GetTicksJs() ?? _conductedLesson.Date.GetTicksJs(),
                duration = _conductedLesson.FromSchedule.CustomDuration ?? await _configRepo.Records.DefaultLessonDuration.GetAsync() ?? 45,
                topic = _conductedLesson.Topic,
                students = GetStudentEntries()
            };
        }

        private ParticipatingStudentJson[] GetStudentEntries()
        {
            return _conductedLesson!.FromSchedule.ParticipatingOrganizationalClass!.Students
                .Select(x => new
                {
                    student = x,
                    presence = _conductedLesson.PresenceOfStudents.FirstOrDefault(y => y.StudentId == x.Id)
                })
                .Select(x => new ParticipatingStudentJson
                {
                    id = x.student.Id,
                    firstName = x.student.Info.FirstName,
                    lastName = x.student.Info.LastName,
                    numberInJournal = x.student.NumberInJournal,
                    presence = x.presence?.Status
                })
                .ToArray();
        }
    }
}
