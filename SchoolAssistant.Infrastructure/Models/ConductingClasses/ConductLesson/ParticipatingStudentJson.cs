using SchoolAssistant.Infrastructure.Enums.Attendance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAssistant.Infrastructure.Models.ConductingClasses.ConductLesson
{
    public class ParticipatingStudentJson
    {
        public long id { get; set; }
        public int numberInJournal { get; set; }
        public string firstName { get; set; } = null!;
        public string lastName { get; set; } = null!;
        public PresenceStatus? presence { get; set; }
    }
}
