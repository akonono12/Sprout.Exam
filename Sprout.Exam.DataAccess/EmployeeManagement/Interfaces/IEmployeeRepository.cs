using Sprout.Exam.DataAccess.EmployeeManagement.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprout.Exam.DataAccess.EmployeeManagement.Interfaces
{
    public interface IEmployeeRepository
    {
        IQueryable<Employee> GetAllEmployees();
        void AddEmployee(Employee employee);
        Task SaveChangesAsync();
    }
}
