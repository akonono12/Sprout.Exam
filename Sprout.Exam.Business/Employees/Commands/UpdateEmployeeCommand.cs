using MediatR;
using Sprout.Exam.Business.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprout.Exam.Business.Employees.Commands
{
    public class UpdateEmployeeCommand:EditEmployeeDto,IRequest<Unit>
    {
    }
}
