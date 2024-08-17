using Microsoft.EntityFrameworkCore;

namespace XuongMay_BE.Data
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options) { }

        #region DbSet
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductionLine> ProductionLines { get; set; }
        public DbSet<Supervisor> Supervisors { get; set; }
        public DbSet<Customer> Customers { get; set; }
        #endregion
    }
}
