using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Sprout.Exam.DataAccess.EmployeeManagement.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprout.Exam.Business.Employees.Commands
{

    public class DeleteEmployeeCommandValidator : AbstractValidator<DeleteEmployeeCommand>
    {
        public DeleteEmployeeCommandValidator(IEmployeeRepository repository)
        {
              RuleFor(x => x.Id)
             .MustAsync(async (id, token) =>
             {
                 var result = await repository.GetAllEmployees().AnyAsync(x => x.Id == id, token);

                 return result;
             })
             .When(x => x.Id != 0)
             .WithMessage("Employee detail has not been found");

        }
    }
}
