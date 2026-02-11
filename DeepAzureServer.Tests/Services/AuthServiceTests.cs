using DeepAzureServer.Data.Configurations;
using DeepAzureServer.Models.Entities;
using DeepAzureServer.Models.Requests;
using DeepAzureServer.Services.Implementations;
using DeepAzureServer.Tests.Helpers;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Moq;

namespace DeepAzureServer.Tests.Services
{
    public class AuthServiceTests
    {
        private readonly Mock<UserManager<User>> _userManagerMock;
        private readonly AuthService _authService;

        public AuthServiceTests()
        {
            _userManagerMock = MockHelpers.MockUserManager<User>();

            var jwtSettings = new JwtSettings
            {
                SecretKey = "SuperSecretKeyForTestingOnly12345!",
                Issuer = "DeepAzureTest",
                Audience = "DeepAzureClient",
                AccessTokenExpirationMinutes = 60
            };

            var optionsMock = Options.Create(jwtSettings);
            _authService = new AuthService(_userManagerMock.Object, optionsMock);
        }

        // --- REGISTER TESTS ---
        [Fact]
        public async Task RegisterAsync_ShouldReturnSuccess_WhenIdentitySucceeds()
        {
            var request = new RegisterRequest()
            {
                Email = "correctregister@gmail.com",
                Password = "StrongPassword123!"
            };

            _userManagerMock.Setup(x => x.FindByEmailAsync(request.Email))
                .ReturnsAsync((User) null); // This checks for existing email
            _userManagerMock.Setup(x => x.CreateAsync(It.IsAny<User>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);
            _userManagerMock.Setup(x => x.AddToRoleAsync(It.IsAny<User>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);

            var result = await _authService.RegisterAsync(request);

            result.Success.Should().BeTrue();
            result.Errors.Should().BeNullOrEmpty();
            result.Token.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public async Task RegisterAsync_ShouldReturnErrors_WhenIdentityFails()
        {
            var request = new RegisterRequest()
            {
                Email = "incorrect-email",
                Password = "unsafe-incorrect-password@@"
            };

            _userManagerMock.Setup(x => x.FindByEmailAsync(request.Email))
                .ReturnsAsync((User) null);
            var expectedError = new IdentityError { Description = "Password too weak"};
            _userManagerMock.Setup(x => x.CreateAsync(It.IsAny<User>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Failed(expectedError));
            _userManagerMock.Setup(x => x.AddToRoleAsync(It.IsAny<User>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Failed(expectedError));

            var result = await _authService.RegisterAsync(request);

            result.Success.Should().BeFalse();
            result.Errors.Should().Contain("Password too weak");
            result.Token.Should().BeNullOrEmpty();
        }

        [Fact]
        public async Task RegisterAsync_ShouldReturnErrors_WhenEmailAlreadyExists()
        {
            var request = new RegisterRequest()
            {
                Email = "alreadyexisted@gmail.com",
                Password = "StrongPassword123!"
            };
            _userManagerMock.Setup(x => x.FindByEmailAsync(request.Email))
            .ReturnsAsync(new User {Email = "alreadyexisted@gmail.com"});

            var result = await _authService.RegisterAsync(request);

            result.Success.Should().BeFalse();
            result.Errors.Should().Contain("Email already in use");

            // Object might be created in database even though Success is false
            _userManagerMock.Verify(x => x.CreateAsync(It.IsAny<User>(), It.IsAny<string>()), Times.Never);
        }

        // --- LOGIN TESTS ---
        [Fact]
        public async Task LoginAsync_ShouldReturnTrue_WhenCredentialsAreCorrect()
        {
            var request = new LoginRequest
            {
                Email = "correctlogin@gmail.com",
                Password = "StrongPassword123!"
            };

            _userManagerMock.Setup(x => x.FindByEmailAsync(request.Email))
                .ReturnsAsync(new User { Email = request.Email, UserName = "Customer" });
            _userManagerMock.Setup(x => x.CheckPasswordAsync(It.IsAny<User>(), It.IsAny<string>()))
                .ReturnsAsync(true);

            var result = await _authService.LoginAsync(request);

            result.Success.Should().BeTrue();
            result.Errors.Should().BeNullOrEmpty();
            result.Token.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public async Task LoginAsync_ShouldReturnErrors_WhenEmailDoesNotExists()
        {
            var request = new LoginRequest
            {
                Email = "notexists@gmail.com",
                Password = "StrongPassword123!"
            };

            _userManagerMock.Setup(x => x.FindByEmailAsync(request.Email))
                .ReturnsAsync((User) null);

            var result = await _authService.LoginAsync(request);

            result.Success.Should().BeFalse();
            result.Errors.Should().Contain("Invalid login attempt");
        }
    }
}
