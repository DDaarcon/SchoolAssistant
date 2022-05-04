using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAssistant.Infrastructure.Models.ScheduleArranger
{
    public class ScheduleClassSelectorEntryJson
    {
        public long id { get; set; }
        public string name { get; set; } = null!;
        public string? specialization { get; set; }
    }
}
