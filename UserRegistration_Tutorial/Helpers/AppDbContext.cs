using Microsoft.EntityFrameworkCore;
using UserRegistration_Tutorial.Entities;

namespace UserRegistration_Tutorial.Helpers
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; }
    }
}
