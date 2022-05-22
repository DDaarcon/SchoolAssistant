using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolAssistant.DAL.Models.StudentsParents;

namespace SchoolAssistant.DAL.ConfigurationSchemas
{
    internal class ParentConfiguration : IEntityTypeConfiguration<Parent>
    {
        public void Configure(EntityTypeBuilder<Parent> builder)
        {

        }
    }
}
