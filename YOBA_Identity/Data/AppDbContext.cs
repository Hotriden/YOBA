using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace YOBA_Identity.Data
{
    public class AppDbContext:IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> contextOptions)
            :base(contextOptions)
        {
            Database.EnsureCreated();
        }
    }
}
