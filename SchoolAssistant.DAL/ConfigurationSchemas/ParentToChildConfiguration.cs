using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolAssistant.DAL.Models.LinkingTables;

namespace SchoolAssistant.DAL.ConfigurationSchemas
{
    internal class ParentToChildConfiguration : IEntityTypeConfiguration<ParentToChild>
    {
        public void Configure(EntityTypeBuilder<ParentToChild> builder)
        {
            builder.HasKey(x => new { x.ChildInfoId, x.IsSecondParent });
        }
    }
}
