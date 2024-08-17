﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using XuongMay_BE.Data;

#nullable disable

namespace XuongMay_BE.Migrations
{
    [DbContext(typeof(MyDbContext))]
    [Migration("20240817062253_AddCustomerTable")]
    partial class AddCustomerTable
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("XuongMay_BE.Data.Category", b =>
                {
                    b.Property<Guid>("CategoryID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("CategoryID1")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CategoryID");

                    b.HasIndex("CategoryID1");

                    b.ToTable("Category");
                });

            modelBuilder.Entity("XuongMay_BE.Data.Customer", b =>
                {
                    b.Property<int>("CustomerID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CustomerID"));

                    b.Property<string>("Address")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("CustomerName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Phone")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("CustomerID");

                    b.ToTable("Customer");
                });

            modelBuilder.Entity("XuongMay_BE.Data.Product", b =>
                {
                    b.Property<Guid>("ProductID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CategoryID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ProductID");

                    b.HasIndex("CategoryID");

                    b.ToTable("Product");
                });

            modelBuilder.Entity("XuongMay_BE.Data.ProductionLine", b =>
                {
                    b.Property<int>("LineID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("LineID"));

                    b.Property<string>("LineName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int?>("SupervisorID")
                        .HasColumnType("int");

                    b.HasKey("LineID");

                    b.HasIndex("SupervisorID")
                        .IsUnique()
                        .HasFilter("[SupervisorID] IS NOT NULL");

                    b.ToTable("ProductionLine");
                });

            modelBuilder.Entity("XuongMay_BE.Data.Supervisor", b =>
                {
                    b.Property<int>("SupervisorID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SupervisorID"));

                    b.Property<int?>("LineID")
                        .HasColumnType("int");

                    b.Property<string>("SupervisorName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("SupervisorID");

                    b.ToTable("Supervisor");
                });

            modelBuilder.Entity("XuongMay_BE.Data.Category", b =>
                {
                    b.HasOne("XuongMay_BE.Data.Category", null)
                        .WithMany("Categories")
                        .HasForeignKey("CategoryID1");
                });

            modelBuilder.Entity("XuongMay_BE.Data.Product", b =>
                {
                    b.HasOne("XuongMay_BE.Data.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("XuongMay_BE.Data.ProductionLine", b =>
                {
                    b.HasOne("XuongMay_BE.Data.Supervisor", "Supervisor")
                        .WithOne("ProductionLine")
                        .HasForeignKey("XuongMay_BE.Data.ProductionLine", "SupervisorID");

                    b.Navigation("Supervisor");
                });

            modelBuilder.Entity("XuongMay_BE.Data.Category", b =>
                {
                    b.Navigation("Categories");
                });

            modelBuilder.Entity("XuongMay_BE.Data.Supervisor", b =>
                {
                    b.Navigation("ProductionLine");
                });
#pragma warning restore 612, 618
        }
    }
}
