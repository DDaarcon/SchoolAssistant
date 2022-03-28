using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolAssistant.DAL.Models.Marks;

namespace SchoolAssistant.DAL.ConfigurationSchemas
{
    internal class MarkConfiguration : SemesterDbEntityConfiguration<Mark>, IEntityTypeConfiguration<Mark>
    {
        public override void Configure(EntityTypeBuilder<Mark> builder)
        {
            base.Configure(builder);

            builder.HasOne(x => x.Student)
                .WithMany(x => x.Marks)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
