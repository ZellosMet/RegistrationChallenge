using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;

namespace RegistrationChallenge.Model.DataBase
{
    public class UsersDBContext : DbContext
    {
        public DbSet<DBUser> Users { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();
            string useConnection = configuration.GetSection("UseDbConnection").Value ?? "DefaultConnection";
            optionsBuilder.UseNpgsql(configuration.GetConnectionString(useConnection));
        }
    }
}
