using Microsoft.EntityFrameworkCore;
using AppServer.Models.Domains;

namespace AppServer.Data
{
    public class HeavyTaskDbContext : DbContext
    {
        private readonly IConfiguration _configuration;
        public HeavyTaskDbContext(DbContextOptions<HeavyTaskDbContext> options, IConfiguration configuration) : base(options) 
        {
            _configuration = configuration;
        }
        public DbSet<HeavyTask> HeavyTasks { get; set;}
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(_configuration.GetConnectionString("connection_string"));
        }
    }
}
