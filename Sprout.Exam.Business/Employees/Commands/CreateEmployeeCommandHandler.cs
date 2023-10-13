using FluentValidation;
using MediatR;
using Sprout.Exam.Business.DataTransferObjects;
using Sprout.Exam.Common.Enums;
using Sprout.Exam.DataAccess.EmployeeManagement.Entities;
using Sprout.Exam.DataAccess.EmployeeManagement.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sprout.Exam.Business.Employees.Commands
{
    internal class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, int>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IValidator<CreateEmployeeCommand> _validator;
        public CreateEmployeeCommandHandler(IEmployeeRepository employeeRepository, IValidator<CreateEmployeeCommand> validator) 
        {
            _employeeRepository = employeeRepository;
            _validator = validator;
        }
        public async Task<int> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(string.Join(" \n", validationResult.Errors.Select(x => x.ErrorMessage).ToList()));
            }

            var regularEmployee = new Employee(request.FullName, request.Birthdate, request.Tin, (EmployeeType)request.TypeId);
            _employeeRepository.AddEmployee(regularEmployee);

            await _employeeRepository.SaveChangesAsync();
            return regularEmployee.Id;
        }
    }
}
