using SchoolAssistant.Infrastructure.Models.ScheduleShared;
using SchoolAssistant.Infrastructure.Models.Shared.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAssistant.Infrastructure.Models.ScheduleArranger
{
    public class AddLessonResponseJson : ResponseJson
    {
        public LessonTimetableEntryJson? lesson { get; set; }
    }
}
