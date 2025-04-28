using Microsoft.EntityFrameworkCore;
using Northwind.OrderManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.OrderManagement.Infrastructure.Persistence
{
    public class NorthwindDbContext : DbContext
    {
        public NorthwindDbContext(DbContextOptions<NorthwindDbContext> options) : base(options)
        {
        }

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Shipper> Shippers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configurar Order
            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(e => e.OrderId);

                entity.HasOne(e => e.Customer)
                    .WithMany(c => c.Orders)
                    .HasForeignKey(e => e.CustomerId)
                    .OnDelete(DeleteBehavior.SetNull); // Esto manejaría relaciones nulas si es necesario

                entity.HasOne(e => e.Employee)
                    .WithMany(emp => emp.Orders)
                    .HasForeignKey(e => e.EmployeeId);

                entity.Property(e => e.ShipAddress).HasMaxLength(60);
                entity.Property(e => e.ShipCity).HasMaxLength(15);
                entity.Property(e => e.ShipRegion).HasMaxLength(15);
                entity.Property(e => e.ShipPostalCode).HasMaxLength(10);
                entity.Property(e => e.ShipCountry).HasMaxLength(15);
            });

            // Configurar OrderDetail (clave compuesta), con eliminacion en cascada
            modelBuilder.Entity<OrderDetail>(entity =>
            {
                entity.HasKey(od => new { od.OrderId, od.ProductId });

                entity.HasOne(od => od.Order)
                    .WithMany(o => o.OrderDetails)
                    .HasForeignKey(od => od.OrderId);

                entity.HasOne(od => od.Product)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(od => od.ProductId);
                
                // Especificar el nombre de la tabla con espacios
                entity.ToTable("Order Details");
            });

            // Configurar Customer
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(c => c.CustomerId);
                entity.Property(c => c.CustomerId).HasMaxLength(5);
            });

            // Configurar Employee
            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasKey(e => e.EmployeeId);
            });

            // Configurar Product
            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(p => p.ProductId);
            });

            // Configurar Shipper
            modelBuilder.Entity<Shipper>(entity =>
            {
                entity.HasKey(s => s.ShipperId);
            });
        }

    }
}
