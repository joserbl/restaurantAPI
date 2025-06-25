using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestaurantAPI.DTOs;


namespace RestaurantAPI.Services
{
    public interface IAuthService
    {
        Task<AuthResponse?> LoginAsync(LoginRequest request);
        Task<AuthResponse?> RegisterAsync(RegisterRequest request);
        string GenerateJwtToken(string username, string role);
    }
}