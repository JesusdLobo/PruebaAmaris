using Microsoft.EntityFrameworkCore;
using PruebaAmaris.Shared.Class;
using Microsoft.Extensions.Configuration;

namespace PruebaAmaris.Server.Class
{
    public class AppDbContext : DbContext
    {
        public DbSet<Order> Orders { get; set; }

        private readonly IConfiguration _configuration;

        public AppDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_configuration.GetConnectionString("AmarisConnection"));
        }
    }
}
