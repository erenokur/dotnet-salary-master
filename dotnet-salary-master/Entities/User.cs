using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using dotnet_salary_master.enums;

namespace dotnet_salary_master.Entities
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string UserName { get; set; } = String.Empty;

        [Required]
        [MaxLength(255)]
        public string Email { get; set; } = String.Empty;

        [Required]
        [MaxLength(255)]
        public string Password { get; set; } = String.Empty;

        [Required]
        public DateTime Created { get; set; }

        [Required]
        public UserRoles Role { get; set; } = UserRoles.Guest;
    }
}
