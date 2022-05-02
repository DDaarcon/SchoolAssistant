using SchoolAssistant.Infrastructure.Models.ScheduleArranger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAssistant.Logic.ScheduleArranger
{
    public interface IFetchLessonsService
    {
        Task<ScheduleClassLessonsJson> ForClassAsync(long classId);
    }

    [Injectable]
    public class FetchLessonsService : IFetchLessonsService
    {



        public async Task<ScheduleClassLessonsJson> ForClassAsync(long classId)
        {
            return null;
        }
    }
}
