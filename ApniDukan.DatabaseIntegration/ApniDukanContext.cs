using ApniDukan.DatabaseIntegration.Model;
using ApniDukan.Models;
using Microsoft.EntityFrameworkCore;
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
    }
}