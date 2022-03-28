using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolAssistant.DAL.Models.LinkingTables;

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
