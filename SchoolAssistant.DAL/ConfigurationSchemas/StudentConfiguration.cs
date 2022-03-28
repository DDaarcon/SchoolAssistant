using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolAssistant.DAL.Models.Students;

namespace SchoolAssistant.DAL.ConfigurationSchemas
{
    internal class StudentConfiguration : SemesterDbEntityConfiguration<Student>, IEntityTypeConfiguration<Student>
    {
        public override void Configure(EntityTypeBuilder<Student> builder)
        {
            base.Configure(builder);
        }
    }
}
