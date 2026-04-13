using Hoya.Inventory.Application.Interfaces;
using Hoya.Inventory.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace Hoya.Inventory.API.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {

        private readonly IJwtService _jwtService;

        public AuthController( IJwtService jwtService)
        {

            _jwtService = jwtService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var user = await _jwtService.GetUser(request.Email.ToLower());

            if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.Password))
                return Unauthorized("Invalid credentials");

            var token = _jwtService.GenerateToken(user.Id, user.Email);

            return Ok(new { token });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            // 1. Check if user already exists
            if (string.IsNullOrWhiteSpace(request.Email) || request.Password.Length < 6)
                return BadRequest("Invalid input");

            var existingUser = await _jwtService.GetUser(request.Email);

            if (existingUser != null)   
                return BadRequest("User already exists");

            // 2. Hash password
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.Password);

            // 3. Create user
            await _jwtService.AddUserAsync(request.Email, hashedPassword);


            // 4. Generate JWT (optional but recommended)

            return Ok(new
            {
                message = "User registered successfully",

            });
        }


        [HttpGet("healthCheck")]
        [AllowAnonymous]
        public IActionResult Get()
        {

            return Ok("App is up and running");
        }
    }
}
