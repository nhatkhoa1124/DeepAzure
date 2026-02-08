using DeepAzureServer.Models.Requests;
using DeepAzureServer.Models.Responses;
using Microsoft.AspNetCore.Identity;

namespace DeepAzureServer.Services.Interfaces
{
    public interface IAuthService
    {
        public Task<AuthResponse> RegisterAsync(RegisterRequest request);
        public Task<AuthResponse> LoginAsync(LoginRequest request);
    }
}
