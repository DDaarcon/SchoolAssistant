using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolAssistant.DAL.Models.Shared;

namespace SchoolAssistant.DAL.ConfigurationSchemas
{
    internal abstract class SemesterDbEntityConfiguration<TSemesterDbEntity> : IEntityTypeConfiguration<TSemesterDbEntity>
        where TSemesterDbEntity : SemesterDbEntity, new()
    {
        public virtual void Configure(EntityTypeBuilder<TSemesterDbEntity> builder)
        {
            builder.HasOne(x => x.Semester)
                .WithMany()
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
