using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAssistant.Infrastructure.Models.Shared.Json
{
    public class ResponseJson
    {
        public bool success => message is null;
        public string? message { get; set; }
    }
}
