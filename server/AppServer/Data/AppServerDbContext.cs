using Microsoft.EntityFrameworkCore;
using AppServer.Models.Domains;

namespace AppServer.Data
{
    public class AppServerDbContext : DbContext
    {
        private readonly IConfiguration _configuration;
        public AppServerDbContext(DbContextOptions<AppServerDbContext> options, IConfiguration configuration) : base(options) 
        {
            _configuration = configuration;
        }
        public DbSet<HeavyTask> HeavyTasks { get; set;}
        public DbSet<User> Users { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(_configuration.GetConnectionString("connection_string"));
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(p => p.HeavyTasks)
                .WithOne(c => c.Owner)
                .HasForeignKey(c => c.OwnerId);
        }
    }
}
