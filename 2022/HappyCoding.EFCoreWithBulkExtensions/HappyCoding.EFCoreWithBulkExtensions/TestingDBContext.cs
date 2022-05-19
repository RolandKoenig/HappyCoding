using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HappyCoding.EFCoreWithBulkExtensions.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace HappyCoding.EFCoreWithBulkExtensions
{
    internal class TestingDBContext : DbContext
    {
        public DbSet<Order> Orders { get; set; } = null!;

        public DbSet<OrderPosition> OrderPositions { get; set; } = null!;

        public TestingDBContext(DbContextOptions options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var tableOrders = modelBuilder.Entity<Order>().ToTable("Orders");
            tableOrders.HasKey(x => x.ID);
            tableOrders
                .Property(x => x.ID)
                .ValueGeneratedNever();
            tableOrders.Property(x => x.CreateDate);

            var tableOrderPositions = modelBuilder.Entity<OrderPosition>().ToTable("OrderPositions");
            tableOrderPositions.HasKey(
                nameof(OrderPosition.OrderID),
                nameof(OrderPosition.PositionID));
            tableOrderPositions
                .Property(x => x.ArticleNumber)
                .HasMaxLength(13);
            tableOrderPositions
                .Property(x => x.Quantity);

            tableOrders
                .HasMany<OrderPosition>(x => x.Positions)
                .WithOne(x => x.Order)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
