using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolAssistant.DAL.Models.LinkingTables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAssistant.DAL.ConfigurationSchemas
{
    internal class TeacherToSubjectConfiguration : IEntityTypeConfiguration<TeacherToMainSubject>, IEntityTypeConfiguration<TeacherToAdditionalSubject>
    {
        public void Configure(EntityTypeBuilder<TeacherToAdditionalSubject> builder)
        {
            builder.HasNoKey();
        }

        public void Configure(EntityTypeBuilder<TeacherToMainSubject> builder)
        {
            builder.HasNoKey();
        }
    }
}
