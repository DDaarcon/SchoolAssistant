using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolAssistant.DAL.Models.Shared;

namespace SchoolAssistant.DAL.ConfigurationSchemas
{
    internal abstract class SchoolYearDbEntityConfiguration<TSchoolYearDbEntity> : IEntityTypeConfiguration<TSchoolYearDbEntity>
        where TSchoolYearDbEntity : SchoolYearDbEntity, new()
    {
        public virtual void Configure(EntityTypeBuilder<TSchoolYearDbEntity> builder)
        {
            builder.HasOne(x => x.SchoolYear)
                .WithMany()
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
