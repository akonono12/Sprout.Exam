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
    internal class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand, Unit>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IValidator<UpdateEmployeeCommand> _validator;
        public UpdateEmployeeCommandHandler(IEmployeeRepository employeeRepository, IValidator<UpdateEmployeeCommand> validator)
        {
            _employeeRepository = employeeRepository;
            _validator = validator;
        }
        public async Task<Unit> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(string.Join(" \n ", validationResult.Errors.Select(x => x.ErrorMessage).ToList()));
            }

            var employee = await _employeeRepository.GetAllEmployees().SingleOrDefaultAsync(x => x.Id ==  request.Id);

            employee.Update(request.FullName, request.Birthdate, request.Tin, (EmployeeType)request.TypeId);

            await _employeeRepository.SaveChangesAsync();

            return Unit.Value;

        }
    }
}
