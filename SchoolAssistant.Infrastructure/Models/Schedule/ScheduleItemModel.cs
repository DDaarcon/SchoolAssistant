﻿namespace SchoolAssistant.Infrastructure.Models.Schedule
{
    public class ScheduleItemModel
    {
        public string Name { get; set; } = null!;
        public DateTime Start { get; set; }
        public DateTime End { get; set; }

    }
}
