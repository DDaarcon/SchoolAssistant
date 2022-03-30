using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolAssistant.DAL.Models.Attendance;

namespace SchoolAssistant.DAL.ConfigurationSchemas
{
    internal class PresenceConfiguration : SemesterDbEntityConfiguration<Presence>, IEntityTypeConfiguration<Presence>
    {
        public override void Configure(EntityTypeBuilder<Presence> builder)
        {
            base.Configure(builder);
        }
    }
}
