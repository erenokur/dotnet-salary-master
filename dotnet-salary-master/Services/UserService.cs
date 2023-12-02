using dotnet_salary_master.Data;
using dotnet_salary_master.DTOs.Requests;
using dotnet_salary_master.DTOs.Responses;
using dotnet_salary_master.Entities;
using dotnet_salary_master.Jwt;
using dotnet_salary_master.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;

namespace dotnet_salary_master.Services
{
    public class UserService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly JwtGenerator _jwtGenerator;

        public UserService(IOptions<AppSettings> appSettings, ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _jwtGenerator = new JwtGenerator(appSettings);
        }

        public AuthenticateResponse? Authenticate(AuthenticateRequest model)
        {
            User? user = _dbContext.Users.FirstOrDefault(x => x.Email == model.Email);
            if (user == null) return null;

            bool result = BCrypt.Net.BCrypt.Verify(model.Password, user.Password);
            if (!result) return null;

            // authentication successful so generate jwt token
            string tokenJson = _jwtGenerator.GenerateJwtToken(user);

            AuthenticateResponse response = new()
            {
                Id = user.Id,
                UserName = user.UserName,
                AccessToken = tokenJson
            };
            return response;
        }

        public bool Register(RegisterRequest model)
        {
            User? user = _dbContext.Users.FirstOrDefault(x => x.Email == model.Email);
            if (user != null) return false;
            // hash password
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(model.Password);
            // create user
            User newUser = new()
            {
                Email = model.Email,
                Password = hashedPassword,
                UserName = model.UserName,
                Created = DateTime.UtcNow,
                Role = model.Role
            };

            // insert user
            _dbContext.Users.Add(newUser);
            _dbContext.SaveChanges();
            return true;
        }

        public bool UpdateUser(UpdateUserRequest model)
        {
            User? user = _dbContext.Users.FirstOrDefault(x => x.Id == model.UserId);
            if (user == null) return false;
            if (!string.IsNullOrEmpty(model.Password))
                user.Password = model.Password;
            if (!string.IsNullOrEmpty(model.UserName))
                user.UserName = model.UserName;
            _dbContext.SaveChanges();
            return true;
        }

        public bool ChangeUserRoleRequest(ChangeUserRoleRequest model)
        {
            User? user = _dbContext.Users.FirstOrDefault(x => x.Id == model.UserId);
            if (user == null) return false;
            user.Role = model.Role;
            _dbContext.SaveChanges();
            return true;
        }

        public IEnumerable<User> GetAll()
        {
            return _dbContext.Users
                .Select(u => new User
                {
                    Id = u.Id,
                    UserName = u.UserName,
                    Email = u.Email,
                    Created = u.Created,
                    Role = u.Role
                })
                .ToList();
        }

        public User? GetById(int id)
        {
            return _dbContext.Users.FirstOrDefault(x => x.Id == id);
        }
    }
}
