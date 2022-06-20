using Microsoft.AspNetCore.Http;
using SchoolAssistant.DAL.Models.Lessons;
using SchoolAssistant.DAL.Repositories;
using SchoolAssistant.Infrastructure.Models.ConductingClasses.ConductLesson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAssistant.Logic.ConductingClasses.ConductLesson
{
    public interface IDisplayPanelForConductingLessonService
    {
        static string VIEW_DATA_KEY = "ConductedLessonModel";

        string ViewDataKey => VIEW_DATA_KEY;

        Task<bool> CheckSessionStateForLessonIdAsync();
        Task<LessonConductionPanelJson?> GetModelAsync();
    }

    [Injectable]
    public class DisplayPanelForConductingLessonService : IDisplayPanelForConductingLessonService
    {
        private readonly IConductLessonSessionService _sessionSvc;
        private readonly IUserRepository _userRepo;
        private readonly IRepositoryBySchoolYear<Lesson> _lessonRepo;

        public DisplayPanelForConductingLessonService(
            IConductLessonSessionService sessionSvc,
            IUserRepository userRepo,
            IRepositoryBySchoolYear<Lesson> lessonRepo)
        {
            _sessionSvc = sessionSvc;
            _userRepo = userRepo;
            _lessonRepo = lessonRepo;
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
                lessonId = 1
            };

            if (!await ValidateConductedLessonIdAsync())
                return null;

            if (!await ValidateWithDatabaseAndFetchAsync())
                return null;

            return ConstructModel();
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

        private LessonConductionPanelJson ConstructModel()
        {
            return new LessonConductionPanelJson
            {
                lessonId = _conductedLesson!.Id,
                subjectName = _conductedLesson.FromSchedule.Subject.Name,
                className = _conductedLesson.FromSchedule.ParticipatingOrganizationalClass?.Name
            };
        }
    }
}
