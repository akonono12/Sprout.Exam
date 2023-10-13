using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Sprout.Exam.DataAccess.EmployeeManagement.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprout.Exam.Business.Employees.Commands
{
    
    public class CalculateEmployeeSalaryCommandValidator : AbstractValidator<CalculateEmployeeSalaryCommand>
    {
        public CalculateEmployeeSalaryCommandValidator(IEmployeeRepository repository)
        {
            RuleFor(x => x.Id)
           .MustAsync(async (id, token) =>
           {
               var result = await repository.GetAllEmployees().AnyAsync(x => x.Id == id, token);

               return result;
           })
           .When(x => x.Id != 0)
           .WithMessage("Employee detail has not been found");

            RuleFor(x => x.WorkedDays)
            .GreaterThan(0)
            .When(x => x.WorkedDays.HasValue && x.WorkedDays.Value > 0)
            .WithMessage("WorkedDays be greater than 0");
           

            RuleFor(x => x.AbsentDays)
            .GreaterThan(0)
            .When(x => x.AbsentDays.HasValue && x.AbsentDays.Value > 0)
            .WithMessage("AbsentDays be greater than 0");

        }
    }
}
