﻿namespace dotnet_salary_master.DTOs.Responses
{
    public class AuthenticateResponse 
    {
        public int Id { get; set; }
        public string UserName { get; set; } = String.Empty;
        public string User { get; set; } = String.Empty;
        public string AccessToken { get; set; } = String.Empty;
        public string Message { get; set; } = String.Empty;
    }
}
