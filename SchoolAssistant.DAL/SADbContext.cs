using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SchoolAssistant.DAL.Models.Application;
using SchoolAssistant.DAL.Models.AppStructure;
using SchoolAssistant.DAL.Models.Attendance;
using SchoolAssistant.DAL.Models.SchoolYears;
using SchoolAssistant.DAL.Models.Staff;
using SchoolAssistant.DAL.Models.StudentsParents;

namespace SchoolAssistant.DAL
{
    public class SADbContext : IdentityDbContext<User, Role, long>
    {
        public SADbContext(DbContextOptions<SADbContext> options)
            : base(options)
        {
        }

        protected DbSet<AppConfig> _Config { get; set; } = null!;
        public DbSet<SchoolYear> Semesters { get; set; } = null!;
        public DbSet<Teacher> Teachers { get; set; } = null!;
        public DbSet<Student> Students { get; set; } = null!;
        public DbSet<Parent> Parents { get; set; } = null!;
        public DbSet<Presence> Attendance { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(typeof(SADbContext).Assembly);
        }
    }
}