using Microsoft.AspNetCore.Identity;
using SchoolAssistant.Infrastructure.Interfaces.Permissions;

namespace SchoolAssistant.DAL.Models.AppStructure
{
    public class Role : IdentityRole<long>, IPermissions
    {
        public bool CanAccessConfiguration { get; set; }
        public bool CanViewAllClassesData { get; set; }
    }
}
