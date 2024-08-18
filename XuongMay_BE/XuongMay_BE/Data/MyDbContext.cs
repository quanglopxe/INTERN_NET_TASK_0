using Microsoft.EntityFrameworkCore;

namespace XuongMay_BE.Data
{
    public class MyDbContext : DbContext
    {
        public MyDbContext()
        {
        }

        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options) { }

        #region DbSet
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Stage> Stage { get; set; }
        public DbSet<ProductionLine> ProductionLines { get; set; }
        public DbSet<Supervisor> Supervisors { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Orders> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Task> Tasks { get; set; }

        public DbSet<User> Users { get; set; }

        #endregion
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderDetail>(entity =>
            {
                entity.ToTable("OrderDetail");
                entity.HasKey(e => new { e.OrderID, e.ProductID, e.SupervisorID });

                entity.HasOne(e => e.Orders)
                    .WithMany(e => e.OrderDetails)
                    .HasForeignKey(e => e.OrderID)
                    .HasConstraintName("FK_OrderDetail_Order");

                entity.HasOne(e => e.Product)
                    .WithMany(e => e.OrderDetails)
                    .HasForeignKey(e => e.ProductID)
                    .HasConstraintName("FK_OrderDetail_Product");

                entity.HasOne(e => e.Supervisor)
                    .WithMany(e => e.OrderDetails)
                    .HasForeignKey(e => e.SupervisorID)
                    .HasConstraintName("FK_OrderDetail_Supervisor");

            });

            
        }
    }
}
