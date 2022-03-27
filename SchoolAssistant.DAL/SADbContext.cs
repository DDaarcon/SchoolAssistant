using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SchoolAssistant.DAL.ConfigurationSchemas;
using SchoolAssistant.DAL.Models.LinkingTables;
using SchoolAssistant.DAL.Models.Marks;
using SchoolAssistant.DAL.Models.Semesters;
using SchoolAssistant.DAL.Models.Staff;
using SchoolAssistant.DAL.Models.Students;

namespace SchoolAssistant.DAL
{
    public class SADbContext : IdentityDbContext
    {
        public SADbContext(DbContextOptions<SADbContext> options)
            : base(options)
        {
        }

        public DbSet<Semester> Semesters { get; set; } = null!;
        public DbSet<Teacher> Teachers { get; set; } = null!;
        public DbSet<Student> Students { get; set; } = null!;


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            new TeacherConfiguration().Configure(builder.Entity<Teacher>());

            var teacherToSubject = new TeacherToSubjectConfiguration();
            teacherToSubject.Configure(builder.Entity<TeacherToMainSubject>());
            teacherToSubject.Configure(builder.Entity<TeacherToAdditionalSubject>());

            new MarkConfiguration().Configure(builder.Entity<Mark>());
        }
    }
}