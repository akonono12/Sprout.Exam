using Sprout.Exam.Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Sprout.Exam.DataAccess.EmployeeManagement.Entities
{
    [Table("Employee")]
    public class Employee
    {
        public int Id { get; private set; }
        public string FullName { get; private set; }
        public DateTime BirthDate { get; private set; }
        public string TIN { get; private set; }
        public int EmployeeTypeId { get; private set; }
        public bool IsDeleted { get; private set; }
        private decimal DailyRate => 500m;
        private decimal MonthlySalary => 20000m;
        private decimal taxRate => 0.12m;
        private decimal TotalWorkingDaysPerMonth => 21.75m;

        private Employee() { }
        public Employee(string name,DateTime dob,string tin, EmployeeType employeeType) { 
            this.FullName = name;
            this.BirthDate = dob;
            this.TIN = tin;
            this.EmployeeTypeId = (int)employeeType;
        }

        public void Update(string name, DateTime dob, string tin, EmployeeType employeeType)
        {
            this.FullName = name;
            this.BirthDate = dob;
            this.TIN = tin;
            this.EmployeeTypeId = (int)employeeType;
        }

        public void Delete()
        {
            IsDeleted = true;
        }

        public decimal CalculateRegularEmployeeSalary(decimal absences)
        {
          
            decimal dailySalary = Math.Round(MonthlySalary / TotalWorkingDaysPerMonth, 2);
            decimal taxDeduction = Math.Round(MonthlySalary * taxRate, 2);
            decimal absencesDeduction = Math.Round(dailySalary * absences, 2);
            decimal netPay = Math.Round(MonthlySalary - (taxDeduction + absencesDeduction),2);
            return netPay;
           
        }

        public decimal CalculateContractualEmployeeSalary(decimal workDays)
        {
            if (workDays < 0) return 0;
            decimal netPay = Math.Round(DailyRate * workDays, 2);
            return netPay;
            
        }
    }
}
