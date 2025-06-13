using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Crypto.Generators;
using RedBook.Core.Constants;
using System.Text;

namespace WebAPI.Configurations
{
    /// <summary>
    /// This class creates database if it doesn't exist and initializes database with seed data
    /// </summary>
    public static class DatabaseIntializer
    {
        /// <summary>
        /// Creates database if it doesn't exist and initializes database with seed data
        /// </summary>
        public static void InitDatabase(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                AppDBContext context = scope.ServiceProvider.GetRequiredService<AppDBContext>();

                // Only run database migrations in development environment
                if (env.IsDevelopment())
                {
                    if (context.Database.GetPendingMigrations().Any())
                    {
                        context.Database.Migrate();
                    }

                    SeedData(context);
                }
            }
        }

        private static void SeedData(AppDBContext context)
        {
            context.Database.EnsureCreated();

            #region Create SysAdmin User
            User? sysAdminUser = context.Users.FirstOrDefault(x =>
                x.FirstName == "Md. Nafis"
                && x.LastName == "Sadik"
                && x.UserName == "nafis_sadik"
            );

            if (sysAdminUser == null)
            {
                string? password = BCrypt.Generate(
                        Encoding.UTF8.GetBytes("a8i#YJVFzk9N"),
                        RedBook.Core.Constants.CommonConstants.PasswordConfig.SaltByte,
                        CommonConstants.PasswordConfig.SaltGeneratorLogRounds
                    ).ToString();
                if (string.IsNullOrEmpty(password))
                    throw new Exception("Failed to generate password hash for sysadmin user.");

                sysAdminUser = context.Users.Add(new User
                {
                    FirstName = "Md. Nafis",
                    LastName = "Sadik",
                    UserName = "nafis_sadik",
                    Password = password,
                    Status = true,
                    Email = "nafis.sadik13@yahoo.com",
                    Address = "12072 Betsy Manors, Tuanstad, ND 16008-8307",
                    PhoneNumber = "(888) 900 1994",
                }).Entity;

                context.SaveChanges();
            }
            #endregion

            context.SaveChanges();

            context.Dispose();
        }
    }
}
