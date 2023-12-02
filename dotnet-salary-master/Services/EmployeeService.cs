using dotnet_salary_master.Data;
using dotnet_salary_master.DTOs.Requests;
using dotnet_salary_master.Entities;
using dotnet_salary_master.Jwt;
using dotnet_salary_master.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace dotnet_salary_master.Services
{
    public class EmployeeService
    {
        private readonly ApplicationDbContext _dbContext;

        public EmployeeService (ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool DeleteEmployee(int id)
        {
            var employee = _dbContext.Employees.FirstOrDefault(x => x.EmployeeId == id);
            if (employee == null)
            {
                return false;
            }
            _dbContext.Employees.Remove(employee);
            _dbContext.SaveChanges();
            return true;
        }

        public bool UpdateEmployee(Employee employee)
        {
            _dbContext.Employees.Update(employee);
            _dbContext.SaveChanges();
            return true;
        }

        public bool AddEmployee(EmployeeRequest employee)
        {
            Employee newEmployee = new()
            {
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Position = employee.Position,
                GrossSalary = employee.GrossSalary,
                IncomeTaxRatio = employee.IncomeTaxRatio
            };

            _dbContext.Employees.Add(newEmployee);
            _dbContext.SaveChanges();
            return true;
        }

        public IEnumerable<CalculatedEmployee> GetEmployees()
        {
            var employees = _dbContext.Employees
                             .Select(u => new
                             {
                                 FullName = $"{u.FirstName} {u.LastName}",
                                 Position = u.Position,
                                 GrossSalary = u.GrossSalary,
                                 IncomeTaxRatio = u.IncomeTaxRatio
                             })
                             .ToList();

            return employees.Select(u => new CalculatedEmployee
            {
                FullName = u.FullName,
                Position = u.Position,
                GrossSalary = u.GrossSalary,
                IncomeTaxRatio = u.IncomeTaxRatio,
                EmployerSgkBurden = CalculateEmployerSgkBurden(u.GrossSalary),
                NetSalary = CalculateNetSalary(u.GrossSalary, u.IncomeTaxRatio)
            });
        }

        private decimal CalculateNetSalary(decimal grossSalary, decimal incomeTaxRatio)
        {
            decimal netSalary = grossSalary - (grossSalary * (incomeTaxRatio / 100));
            return netSalary;
        }

        private decimal CalculateEmployerSgkBurden(decimal grossSalary)
        {
            decimal employerSgkBurden = grossSalary * 0.23m; 
            return employerSgkBurden;
        }

        public Employee? GetById(int id)
        {
            return _dbContext.Employees.FirstOrDefault(x => x.EmployeeId == id);
        }
    }
}
