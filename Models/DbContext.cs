using System.Transactions;
using System;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using Microsoft.Extensions.Configuration;
using Radit_BackEnd_30012024.Models;

namespace Radit_BackEnd_30012024.Models
{
    public class DbMainContext : DbContext
    {
        public DbMainContext()
        {
        }

        public virtual DbSet<Customer> Customer { get; set; }
        public virtual DbSet<Transaction> Transaction { get; set; }

        public DbMainContext(DbContextOptions<DbMainContext> options)
            : base(options)
        { }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var envName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            IConfigurationRoot configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false)
                .Build();
            optionsBuilder.UseMySql(configuration["ConnectionStrings:db_connection"], Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.18-mysql"));
        }
    }
}
