using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolAssistant.DAL.Models.StudentsOrganization;

namespace SchoolAssistant.DAL.ConfigurationSchemas
{
    internal class SubjectClassConfiguration : SchoolYearDbEntityConfiguration<SubjectClass>
    {
        public override void Configure(EntityTypeBuilder<SubjectClass> builder)
        {
            base.Configure(builder);
        }
    }
}
