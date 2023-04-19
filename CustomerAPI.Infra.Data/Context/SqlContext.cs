using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CustomerAPI.Domain.Entities;
using CustomerAPI.Infra.Data.Mapping;
using Microsoft.Extensions.Configuration;

namespace CustomerAPI.Infra.Data.Context
{
    public class SqlContext : DbContext
    {
        public SqlContext() : base()
        {

        }
        public SqlContext(DbContextOptions<SqlContext> options) 
            : base(options)
        {
            
        }

        public DbSet<Customer> Customers { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Customer>(new CustomerMap().Configure);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                
            }


            optionsBuilder.UseSqlServer("Server=localhost,1433;Database=customer;User ID=sa;Password=1q2w3e4r@#$;TrustServerCertificate=True");

        }
    }
}
