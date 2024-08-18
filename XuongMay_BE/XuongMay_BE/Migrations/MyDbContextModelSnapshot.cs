﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using XuongMay_BE.Data;

#nullable disable

namespace XuongMay_BE.Migrations
{
    [DbContext(typeof(MyDbContext))]
    partial class MyDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
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

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CategoryID");

                    b.ToTable("Category");
                });

            modelBuilder.Entity("XuongMay_BE.Data.Customer", b =>
                {
                    b.Property<Guid>("CustomerID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

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

            modelBuilder.Entity("XuongMay_BE.Data.Employee", b =>
                {
                    b.Property<Guid>("EmpID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("EmpName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("LineID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("EmpID");

                    b.HasIndex("LineID");

                    b.ToTable("Employee");
                });

            modelBuilder.Entity("XuongMay_BE.Data.OrderDetail", b =>
                {
                    b.Property<Guid>("OrderID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ProductID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("SupervisorID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<double>("TotalPrice")
                        .HasColumnType("float");

                    b.HasKey("OrderID", "ProductID", "SupervisorID");

                    b.HasIndex("ProductID");

                    b.HasIndex("SupervisorID");

                    b.ToTable("OrderDetail", (string)null);
                });

            modelBuilder.Entity("XuongMay_BE.Data.Orders", b =>
                {
                    b.Property<Guid>("OrderID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CustomerID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DeliveryDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("TotalQuantity")
                        .HasColumnType("int");

                    b.HasKey("OrderID");

                    b.HasIndex("CustomerID");

                    b.ToTable("Orders");
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
                    b.Property<Guid>("LineID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("LineName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<Guid?>("SupervisorID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("LineID");

                    b.HasIndex("SupervisorID");

                    b.ToTable("ProductionLine");
                });

            modelBuilder.Entity("XuongMay_BE.Data.Stage", b =>
                {
                    b.Property<Guid>("StageID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Sequence")
                        .HasColumnType("int");

                    b.Property<string>("StageName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("StageID");

                    b.ToTable("Stage");
                });

            modelBuilder.Entity("XuongMay_BE.Data.Supervisor", b =>
                {
                    b.Property<Guid>("SupervisorID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int?>("LineID")
                        .HasColumnType("int");

                    b.Property<string>("SupervisorName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("SupervisorID");

                    b.ToTable("Supervisor");
                });

            modelBuilder.Entity("XuongMay_BE.Data.Task", b =>
                {
                    b.Property<Guid>("TaskID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AssignedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AssignedTo")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("EmpID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("EndTime")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("OrderID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Remarks")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("StageID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("SupervisorID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("TaskID");

                    b.HasIndex("EmpID");

                    b.HasIndex("OrderID");

                    b.HasIndex("StageID");

                    b.HasIndex("SupervisorID");

                    b.ToTable("Task");
                });

            modelBuilder.Entity("XuongMay_BE.Data.Employee", b =>
                {
                    b.HasOne("XuongMay_BE.Data.ProductionLine", "ProductionLines")
                        .WithMany()
                        .HasForeignKey("LineID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ProductionLines");
                });

            modelBuilder.Entity("XuongMay_BE.Data.OrderDetail", b =>
                {
                    b.HasOne("XuongMay_BE.Data.Orders", "Orders")
                        .WithMany("OrderDetails")
                        .HasForeignKey("OrderID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_OrderDetail_Order");

                    b.HasOne("XuongMay_BE.Data.Product", "Product")
                        .WithMany("OrderDetails")
                        .HasForeignKey("ProductID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_OrderDetail_Product");

                    b.HasOne("XuongMay_BE.Data.Supervisor", "Supervisor")
                        .WithMany("OrderDetails")
                        .HasForeignKey("SupervisorID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_OrderDetail_Supervisor");

                    b.Navigation("Orders");

                    b.Navigation("Product");

                    b.Navigation("Supervisor");
                });

            modelBuilder.Entity("XuongMay_BE.Data.Orders", b =>
                {
                    b.HasOne("XuongMay_BE.Data.Customer", "Customers")
                        .WithMany()
                        .HasForeignKey("CustomerID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customers");
                });

            modelBuilder.Entity("XuongMay_BE.Data.Product", b =>
                {
                    b.HasOne("XuongMay_BE.Data.Category", "Category")
                        .WithMany("Products")
                        .HasForeignKey("CategoryID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("XuongMay_BE.Data.ProductionLine", b =>
                {
                    b.HasOne("XuongMay_BE.Data.Supervisor", "Supervisor")
                        .WithMany()
                        .HasForeignKey("SupervisorID");

                    b.Navigation("Supervisor");
                });

            modelBuilder.Entity("XuongMay_BE.Data.Task", b =>
                {
                    b.HasOne("XuongMay_BE.Data.Employee", "Employees")
                        .WithMany("Tasks")
                        .HasForeignKey("EmpID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("XuongMay_BE.Data.Orders", "Orders")
                        .WithMany("Tasks")
                        .HasForeignKey("OrderID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("XuongMay_BE.Data.Stage", "Stages")
                        .WithMany("Tasks")
                        .HasForeignKey("StageID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("XuongMay_BE.Data.Supervisor", "Supervisors")
                        .WithMany("Tasks")
                        .HasForeignKey("SupervisorID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employees");

                    b.Navigation("Orders");

                    b.Navigation("Stages");

                    b.Navigation("Supervisors");
                });

            modelBuilder.Entity("XuongMay_BE.Data.Category", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("XuongMay_BE.Data.Employee", b =>
                {
                    b.Navigation("Tasks");
                });

            modelBuilder.Entity("XuongMay_BE.Data.Orders", b =>
                {
                    b.Navigation("OrderDetails");

                    b.Navigation("Tasks");
                });

            modelBuilder.Entity("XuongMay_BE.Data.Product", b =>
                {
                    b.Navigation("OrderDetails");
                });

            modelBuilder.Entity("XuongMay_BE.Data.Stage", b =>
                {
                    b.Navigation("Tasks");
                });

            modelBuilder.Entity("XuongMay_BE.Data.Supervisor", b =>
                {
                    b.Navigation("OrderDetails");

                    b.Navigation("Tasks");
                });
#pragma warning restore 612, 618
        }
    }
}
