using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SchoolAssistant.DAL.ConfigurationSchemas;
using SchoolAssistant.DAL.Models.AppStructure;
using SchoolAssistant.DAL.Models.Attendance;
using SchoolAssistant.DAL.Models.Lessons;
using SchoolAssistant.DAL.Models.LinkingTables;
using SchoolAssistant.DAL.Models.Marks;
using SchoolAssistant.DAL.Models.Semesters;
using SchoolAssistant.DAL.Models.Staff;
using SchoolAssistant.DAL.Models.Students;
using SchoolAssistant.DAL.Models.StudentsOrganization;

namespace SchoolAssistant.DAL
{
    public class SADbContext : IdentityDbContext<User, Role, long>
    {
        public SADbContext(DbContextOptions<SADbContext> options)
            : base(options)
        {
        }

        public DbSet<Semester> Semesters { get; set; } = null!;
        public DbSet<Teacher> Teachers { get; set; } = null!;
        public DbSet<Student> Students { get; set; } = null!;
        public DbSet<Presence> Attendance { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            new TeacherConfiguration().Configure(builder.Entity<Teacher>());

            var teacherToSubject = new TeacherToSubjectConfiguration();
            teacherToSubject.Configure(builder.Entity<TeacherToMainSubject>());
            teacherToSubject.Configure(builder.Entity<TeacherToAdditionalSubject>());

            new MarkConfiguration().Configure(builder.Entity<Mark>());
            new MarksOfClassConfiguration().Configure(builder.Entity<MarksOfClass>());

            new OrganizationalClassConfiguration().Configure(builder.Entity<OrganizationalClass>());
            new SubjectClassConfiguration().Configure(builder.Entity<SubjectClass>());
            new StudentConfiguration().Configure(builder.Entity<Student>());

            new PresenceConfiguration().Configure(builder.Entity<Presence>());

            new PeriodicLessonConfiguration().Configure(builder.Entity<PeriodicLesson>());
            new LessonConfiguration().Configure(builder.Entity<Lesson>());

        }
    }
}