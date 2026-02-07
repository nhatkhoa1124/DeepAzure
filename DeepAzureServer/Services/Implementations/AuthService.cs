using DeepAzureServer.Services.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace DeepAzureServer.Services.Implementations
{
    public class AuthService : IAuthService
    {
        public Task<string> GenerateJwtToken()
        {
            throw new NotImplementedException();
        }

        public Task<string?> Login()
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResult> RegisterAsync()
        {
            throw new NotImplementedException();
        }
    }
}
