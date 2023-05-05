using Microsoft.EntityFrameworkCore;

namespace MyFirstProject.Models
{
    public class AppDbContext : DbContext
    {
         
        public AppDbContext(DbContextOptions options):base(options)
        {

        }
     
        public DbSet<user> users { get; set; }

    }
}

