using SchoolAssistant.Infrastructure.Models.ConductingClasses.ConductLesson;
using SchoolAssistant.Infrastructure.Models.Shared.Json;

namespace SchoolAssistant.Logic.ConductingClasses.ConductLesson
{
    public interface IEditLessonDetailsService
    {
        Task<ResponseJson> Edit(LessonDetailsEditJson model);
    }

    [Injectable]
    public class EditLessonDetailsService : IEditLessonDetailsService
    {


        public async Task<ResponseJson> Edit(LessonDetailsEditJson model)
        {
            return null;
        }
    }
}
