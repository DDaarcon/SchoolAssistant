using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolAssistant.DAL.Models.LinkingTables;

namespace SchoolAssistant.DAL.ConfigurationSchemas
{
    internal class TeacherToSubjectConfiguration : IEntityTypeConfiguration<TeacherToMainSubject>, IEntityTypeConfiguration<TeacherToAdditionalSubject>
    {
        public void Configure(EntityTypeBuilder<TeacherToMainSubject> builder)
        {
            builder.HasKey(x => new { x.SubjectId, x.TeacherId });
        }

        public void Configure(EntityTypeBuilder<TeacherToAdditionalSubject> builder)
        {
            builder.HasKey(x => new { x.SubjectId, x.TeacherId });
        }
    }
}
