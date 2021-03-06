using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolAssistant.DAL.Models.Staff;

namespace SchoolAssistant.DAL.ConfigurationSchemas
{
    internal class TeacherConfiguration : IEntityTypeConfiguration<Teacher>
    {
        public void Configure(EntityTypeBuilder<Teacher> builder)
        {
            builder.HasMany(x => x.MainSubjects)
                .WithOne(x => x.Teacher);
            builder.HasMany(x => x.AdditionalSubjects)
                .WithOne(x => x.Teacher);
        }
    }
}
