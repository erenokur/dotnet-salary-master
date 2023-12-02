using dotnet_salary_master.enums;
using System.ComponentModel.DataAnnotations;

namespace dotnet_salary_master.DTOs.Requests
{
    public class AuthenticateRequest
    {
        [Required]
        public string Email { get; set; } = String.Empty;

        [Required]
        public string Password { get; set; } = String.Empty;
    }
}
