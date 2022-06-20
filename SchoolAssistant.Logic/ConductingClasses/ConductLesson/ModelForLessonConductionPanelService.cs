using AppConfigurationEFCore;
using Microsoft.AspNetCore.Http;
using SchoolAssistant.DAL.Help.AppConfiguration;
using SchoolAssistant.DAL.Models.Lessons;
using SchoolAssistant.DAL.Repositories;
using SchoolAssistant.Infrastructure.Models.ConductingClasses.ConductLesson;
using SchoolAssistant.Logic.Help;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            return new LessonConductionPanelJson
            {
                lessonId = 1,
                className = "4e",
                subjectName = "Matematyka",
                startTimeTk = DateTime.Now.GetTicksJs(),
                duration = 45,
                students = new ParticipatingStudentJson[]
                {
                    new()
                    {
                        id = 1,
                        numberInJournal = 1,
                        firstName = "Jonasz",
                        lastName = "Kowalski"
                    },
                    new()
                    {
                        id = 2,
                        numberInJournal = 2,
                        firstName = "Jonasz",
                        lastName = "Kowalski"
                    },
                    new()
                    {
                        id = 3,
                        numberInJournal = 3,
                        firstName = "Jonasz",
                        lastName = "Kowalski"
                    },
                    new()
                    {
                        id = 4,
                        numberInJournal = 4,
                        firstName = "Jonasz",
                        lastName = "Kowalski"
                    },
                    new()
                    {
                        id = 5,
                        numberInJournal = 5,
                        firstName = "Jonasz",
                        lastName = "Kowalski"
                    },
                    new()
                    {
                        id = 6,
                        numberInJournal = 6,
                        firstName = "Jonasz",
                        lastName = "Kowalski"
                    },
                    new()
                    {
                        id = 7,
                        numberInJournal = 7,
                        firstName = "Jonasz",
                        lastName = "Kowalski"
                    }
                }
            };

            if (!await ValidateConductedLessonIdAsync())
                return null;

            if (!await ValidateWithDatabaseAndFetchAsync())
                return null;

            return await ConstructModelAsync();
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

            _conductedLesson = await _lessonRepo.GetByIdAndCurrentYearAsync(_conductedLessonId!.Value).ConfigureAwait(false);
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
                className = _conductedLesson.FromSchedule.ParticipatingOrganizationalClass?.Name,
                startTimeTk = _conductedLesson.ActualDate.GetTicksJs() ?? _conductedLesson.Date.GetTicksJs(),
                duration = _conductedLesson.FromSchedule.CustomDuration ?? await _configRepo.Records.DefaultLessonDuration.GetAsync() ?? 45,
                topic = _conductedLesson.Topic,
                students = null
            };
        }
    }
}
