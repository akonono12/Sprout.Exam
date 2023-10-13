using MediatR;
using Sprout.Exam.Business.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprout.Exam.Business.Employees.Queries
{
    public class GetEmployeeByIdQuery:IRequest<EmployeeDto>
    {
        public int Id { get; set; }
    }
}
