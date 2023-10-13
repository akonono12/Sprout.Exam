using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprout.Exam.Business.Employees.Commands
{
    public class CalculateEmployeeSalaryCommand:IRequest<decimal>
    {
        public int Id { get; set; }
        public decimal? AbsentDays { get; set;}
        public decimal? WorkedDays { get; set; }
    }
}
