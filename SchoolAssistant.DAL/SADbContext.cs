using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace SchoolAssistantWeb.Data
{
    public class SADbContext : IdentityDbContext
    {
        public SADbContext(DbContextOptions<SADbContext> options)
            : base(options)
        {
        }
    }
}