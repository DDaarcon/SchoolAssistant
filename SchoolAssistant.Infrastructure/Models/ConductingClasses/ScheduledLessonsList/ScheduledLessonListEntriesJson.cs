using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAssistant.Infrastructure.Models.ConductingClasses.ScheduledLessonsList
{
    public class ScheduledLessonListEntriesJson
    {
        public ScheduledLessonListEntryJson[] entries { get; set; } = null!;
        public long? incomingAtTk { get; set; }
    }
}
