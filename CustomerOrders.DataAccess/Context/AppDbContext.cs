using CustomerOrders.DataAccess.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders; // Ensure this namespace is included

namespace CustomerOrders.DataAccess.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customers>()
                .HasMany(c => c.Orders)
                .WithOne(o => o.Customer)
                .HasForeignKey(o => o.CustomerID);

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
