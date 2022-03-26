using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace SchoolAssistant.DAL.Models.Shared
{
    public abstract class DbEntity
    {
        public long Id { get; set; }

    }
}
