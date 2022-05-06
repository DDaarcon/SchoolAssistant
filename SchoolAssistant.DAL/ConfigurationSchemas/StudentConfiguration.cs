using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolAssistant.DAL.Models.StudentsParents;

namespace SchoolAssistant.DAL.ConfigurationSchemas
{
    internal class StudentConfiguration : SchoolYearDbEntityConfiguration<Student>, IEntityTypeConfiguration<Student>
    {
        public override void Configure(EntityTypeBuilder<Student> builder)
        {
            base.Configure(builder);

            builder.HasOne(x => x.Info)
                .WithMany(x => x.StudentInstances);
        }
    }
}
