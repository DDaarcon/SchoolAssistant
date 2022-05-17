using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAssistant.Infrastructure.Models.Schedule
{
    public class ScheduleConfigJson
    {
        public string locale { get; set; } = null!;
        public DayOfWeek[] hiddenDays { get; set; } = null!;
        public string startTime { get; set; } = null!;
        public string endTime { get; set; } = null!;
    }
}
