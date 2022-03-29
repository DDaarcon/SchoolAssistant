using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolAssistant.DAL.Models.StudentsOrganization;

namespace SchoolAssistant.DAL.ConfigurationSchemas
{
    internal class SubjectClassConfiguration : SemesterDbEntityConfiguration<SubjectClass>
    {
        public override void Configure(EntityTypeBuilder<SubjectClass> builder)
        {
            base.Configure(builder);
        }
    }
}
