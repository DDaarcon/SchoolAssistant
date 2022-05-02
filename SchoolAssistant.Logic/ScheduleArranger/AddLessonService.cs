using SchoolAssistant.Infrastructure.Models.ScheduleArranger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAssistant.Logic.ScheduleArranger
{
    public interface IAddLessonService
    {
        Task<AddLessonResponseJson> AddToClass(AddLessonRequestJson model);
    }

    [Injectable]
    public class AddLessonService : IAddLessonService
    {


        public async Task<AddLessonResponseJson> AddToClass(AddLessonRequestJson model)
        {
            return null;
        }
    }
}
