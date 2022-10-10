using Microsoft.EntityFrameworkCore;
using UserRegistration_Tutorial.Models.Users;

namespace UserRegistration_Tutorial.Helpers;

public class AppDbContext : DbContext
{
    private AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
}