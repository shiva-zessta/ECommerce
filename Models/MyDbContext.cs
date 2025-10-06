using ECommerce.Entities;
using Microsoft.EntityFrameworkCore;
namespace ECommerce.Models
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Category> Category { get; set; }

        public DbSet<Product> Product { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            optionsBuilder.UseNpgsql(config.GetConnectionString("DefaultConnection"));
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(t =>
            {
                t.HasKey(e => e.Id);
                t.Property(e => e.Id).ValueGeneratedOnAdd();
            }
            );

            modelBuilder.Entity<Category>(t =>
            {
                t.HasKey(e => e.Id);
                t.Property(e => e.Id).ValueGeneratedOnAdd();
            }
            );
            modelBuilder.Entity<Product>(t =>
            {
                t.HasKey(e => e.Id);
                t.Property(e => e.Id).ValueGeneratedOnAdd();
            }
            );
        }
    }
}

