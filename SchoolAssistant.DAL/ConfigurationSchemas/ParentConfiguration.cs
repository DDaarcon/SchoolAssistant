using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolAssistant.DAL.Models.StudentsParents;

namespace SchoolAssistant.DAL.ConfigurationSchemas
{
    internal class ParentConfiguration : SemesterDbEntityConfiguration<Parent>, IEntityTypeConfiguration<Parent>
    {
        public override void Configure(EntityTypeBuilder<Parent> builder)
        {
            base.Configure(builder);
        }
    }
}
