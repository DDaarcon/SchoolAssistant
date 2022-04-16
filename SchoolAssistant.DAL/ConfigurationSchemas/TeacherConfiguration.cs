using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolAssistant.DAL.Models.LinkingTables;
using SchoolAssistant.DAL.Models.Staff;

namespace SchoolAssistant.DAL.ConfigurationSchemas
{
    internal class TeacherConfiguration : IEntityTypeConfiguration<Teacher>
    {
        public void Configure(EntityTypeBuilder<Teacher> builder)
        {
            builder.HasMany(x => x.MainSubjects)
                .WithMany(x => x.MainTeachers)
                .UsingEntity<TeacherToMainSubject>(
                    j => j
                        .HasOne(x => x.Subject)
                        .WithMany("_mainTeachersLinking")
                        .HasForeignKey(x => x.SubjectId),
                    j => j
                        .HasOne(x => x.Teacher)
                        .WithMany("_mainSubjectsLinking")
                        .HasForeignKey(x => x.TeacherId),
                    j =>
                    {
                        j.HasKey(t => new { t.SubjectId, t.TeacherId });
                    });

            builder.HasMany(x => x.AdditionalSubjects)
                .WithMany(x => x.AdditionalTeachers)
                .UsingEntity<TeacherToAdditionalSubject>(
                    j => j
                        .HasOne(x => x.Subject)
                        .WithMany("_additionalTeachersLinking")
                        .HasForeignKey(x => x.SubjectId),
                    j => j
                        .HasOne(x => x.Teacher)
                        .WithMany("_additionalSubjectsLinking")
                        .HasForeignKey(x => x.TeacherId),
                    j =>
                    {
                        j.HasKey(t => new { t.SubjectId, t.TeacherId });
                    });
        }
    }
}
