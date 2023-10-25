using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using AppServer.DAL.Models.Domains;

namespace AppServer.DAL.Data
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
                .HasMany(e => e.HeavyTasks)
                .WithOne()
                .HasForeignKey(e => e.OwnerId)
                .IsRequired();
        }
    }
}
