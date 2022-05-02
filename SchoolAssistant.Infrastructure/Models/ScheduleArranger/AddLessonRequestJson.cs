using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAssistant.Infrastructure.Models.ScheduleArranger
{
    public class AddLessonRequestJson
    {
        public long classId { get; set; }
        public TimeJson time { get; set; } = null!;
        public int? customDuration { get; set; }
        public long subjectId { get; set; }
        public long lecturerId { get; set; }
        public long roomId { get; set; }
    }
}
