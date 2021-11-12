using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace QuarterlySales.Models
{
    public class QuarterlySalesContext : DbContext
    {
        public QuarterlySalesContext(DbContextOptions<QuarterlySalesContext> options) : base(options)
        { }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Sale> Sales { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().HasData(
                new Employee
                {
                    EmployeeId = 1,
                    FirstName = "Ada",
                    LastName = "Lovelace",
                    DateOfBirth = new DateTime(1956, 12, 10),
                    DateOfHire = new DateTime(1995, 1, 1),
                    ManagerId = 0,
                    EmployeeName = "Ada Lovelace"
                },
                new Employee
                {
                    EmployeeId = 2,
                    FirstName = "John",
                    LastName = "Doe",
                    DateOfBirth = new DateTime(1987, 1, 1),
                    DateOfHire = new DateTime(1996, 1, 1),
                    ManagerId = 1,
                    EmployeeName = "John Doe"
                }
            );
        }
    }
}
