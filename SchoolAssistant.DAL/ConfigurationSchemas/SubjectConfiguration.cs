using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolAssistant.DAL.Models.Subjects;

namespace SchoolAssistant.DAL.ConfigurationSchemas
{
    internal class SubjectConfiguration : IEntityTypeConfiguration<Subject>
    {
        public void Configure(EntityTypeBuilder<Subject> builder)
        {
            builder.HasMany(x => x.MainTeachers)
                .WithOne(x => x.Subject);
            builder.HasMany(x => x.AdditionalTeachers)
                .WithOne(x => x.Subject);
        }
    }
}
