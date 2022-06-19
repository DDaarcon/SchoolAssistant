using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAssistant.Infrastructure.Models.ConductingClasses.ConductLesson
{
    public class PanelForConductingLessonJson
    {
        public long lessonId { get; set; }
        public string subjectName { get; set; } = null!;
        public string className { get; set; } = null!;

    }
}
