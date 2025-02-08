using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EcommerceProject.Models
{
    public class EcommerceDbContext:IdentityDbContext<ApplicationUser> 
    {
        public DbSet<Product> products { get; set; }
        public DbSet<Category> categories { get; set; }

        public EcommerceDbContext(DbContextOptions<EcommerceDbContext> options):
            base(options) 
        {
            
        }

    }
}
