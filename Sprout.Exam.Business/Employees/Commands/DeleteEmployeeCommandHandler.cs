using FluentValidation;
using MediatR;
using Sprout.Exam.DataAccess.EmployeeManagement.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Sprout.Exam.Common.Enums;
using Sprout.Exam.DataAccess.EmployeeManagement.Entities;

namespace Sprout.Exam.Business.Employees.Commands
{
    
    internal class DeleteEmployeeCommandHandler : IRequestHandler<DeleteEmployeeCommand, Unit>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IValidator<DeleteEmployeeCommand> _validator;
        public DeleteEmployeeCommandHandler(IEmployeeRepository employeeRepository, IValidator<DeleteEmployeeCommand> validator)
        {
            _employeeRepository = employeeRepository;
            _validator = validator;
        }
        public async Task<Unit> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(string.Join(" \n", validationResult.Errors.Select(x => x.ErrorMessage).ToList()));
            }

            var employee = await _employeeRepository.GetAllEmployees().SingleOrDefaultAsync(x => x.Id == request.Id);

            employee.Delete();

            await _employeeRepository.SaveChangesAsync();

            return Unit.Value;

        }
    }
}
