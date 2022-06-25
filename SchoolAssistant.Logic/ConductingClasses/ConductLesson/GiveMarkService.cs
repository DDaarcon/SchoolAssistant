using SchoolAssistant.Infrastructure.Models.ConductingClasses.ConductLesson;
using SchoolAssistant.Infrastructure.Models.Shared.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAssistant.Logic.ConductingClasses.ConductLesson
{
    public interface IGiveMarkService
    {
        Task<ResponseJson> GiveAsync(GiveMarkJson model);
    }

    [Injectable]
    public class GiveMarkService : IGiveMarkService
    {
        
        private GiveMarkJson _model = null!;
        private ResponseJson _response = null!;

        public async Task<ResponseJson> GiveAsync(GiveMarkJson model)
        {
            _model = model;
            _response = new ResponseJson();
            

            return _response;
        }
    }
}
