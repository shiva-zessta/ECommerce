using ECommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
namespace ECommerce.Infrastructure.Persistence
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Category> Category { get; set; }

        public DbSet<Product> Product { get; set; }
        public DbSet<Address> Address { get; set; }

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
            modelBuilder.Entity<Address>(t =>
            {
                t.HasKey(e => e.Id);
                t.Property(e => e.Id).ValueGeneratedOnAdd();
                t.HasOne(a => a.User)
                .WithMany(a => a.Addresses)
                .HasForeignKey(a => a.UserId);
            }
            );

            modelBuilder.Entity<Category>(t =>
            {
                t.HasKey(e => e.Id);
                t.Property(e => e.Id).ValueGeneratedOnAdd();
                t.HasMany(c => c.Products)
                .WithOne(p => p.Category)
                .HasForeignKey(p => p.CategoryId);
            }
            );
            modelBuilder.Entity<Product>(t =>
            {
                
                t.HasKey(e => e.Id);
                t.Property(e => e.Id).ValueGeneratedOnAdd();
                t.HasOne(p => p.Category)
                    .WithMany(c => c.Products)
                    .HasForeignKey(p => p.CategoryId);
            }
            );

        }
    }
}

