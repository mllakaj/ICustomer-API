using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Drawing.Printing;

namespace ICustomer_API.Models
{
    public class ICustomerDbContext : DbContext
    {
        public ICustomerDbContext(DbContextOptions<ICustomerDbContext> options) : base(options) { }
        public DbSet<Customer> Customers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server = LAPTOP - IDLT3ES4\\MARIUS; Database = master; Integrated Security = True; MultipleActiveResultSets=true");
            }
        }
    }
}
