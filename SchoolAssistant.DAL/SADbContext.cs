using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace SchoolAssistant.DAL
{
    public class SADbContext : IdentityDbContext
    {
        public SADbContext(DbContextOptions<SADbContext> options)
            : base(options)
        {
        }
    }
}