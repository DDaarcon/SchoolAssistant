using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolAssistant.DAL.Models.StudentsOrganization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAssistant.DAL.ConfigurationSchemas
{
    internal class SubjectClassConfiguration : SemesterDbEntityConfiguration<SubjectClass>
    {
        public override void Configure(EntityTypeBuilder<SubjectClass> builder)
        {
            base.Configure(builder);
        }
    }
}
