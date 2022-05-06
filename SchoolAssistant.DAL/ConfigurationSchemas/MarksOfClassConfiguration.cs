using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolAssistant.DAL.Models.Marks;

namespace SchoolAssistant.DAL.ConfigurationSchemas
{
    internal class MarksOfClassConfiguration : SchoolYearDbEntityConfiguration<MarksOfClass>, IEntityTypeConfiguration<MarksOfClass>
    {
        public override void Configure(EntityTypeBuilder<MarksOfClass> builder)
        {
            base.Configure(builder);
        }
    }
}
