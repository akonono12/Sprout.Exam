using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
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
    internal class CalculateEmployeeSalaryCommandHandler : IRequestHandler<CalculateEmployeeSalaryCommand, decimal>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IValidator<CalculateEmployeeSalaryCommand> _validator;

        public CalculateEmployeeSalaryCommandHandler(IEmployeeRepository employeeRepository, IValidator<CalculateEmployeeSalaryCommand> validator)
        {
            _employeeRepository = employeeRepository;
            _validator = validator;
        }
        public async Task<decimal> Handle(CalculateEmployeeSalaryCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request);
            decimal netPay = new decimal(0);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(string.Join(" \n", validationResult.Errors.Select(x => x.ErrorMessage).ToList()));
            }

            var employee = await _employeeRepository.GetAllEmployees().SingleOrDefaultAsync(x => x.Id == request.Id);

            if (employee.EmployeeTypeId == (int)EmployeeType.Regular)
            {
                netPay = employee.CalculateRegularEmployeeSalary((decimal)request.AbsentDays);
            }
            else
            {
                netPay = employee.CalculateContractualEmployeeSalary((decimal)request.WorkedDays);
            }

            return netPay;
        }
    }
}
