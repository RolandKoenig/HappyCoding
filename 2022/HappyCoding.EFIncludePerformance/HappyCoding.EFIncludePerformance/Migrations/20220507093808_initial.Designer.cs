﻿// <auto-generated />
using System;
using HappyCoding.EFIncludePerformance;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace HappyCoding.EFIncludePerformance.Migrations
{
    [DbContext(typeof(TestingDBContext))]
    [Migration("20220507093808_initial")]
    partial class initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.11")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("HappyCoding.EFIncludePerformance.Model.Process", b =>
                {
                    b.Property<string>("ID")
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<DateTimeOffset>("CreateTimestamp")
                        .HasColumnType("datetimeoffset");

                    b.Property<int>("Field1")
                        .HasColumnType("int");

                    b.Property<int>("Field2")
                        .HasColumnType("int");

                    b.Property<int>("Field3")
                        .HasColumnType("int");

                    b.Property<string>("Field4")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("Field5")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<int>("Field6")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.ToTable("Process");
                });

            modelBuilder.Entity("HappyCoding.EFIncludePerformance.Model.ProcessActivity", b =>
                {
                    b.Property<long>("ProcessActivityID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTimeOffset>("ActivityTimetamp")
                        .HasColumnType("datetimeoffset");

                    b.Property<int>("Field1")
                        .HasColumnType("int");

                    b.Property<int>("Field2")
                        .HasColumnType("int");

                    b.Property<int>("Field3")
                        .HasColumnType("int");

                    b.Property<string>("Field4")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("Field5")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<int>("Field6")
                        .HasColumnType("int");

                    b.Property<byte[]>("Field7")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("ProcessID")
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("ProcessActivityID");

                    b.HasIndex("ProcessID");

                    b.ToTable("ProcessActivity");
                });

            modelBuilder.Entity("HappyCoding.EFIncludePerformance.Model.ProcessActivity", b =>
                {
                    b.HasOne("HappyCoding.EFIncludePerformance.Model.Process", null)
                        .WithMany("Activities")
                        .HasForeignKey("ProcessID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("HappyCoding.EFIncludePerformance.Model.Process", b =>
                {
                    b.Navigation("Activities");
                });
#pragma warning restore 612, 618
        }
    }
}