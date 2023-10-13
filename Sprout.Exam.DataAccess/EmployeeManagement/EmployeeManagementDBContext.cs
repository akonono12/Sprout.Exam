using Microsoft.EntityFrameworkCore;
using Sprout.Exam.DataAccess.EmployeeManagement.Configurations;
using Sprout.Exam.DataAccess.EmployeeManagement.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprout.Exam.DataAccess.EmployeeManagement
{
    public class EmployeeManagementDBContext : DbContext
    {
        public EmployeeManagementDBContext(DbContextOptions<EmployeeManagementDBContext> options) : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new EmployeeConfiguration());
            //modelBuilder.ApplyConfiguration(new RegularEmployeeConfiguration());
            //modelBuilder.ApplyConfiguration(new ContractualEmployeeConfiguration());
        }
    }
}
