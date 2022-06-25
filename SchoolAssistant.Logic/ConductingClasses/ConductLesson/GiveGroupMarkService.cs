using SchoolAssistant.Infrastructure.Models.ConductingClasses.ConductLesson;
using SchoolAssistant.Infrastructure.Models.Shared.Json;

namespace SchoolAssistant.Logic.ConductingClasses.ConductLesson
{
    public interface IGiveGroupMarkService
    {
        Task<ResponseJson> GiveAsync(GiveGroupMarkJson model);
    }

    [Injectable]
    public class GiveGroupMarkService : IGiveGroupMarkService
    {

        private GiveGroupMarkJson _model = null!;
        private ResponseJson _response = null!;

        public async Task<ResponseJson> GiveAsync(GiveGroupMarkJson model)
        {
            _model = model;
            _response = new ResponseJson();


        }
    }
}
