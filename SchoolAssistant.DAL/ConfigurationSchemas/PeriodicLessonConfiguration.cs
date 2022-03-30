using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolAssistant.DAL.Models.Lessons;

namespace SchoolAssistant.DAL.ConfigurationSchemas
{
    internal class PeriodicLessonConfiguration : SemesterDbEntityConfiguration<PeriodicLesson>, IEntityTypeConfiguration<PeriodicLesson>
    {
        public override void Configure(EntityTypeBuilder<PeriodicLesson> builder)
        {
            base.Configure(builder);
        }
    }
}
