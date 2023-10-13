using MediatR;
using Microsoft.EntityFrameworkCore;
using Sprout.Exam.Business.DataTransferObjects;
using Sprout.Exam.DataAccess.EmployeeManagement.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sprout.Exam.Business.Employees.Queries
{
    internal class GetEmployeeByIdQueryHandler : IRequestHandler<GetEmployeeByIdQuery, EmployeeDto>
    {
        private readonly IEmployeeRepository _employeeRepository;

        public GetEmployeeByIdQueryHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        public async Task<EmployeeDto> Handle(GetEmployeeByIdQuery request, CancellationToken cancellationToken)
        {
            var employee = await _employeeRepository.GetAllEmployees().SingleOrDefaultAsync(x => x.Id == request.Id);
            if (employee == null)
            {
                return new EmployeeDto();
            }
            return new EmployeeDto()
            {
                FullName = employee.FullName,
                Tin = employee.TIN,
                TypeId = employee.EmployeeTypeId,
                Birthdate = employee.BirthDate.ToString("yyyy-MM-dd"),
                Id = employee.Id
            };
        }
    }
}
