using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAssistant.Infrastructure.Models.ScheduleArranger
{
    public class LessonJson : ScheduleLessonTimetableEntryJson
    {
        public IdNameJson? orgClass { get; set; }
        public IdNameJson? subjClass { get; set; }
    }
}
