using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAssistant.Infrastructure.Models.Schedule
{
    public class ScheduleEventJson
    {
        public string id { get; set; } = null!;
        public string title { get; set; } = null!;
        public long startTime { get; set; }
        public long endTime { get; set; }
        public string @class { get; set; } = null!;
        public string lecturer { get; set; } = null!;
        public string room { get; set; } = null!;
        public string subject { get; set; } = null!;
    }
}
