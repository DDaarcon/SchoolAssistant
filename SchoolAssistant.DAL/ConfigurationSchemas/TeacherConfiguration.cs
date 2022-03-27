using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolAssistant.DAL.Models.LinkingTables;
using SchoolAssistant.DAL.Models.Staff;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAssistant.DAL.ConfigurationSchemas
{
    internal class TeacherConfiguration : IEntityTypeConfiguration<Teacher>
    {
        public void Configure(EntityTypeBuilder<Teacher> builder)
        {

            //builder.Property<TeacherToMainSubject>("_mainSubjects")
            //    .HasColumnName("MainSubjects")
            //    .HasColumnType("bigint")
            //    .UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.HasMany<TeacherToMainSubject>("_mainSubjects")
                .WithOne(x => x.Teacher)
                .HasForeignKey(x => x.TeacherId)
                .OnDelete(DeleteBehavior.Cascade);
            //builder.OwnsMany<TeacherToMainSubject>("_mainSubjects", a =>
            //{
            //    a.WithOwner().HasForeignKey("TeacherId");
            //    a.Property(x => x.Id);
            //    a.HasKey(x => x.Id);
            //    a.HasOne(x => x.Subject)
            //        .WithMany()
            //        .IsRequired().OnDelete(DeleteBehavior.NoAction).HasForeignKey(x => x.SubjectId);
            //});
            //builder.Property<TeacherToAdditionalSubject>("_additionalSubjects");
        }
    }
}
