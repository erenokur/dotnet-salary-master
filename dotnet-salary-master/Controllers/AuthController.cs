using dotnet_salary_master.Data;
using dotnet_salary_master.DTOs.Requests;
using dotnet_salary_master.Models;
using dotnet_salary_master.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Diagnostics;

namespace dotnet_salary_master.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : Controller
    {
        private readonly ILogger<AuthController> _logger;
        private UserService _userService;
        private AppSettings _appSettings;

        public AuthController(ILogger<AuthController> logger, IOptions<AppSettings> appSettings, ApplicationDbContext dbContext)
        {
            _userService = new UserService(appSettings, dbContext);
            _logger = logger;
            _appSettings = appSettings.Value;
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    

        [HttpPost("Login")]
        public IActionResult Login(AuthenticateRequest request)
        {
            var response = _userService.Authenticate(request);

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(response);
        }

        [HttpPost("Register")]
        public IActionResult Register(RegisterRequest request)
        {
            var response = _userService.Register(request);
            if (response == false)
                return BadRequest(new { message = "Register Failed" });

            return Ok(new { message = "User registered successfully!" });
        }

        [Authorize(Policy = "AdminPolicy")]
        [HttpGet]
        public IActionResult GetAll()
        {
            var users = _userService.GetAll();
            return Ok(users);
        }

        [Authorize]
        [HttpGet("GetUserData")]
        public IActionResult GetUserData()
        {
            var userId = HttpContext.User.FindFirst("id")?.Value;
            var userName = HttpContext.User.FindFirst("username")?.Value;
            if (!string.IsNullOrEmpty(userName))
            {
                return Ok(new { username = userName, userId = userId });
            }
            else
            {
                return BadRequest(new { message = "Username or password is not found" });
            }

        }

        [Authorize(Policy = "AdminPolicy")]
        [HttpPost("ChangeUserRole")]
        public IActionResult changeUserRole(ChangeUserRoleRequest request)
        {
            var result = _userService.ChangeUserRoleRequest(request);
            if (result)
            {
                return Ok();
            }
            return BadRequest();
        }

        [Authorize(Policy = "UserOrSellerPolicy")]
        [HttpPost("UpdateUser")]
        public IActionResult UpdateUser(UpdateUserRequest request)
        {
            var userId = HttpContext.User.FindFirst("id")?.Value;
            request.UserId = Convert.ToInt32(userId);
            var result = _userService.UpdateUser(request);
            if (result)
            {
                return Ok();
            }
            return BadRequest();
        }
    }
}
