using CustomerOrders.DataAccess.Models;

using Microsoft.EntityFrameworkCore;

namespace CustomerOrders.DataAccess.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customers>();

            modelBuilder.Entity<Orders>()
                .HasMany(o => o.OrderDetails)
                .WithOne(od => od.Order)
                .HasForeignKey(od => od.OrderID);

            modelBuilder.Entity<OrderDetails>()
                .HasKey(od => new { od.OrderID, od.ProductId });

            modelBuilder.Entity<OrderDetails>()
                .HasOne(od => od.Products)
                .WithMany()
                .HasForeignKey(od => od.ProductId);
        }

        public DbSet<Customers> Customers { get; set; }
        public DbSet<Orders> Orders { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }
        public DbSet<Products> Products { get; set; }
    }
}
