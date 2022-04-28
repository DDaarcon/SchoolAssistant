using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SchoolAssistant.DAL.ConfigurationSchemas;
using SchoolAssistant.DAL.Models.AppStructure;
using SchoolAssistant.DAL.Models.Attendance;
using SchoolAssistant.DAL.Models.Lessons;
using SchoolAssistant.DAL.Models.LinkingTables;
using SchoolAssistant.DAL.Models.Marks;
using SchoolAssistant.DAL.Models.SchoolYears;
using SchoolAssistant.DAL.Models.Staff;
using SchoolAssistant.DAL.Models.StudentsOrganization;
using SchoolAssistant.DAL.Models.StudentsParents;
using SchoolAssistant.DAL.Models.Subjects;

namespace SchoolAssistant.DAL
{
    public class SADbContext : IdentityDbContext<User, Role, long>
    {
        public SADbContext(DbContextOptions<SADbContext> options)
            : base(options)
        {
        }

        public DbSet<SchoolYear> Semesters { get; set; } = null!;
        public DbSet<Teacher> Teachers { get; set; } = null!;
        public DbSet<Student> Students { get; set; } = null!;
        public DbSet<Parent> Parents { get; set; } = null!;
        public DbSet<Presence> Attendance { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // TODO: Use Attribute to register and call configurations
            new TeacherConfiguration().Configure(builder.Entity<Teacher>());
            var teacherToSubjectConfiguration = new TeacherToSubjectConfiguration();
            teacherToSubjectConfiguration.Configure(builder.Entity<TeacherToMainSubject>());
            teacherToSubjectConfiguration.Configure(builder.Entity<TeacherToAdditionalSubject>());
            new SubjectConfiguration().Configure(builder.Entity<Subject>());


            new MarkConfiguration().Configure(builder.Entity<Mark>());
            new MarksOfClassConfiguration().Configure(builder.Entity<MarksOfClass>());

            new OrganizationalClassConfiguration().Configure(builder.Entity<OrganizationalClass>());
            new SubjectClassConfiguration().Configure(builder.Entity<SubjectClass>());

            new PresenceConfiguration().Configure(builder.Entity<Presence>());

            new PeriodicLessonConfiguration().Configure(builder.Entity<PeriodicLesson>());
            new LessonConfiguration().Configure(builder.Entity<Lesson>());

            new StudentConfiguration().Configure(builder.Entity<Student>());
            new ParentConfiguration().Configure(builder.Entity<Parent>());

            new StudentRegisterRecordConfiguration().Configure(builder.Entity<StudentRegisterRecord>());
        }
    }
}