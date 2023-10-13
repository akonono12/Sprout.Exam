using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Sprout.Exam.DataAccess.EmployeeManagement.Entities;
using Sprout.Exam.Common.Enums;

namespace Sprout.Exam.DataAccess.EmployeeManagement.Configurations
{
    
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.HasKey(c => c.Id);
            builder.HasQueryFilter(x => !x.IsDeleted);

        }
    }
}
