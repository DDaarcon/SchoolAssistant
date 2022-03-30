using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolAssistant.DAL.Models.Lessons;

namespace SchoolAssistant.DAL.ConfigurationSchemas
{
    internal class LessonConfiguration : SemesterDbEntityConfiguration<Lesson>, IEntityTypeConfiguration<Lesson>
    {
        public override void Configure(EntityTypeBuilder<Lesson> builder)
        {
            base.Configure(builder);
        }
    }
}