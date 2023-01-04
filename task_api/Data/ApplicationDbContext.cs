using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using task_api.Models;

namespace task_api.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Employee>().HasData(
                new Employee
                {
                    Id = 1,
                    Name= "mohamed",
                    Address="1St",
                    PhoneNumber="0123456789",
                    DepartementId = 1,
                    username="mohamed_123",
                    password="123456",
                },
              new Employee
              {
                  Id = 2,
                  Name = "hesham",
                  Address = "1St",
                  PhoneNumber = "0123456789",
                  DepartementId = 1,
                  username = "hesham_123",
                  password = "123456",
              },
              new Employee
              {
                  Id = 3,
                  Name = "ebrahim",
                  Address = "1St",
                  PhoneNumber = "0123456789",
                  DepartementId = 1,
                  username = "ebrahim_123",
                  password = "123456",
              },
              new Employee
              {
                  Id = 4,
                  Name = "mustafa",
                  Address = "1St",
                  PhoneNumber = "0123456789",
                  DepartementId = 1,
                  username = "mustafa_123",
                  password = "123456",
              },
              new Employee
              {
                  Id = 5,
                  Name = "ali",
                  Address = "1St",
                  PhoneNumber = "0123456789",
                  DepartementId = 1,
                  username = "ali_123",
                  password = "123456",
              });
            modelBuilder.Entity<Departement>().HasData(
                    new Departement
                    {
                        Id=1,
                        Name = "Dep1",
                
                    });
        }
        public DbSet<Employee> Employess { get; set; }
        public DbSet<Departement> Departements { get; set; }

    }
}
