﻿namespace dotnet_salary_master.DTOs.Responses
{
    public class RegisterResponse
    {
        public int Id { get; set; }
        public string UserName { get; set; } = String.Empty;
        public string Email { get; set; } = String.Empty;
        public string AccessToken { get; set; } = String.Empty;
    }
}
