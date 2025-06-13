using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Data.Entities
{
    public class AppDBContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public AppDBContext(DbContextOptions<AppDBContext> options, IConfiguration configuration)
            : base(options)
        {
            _configuration = configuration;
        }

        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string? connectionString = _configuration["DbConfig:ConnectionString"];
            if (string.IsNullOrEmpty(connectionString))
                throw new ArgumentException("Database connection string is not provided in configuration.");

            if (_configuration["DbConfig:DatabaseProvider"] == "MYSQL")
                optionsBuilder.UseMySQL(connectionString);
            else if (_configuration["DbConfig:DatabaseProvider"] == "MSSQL")
                optionsBuilder.UseSqlServer(connectionString);
            else if (_configuration["DbConfig:DatabaseProvider"] == "AZURE_SQL")
                optionsBuilder.UseSqlServer(connectionString);
            else
                throw new ArgumentException("Invalid database connection string provided.");
        }
    }
}
