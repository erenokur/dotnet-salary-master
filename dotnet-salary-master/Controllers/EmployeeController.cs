using dotnet_salary_master.Data;
using dotnet_salary_master.DTOs.Requests;
using dotnet_salary_master.Entities;
using dotnet_salary_master.Models;
using dotnet_salary_master.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Diagnostics;

namespace dotnet_salary_master.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeeController : Controller
    {
        private readonly ILogger<AuthController> _logger;
        private EmployeeService _employeeService;
        private AppSettings _appSettings;

        public EmployeeController(ILogger<AuthController> logger, IOptions<AppSettings> appSettings, ApplicationDbContext dbContext)
        {
            _employeeService = new EmployeeService(dbContext);
            _logger = logger;
            _appSettings = appSettings.Value;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [Authorize(Policy = "AdminPolicy")]
        [HttpGet("GetEmployees")]
        public IActionResult GetEmployees()
        {
            var employees = _employeeService.GetEmployees();
            return Ok(employees);
        }

        [Authorize(Policy = "AdminPolicy")]
        [HttpPost("AddEmployee")]
        public IActionResult AddEmployee(EmployeeRequest employee)
        {
            var response = _employeeService.AddEmployee(employee);
            if (response == false)
                return BadRequest(new { message = "Add Employee Failed" });

            return Ok(new { message = "Employee added successfully!" });
        }


        [Authorize(Policy = "AdminPolicy")]
        [HttpPost("UpdateEmployee")]
        public IActionResult UpdateEmployee(Employee employee)
        {
            var response = _employeeService.UpdateEmployee(employee);
            if (response == false)
                return BadRequest(new { message = "Update Employee Failed" });

            return Ok(new { message = "Employee updated successfully!" });
        }


        [Authorize(Policy = "AdminPolicy")]
        [HttpPost("DeleteEmployee")]
        public IActionResult DeleteEmployee(int id)
        {
            var response = _employeeService.DeleteEmployee(id);
            if (response == false)
                return BadRequest(new { message = "Delete Employee Failed" });

            return Ok(new { message = "Employee deleted successfully!" });
        }

        [Authorize(Policy = "AdminPolicy")]
        [HttpGet]
        public IActionResult GetById(int id)
        {
            var employee = _employeeService.GetById(id);
            return Ok(employee);
        }

    }
}
