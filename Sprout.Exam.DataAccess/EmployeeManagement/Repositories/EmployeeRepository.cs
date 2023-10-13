using Sprout.Exam.DataAccess.EmployeeManagement.Entities;
using Sprout.Exam.DataAccess.EmployeeManagement.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprout.Exam.DataAccess.EmployeeManagement.Repositories
{
    public class EmployeeRepository:IEmployeeRepository
    {
        private readonly EmployeeManagementDBContext _dbContext;
        public EmployeeRepository(EmployeeManagementDBContext dBContext)
        {
            _dbContext = dBContext;
        }

        public IQueryable<Employee> GetAllEmployees()
        {
            var result = _dbContext.Employees;
            return result.AsQueryable();
        }

        public void AddEmployee(Employee employee)
        {
            _dbContext.Employees.Add(employee);
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
