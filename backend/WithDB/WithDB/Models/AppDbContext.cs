using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace WithDB.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options) {}
        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
