using dotnet_salary_master.enums;
using System.ComponentModel.DataAnnotations;

namespace dotnet_salary_master.DTOs.Requests
{
    public class ChangeUserRoleRequest
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        public UserRoles Role { get; set; } = UserRoles.Guest;
    }
}
