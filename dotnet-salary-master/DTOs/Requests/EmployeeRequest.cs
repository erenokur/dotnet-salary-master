using System.ComponentModel.DataAnnotations;

namespace dotnet_salary_master.DTOs.Requests
{
    public class EmployeeRequest
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Position { get; set; }

        [Required]
        public decimal GrossSalary { get; set; }

        [Required]
        public decimal IncomeTaxRatio { get; set; }
    }
}
