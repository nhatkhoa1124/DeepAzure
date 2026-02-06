using Microsoft.AspNetCore.Identity;

namespace DeepAzureServer.Services.Interfaces
{
    public interface IAuthService
    {
        public Task<IdentityResult> RegisterAsync();
        public Task<string?> Login();
        public Task<string> GenerateJwtToken();
    }
}
