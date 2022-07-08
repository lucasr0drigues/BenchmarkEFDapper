using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenchmarkEFDapper
{
    public class BenchmarkDbContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost,1433;Database=BenchmarkDapperEF;User ID=sa;Password=1q2w3e4r@#$");
        }
    }
}
