using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprout.Exam.Business.Employees.Commands
{
    public class DeleteEmployeeCommand:IRequest<Unit>
    {
        public int Id { get; set; }
    }
}
