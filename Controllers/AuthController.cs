using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using RestaurantAPI.Services;

namespace RestaurantAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(RestaurantAPI.DTOs.LoginRequest request)
        {
            var response = await _authService.LoginAsync(request);
            if (response == null)
            {
                return Unauthorized(new { message = "Invalid username or password" });
            }
            return Ok(response);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RestaurantAPI.DTOs.RegisterRequest request)
        {
            var response = await _authService.RegisterAsync(request);
            if (response == null)
            {
                return BadRequest(new { message = "Registration failed" });
            }
            return Ok(response);
        }
    }
}