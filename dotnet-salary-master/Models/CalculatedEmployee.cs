namespace dotnet_salary_master.Models
{
    public class CalculatedEmployee
    {
        public string FullName { get; set; }
        public string Position { get; set; }
        public decimal GrossSalary { get; set; }
        public decimal IncomeTaxRatio { get; set; }
        public decimal EmployerSgkBurden { get; set; }
        public decimal NetSalary { get; set; }
    }
}
