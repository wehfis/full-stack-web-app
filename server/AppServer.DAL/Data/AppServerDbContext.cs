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
            var connection_string = _configuration.GetConnectionString("connection_string");
            //var connection_string = _configuration.GetConnectionString("docker_connection_string");
            optionsBuilder.UseSqlServer(connection_string);
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
