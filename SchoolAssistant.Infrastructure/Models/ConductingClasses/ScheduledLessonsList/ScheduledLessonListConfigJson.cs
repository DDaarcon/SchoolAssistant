using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAssistant.Infrastructure.Models.ConductingClasses.ScheduledLessonsList
{
    public class ScheduledLessonListConfigJson
    {
        public int minutesBeforeLessonIsSoon { get; set; }
        public int entryHeight { get; set; }

        public string? tableClassName { get; set; }
        public string? theadClassName { get; set; }
        public string? theadTrClassName { get; set; }
        public string? tbodyClassName { get; set; }
        public string? tbodyTrClassName { get; set; }
    }
}
