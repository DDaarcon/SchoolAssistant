using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolAssistant.DAL.ConfigurationSchemas.Help;
using SchoolAssistant.DAL.Models.StudentsParents;

namespace SchoolAssistant.DAL.ConfigurationSchemas
{
    internal class StudentRegisterRecordConfiguration : IEntityTypeConfiguration<StudentRegisterRecord>
    {
        public void Configure(EntityTypeBuilder<StudentRegisterRecord> builder)
        {
            builder.Property(x => x.DateOfBirth)
                .HasConversion<DateOnlyConverter>()
                .HasColumnType("date");
        }
    }
}
