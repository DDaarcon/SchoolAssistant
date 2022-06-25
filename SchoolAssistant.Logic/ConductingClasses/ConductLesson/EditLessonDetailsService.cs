using SchoolAssistant.DAL.Models.Lessons;
using SchoolAssistant.DAL.Repositories;
using SchoolAssistant.Infrastructure.Models.ConductingClasses.ConductLesson;
using SchoolAssistant.Infrastructure.Models.Shared.Json;

namespace SchoolAssistant.Logic.ConductingClasses.ConductLesson
{
    public interface IEditLessonDetailsService
    {
        Task<ResponseJson> EditAsync(LessonDetailsEditJson model);
    }

    [Injectable]
    public class EditLessonDetailsService : IEditLessonDetailsService
    {
        private readonly IRepositoryBySchoolYear<Lesson> _lessonRepo;

        private LessonDetailsEditJson _model = null!;
        private Lesson _lesson = null!;
        private ResponseJson _response = null!;

        public EditLessonDetailsService(IRepositoryBySchoolYear<Lesson> lessonRepo)
        {
            _lessonRepo = lessonRepo;
        }

        public async Task<ResponseJson> EditAsync(LessonDetailsEditJson model)
        {
            _model = model;
            _response = new ResponseJson();

            if (!await FetchAndValidateAsync().ConfigureAwait(false))
                return _response;

            await ApplyModificationsAndSaveAsync().ConfigureAwait(false);

            return _response;
        }

        private async Task<bool> FetchAndValidateAsync()
        {
            if (String.IsNullOrEmpty(_model.topic))
            {
                _response.message = "Należy wprowadzić temat zajęć";
                return false;
            }

            _lesson = (await _lessonRepo.GetByIdAndCurrentYearAsync(_model.id).ConfigureAwait(false))!;
            if (_lesson is null)
            {
                _response.message = "Nie odnaleziono zajęć o padanym Id";
                return false;
            }

            return true;
        }

        private async Task ApplyModificationsAndSaveAsync()
        {
            _lesson.Topic = _model.topic;

            await _lessonRepo.SaveAsync();
        }
    }
}
