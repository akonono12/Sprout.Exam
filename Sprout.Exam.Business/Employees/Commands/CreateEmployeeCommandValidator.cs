using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Sprout.Exam.DataAccess.EmployeeManagement.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprout.Exam.Business.Employees.Commands
{
    public class CreateEmployeeCommandValidator : AbstractValidator<CreateEmployeeCommand>
    {
        public CreateEmployeeCommandValidator(IEmployeeRepository repository)
        {

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


            RuleFor(x => x.FullName)
            .MustAsync(async (name, token) =>
            {
                var result = await repository.GetAllEmployees().AnyAsync(x => x.FullName == name, token);

                return !result;
            })
            .When(x => !string.IsNullOrEmpty(x.FullName))
            .WithMessage("Name has already exist");

            RuleFor(x => x.Tin)
            .MustAsync(async (tin, token) =>
            {
                var result = await repository.GetAllEmployees().AnyAsync(x => x.TIN == tin, token);

                return !result;
            })
            .When(x => !string.IsNullOrEmpty(x.Tin))
            .WithMessage("TIN  has already exist");

        }
    }
}
