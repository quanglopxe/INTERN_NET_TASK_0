using Microsoft.EntityFrameworkCore;

namespace XuongMay_BE.Data
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options) { }

        #region DbSet
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }

        //Trân
        public DbSet<Employee> Employees { get; set; }

        public DbSet<Stage> Stage { get; set; }

        #endregion
    }
}
