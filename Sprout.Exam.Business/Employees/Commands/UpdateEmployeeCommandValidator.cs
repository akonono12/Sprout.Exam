using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Sprout.Exam.DataAccess.EmployeeManagement.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprout.Exam.Business.Employees.Commands
{
    public class UpdateEmployeeCommandValidator : AbstractValidator<UpdateEmployeeCommand>
    {
        public UpdateEmployeeCommandValidator(IEmployeeRepository repository)
        {
            RuleFor(x => x.Id)
           .MustAsync(async (id, token) =>
           {
               var result = await repository.GetAllEmployees().AnyAsync(x => x.Id == id, token);

               return result;
           })
           .When(x => x.Id != 0)
           .WithMessage("Employee detail has not been found");

            RuleFor(x => x.FullName)
            .NotEmpty()
            .WithMessage("FullName must not be empty");

            RuleFor(x => x.Birthdate)
            .NotEmpty()
            .WithMessage("Birthdate must not be empty");

            RuleFor(x => x.Tin)
            .NotEmpty()
            .WithMessage("Tin must not be empty");

            RuleFor(x => x.TypeId)
            .NotEmpty()
            .WithMessage("Employee Type must not be empty");


            RuleFor(x => x)
            .MustAsync(async (model, token) =>
            {
                var result = await repository.GetAllEmployees().SingleOrDefaultAsync(x => x.FullName == model.FullName && x.Id != model.Id, token);
                if (result == null)
                {
                    return true;
                }
                return false;
            })
            .When(x => !string.IsNullOrEmpty(x.FullName))
            .WithMessage("Name has already exist");

            RuleFor(x => x)
            .MustAsync(async (model, token) =>
            {
                var result = await repository.GetAllEmployees().SingleOrDefaultAsync(x => x.TIN == model.Tin && x.Id != model.Id, token);

                if (result == null)
                {
                    return true;
                }
                return false;
            })
            .When(x => !string.IsNullOrEmpty(x.Tin))
            .WithMessage("TIN  has already exist");

        }
    }
}
