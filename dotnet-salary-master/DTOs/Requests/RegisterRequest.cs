using dotnet_salary_master.enums;
using System.ComponentModel.DataAnnotations;

namespace dotnet_salary_master.DTOs.Requests
{
    public class RegisterRequest
    {
        [Required]
        public string Email { get; set; } = String.Empty;

        [Required]
        public string Password { get; set; } = String.Empty;

        [Required]
        public string UserName { get; set; } = String.Empty;

        [Required]
        public UserRoles Role { get; set; } = UserRoles.Guest;
    }
}
