using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAssistant.Infrastructure.Models.ConductingClasses.ConductLesson
{
    public class LessonConductionPanelJson
    {
        public long lessonId { get; set; }
        public string subjectName { get; set; } = null!;
        public string className { get; set; } = null!;
        public long startTimeTk { get; set; }
        public int duration { get; set; }
        public string? topic { get; set; }
    }
}
