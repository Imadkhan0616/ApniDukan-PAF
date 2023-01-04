using ApniDukan.DatabaseIntegration.Model;
using ApniDukan.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Newtonsoft.Json.Linq;

namespace ApniDukan.DatabaseIntegration
{
    public class ApniDukanContext : DbContext
    {
        public DbSet<Product> Product { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<OrderItem> OrderItem { get; set; }
        public DbSet<Customer> Customer { get; set; }
        public DbSet<Coupon> Coupon { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string databaseConfigString = File.ReadAllText(@"E:\ApniDukaan\ApniDukan\ApniDukan.DatabaseIntegration\bin\Debug\net6.0\DatabaseConfig.json");
            JObject databaseConfig = JObject.Parse(databaseConfigString);

            DatabaseConfigModel databaseConfigModel = databaseConfig.ToObject<DatabaseConfigModel>();

            optionsBuilder.UseSqlServer("Server=DESKTOP-2LU51K8\\MYSQL;Database=ApniDukaan_CodeFirst;User Id=sa;Password=sa123;");

            //optionsBuilder.UseSqlServer(
            //    $"Server={databaseConfigModel.ServerName};" +
            //    $"Database={databaseConfigModel.DatabaseName};" +
            //    $"User Id={databaseConfigModel.UserId};" +
            //    $"Password={databaseConfigModel.Password};");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity(typeof(User)).Property(nameof(Models.User.UserID)).UseHiLo("Seq_User","Admin");
            modelBuilder.Entity(typeof(Order)).Property(nameof(Models.Order.OrderID)).UseHiLo("Seq_Order","Cart");
            modelBuilder.Entity(typeof(OrderItem)).Property(nameof(Models.OrderItem.OrderItemID)).UseHiLo("Seq_OrderItem","Cart");
            modelBuilder.Entity(typeof(Customer)).Property(nameof(Models.Customer.CustomerID)).UseHiLo("Seq_Customer","Cart");
        }

        public async Task CommitChangesAsync()
        {
            IDbContextTransaction transaction = Database.BeginTransaction();
            try
            {
                await SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}