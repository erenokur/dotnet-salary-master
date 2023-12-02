namespace dotnet_salary_master.DTOs.Requests
{
    public class UpdateUserRequest
    {
        public string Password { get; set; } = String.Empty;

        public string UserName { get; set; } = String.Empty;

        public int UserId { get; set; }
    }
}
