using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HappyCoding.EFIncludePerformance.Model;
using Microsoft.EntityFrameworkCore;

namespace HappyCoding.EFIncludePerformance
{
    public class TestingDBContext : DbContext
    {
        public DbSet<Process> Processes { get; set; }

        public DbSet<ProcessActivity> ProcessActivities { get; set; }

        public TestingDBContext(DbContextOptions options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var processTable = modelBuilder.Entity<Process>().ToTable("Process");
            processTable.HasKey(x => x.ID);
            processTable
                .Property(x => x.ID)
                .HasMaxLength(30)
                .ValueGeneratedNever();
            processTable.Property(x => x.CreateTimestamp);
            processTable.Property(x => x.Field1);
            processTable.Property(x => x.Field2);
            processTable.Property(x => x.Field3);
            processTable
                .Property(x => x.Field4)
                .HasMaxLength(10);
            processTable
                .Property(x => x.Field5)
                .HasMaxLength(10);
            processTable.Property(x => x.Field6);

            var processActivityTable = modelBuilder.Entity<ProcessActivity>().ToTable("ProcessActivity");
            processActivityTable.HasKey(x => x.ProcessActivityID);
            processActivityTable
                .Property(x => x.ProcessActivityID)
                .ValueGeneratedOnAdd();
            processActivityTable
                .Property(x => x.ProcessID)
                .HasMaxLength(30);
            processActivityTable.Property(x => x.Field1);
            processActivityTable.Property(x => x.Field2);
            processActivityTable.Property(x => x.Field3);
            processActivityTable
                .Property(x => x.Field4)
                .HasMaxLength(10);
            processActivityTable
                .Property(x => x.Field5)
                .HasMaxLength(10);
            processActivityTable.Property(x => x.Field6);
            processActivityTable.Property(x => x.Field7);
            processActivityTable.HasIndex(x => x.ProcessID);

            processTable
                .HasMany<ProcessActivity>(x => x.Activities)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade)
                .HasForeignKey(x => x.ProcessID);
        }
    }
}
