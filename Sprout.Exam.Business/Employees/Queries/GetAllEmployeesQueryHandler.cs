using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Sprout.Exam.Business.DataTransferObjects;
using Sprout.Exam.Business.Employees.Commands;
using Sprout.Exam.DataAccess.EmployeeManagement.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sprout.Exam.Business.Employees.Queries
{
    internal class GetAllEmployeesQueryHandler : IRequestHandler<GetAllEmployeesQuery, List<EmployeeDto>>
    {
        private readonly IEmployeeRepository _employeeRepository;

        public GetAllEmployeesQueryHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        public async Task<List<EmployeeDto>> Handle(GetAllEmployeesQuery request, CancellationToken cancellationToken)
        {
            var employees = await _employeeRepository.GetAllEmployees().ToListAsync();
            var result = new List<EmployeeDto>();
            foreach (var employee in employees)
            {
                result.Add(new EmployeeDto()
                {
                    FullName = employee.FullName,
                    Tin = employee.TIN,
                    TypeId = employee.EmployeeTypeId,
                    Birthdate = employee.BirthDate.ToString("yyyy-MM-dd"),
                    Id = employee.Id,
                });
            }
            return result;
        }
    }
}
