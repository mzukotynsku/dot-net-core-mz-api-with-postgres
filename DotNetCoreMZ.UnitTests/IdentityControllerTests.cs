using DotNetCoreMZ.API.Contracts.V1.Requests;
using DotNetCoreMZ.API.Contracts.V1.Responses;
using DotNetCoreMZ.API.Controllers.V1;
using DotNetCoreMZ.API.Domain;
using DotNetCoreMZ.API.Services;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace DotNetCoreMZ.UnitTests
{
    public  class IdentityControllerTests
    {
        private readonly Mock<IIdentityService> _identityService;
        private IdentityController sut;

        public IdentityControllerTests()
        {
            _identityService = new Mock<IIdentityService>();
            sut = new IdentityController(_identityService.Object);

            
        }

        [Fact]
        public async Task Register_ShouldReturn_BadRequest_With_Error_InvalidEmailAddress()
        {
            // Arrange
            var userRegistrationRequest = new UserRegistrationRequest { Email = "emailWithoutAtSignExample.com", Password = "My$trongSecretPa$$w0rd! " };

            // Act
            sut.ModelState.AddModelError("ErrorMessage", "Invalid email address");
            var result =  await sut.Register(userRegistrationRequest);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<BadRequestObjectResult>();
            var okObjectResult = result as BadRequestObjectResult;
            var authFailedResponse = okObjectResult.Value as AuthFailedResponse;
            authFailedResponse.Errors.Should().Contain(new List<string> { "Invalid email address" });
        }

        [Fact]
        public async Task Register_ShouldReturn_BadRequest_With_Error_PasswordIsRequired()
        {
            // Arrange
            var userRegistrationRequest = new UserRegistrationRequest { Email = "emailWithoutAtSignExample.com",};

            // Act
            sut.ModelState.AddModelError("ErrorMessage", "Password Is Required");
            var result = await sut.Register(userRegistrationRequest);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<BadRequestObjectResult>();
            var okObjectResult = result as BadRequestObjectResult;
            var authFailedResponse = okObjectResult.Value as AuthFailedResponse;
            authFailedResponse.Errors.Should().Contain(new List<string> { "Password Is Required" });
        }

        [Fact]
        public async Task Register_ShouldReturn_BadRequest_With_InvalidAuthResponse()
        {
            // Arrange
            var userRegistrationRequest = new UserRegistrationRequest { Email = "newEmail@example.com", Password = "incorrectPassword" };
            var authResponse= GetInvalidAuthenticationResult();
            _identityService.Setup(x => x.RegisterAsync(It.IsAny<string>(), It.IsAny<string>())).Returns(authResponse);

            // Act
            sut.ModelState.AddModelError("ErrorMessage", "User/password combination is wrong");
            var result = await sut.Register(userRegistrationRequest);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<BadRequestObjectResult>();
            var okObjectResult = result as BadRequestObjectResult;
            var authFailedResponse = okObjectResult.Value as AuthFailedResponse;
            authFailedResponse.Errors.Should().Contain(new List<string> { "User/password combination is wrong" });
        }

        [Fact]
        public async Task Register_ShouldRegister_User_With_ValidAuthResponse()
        {
            // Arrange
            var userRegistrationRequest = new UserRegistrationRequest { Email = "newEmail@example.com", Password = "My$trongSecretPa$$w0rd! " };
            var authResponse = GetValidAuthenticationResult();
            _identityService.Setup(x => x.RegisterAsync(It.IsAny<string>(), It.IsAny<string>())).Returns(authResponse);

            // Act
            var result = await sut.Register(userRegistrationRequest);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
            var okObjectResult = result as OkObjectResult;
            var authSuccessResponse = okObjectResult.Value as AuthSuccessResponse;
            authSuccessResponse.RefreshToken.Should().BeEquivalentTo(authResponse.Result.RefreshToken);
            authSuccessResponse.Token.Should().BeEquivalentTo(authResponse.Result.Token);
            authResponse.Result.Success.Should().BeTrue();
        }

        [Fact]
        public async Task Login_ShouldReturn_BadRequest_WithoutLoggedUser()
        {
            // Arrange
            var userLoginRequest = new UserLoginRequest { Email = "newEmail@example.com", Password = "incorretPassword" };
            var authResponse = GetInvalidAuthenticationResult();
            _identityService.Setup(x => x.LoginAsync(It.IsAny<string>(), It.IsAny<string>())).Returns(authResponse);

            // Act
            var result = await sut.Login(userLoginRequest);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<BadRequestObjectResult>();
            var okObjectResult = result as BadRequestObjectResult;
            var authFailedResponse = okObjectResult.Value as AuthFailedResponse;
            authFailedResponse.Errors.Should().Contain(new List<string> { "User/password combination is wrong" });
        }

        [Fact]
        public async Task Login_ShouldReturn_LoggedUser()
        {
            // Arrange
            var userLoginRequest = new UserLoginRequest { Email = "newEmail@example.com", Password = "My$trongSecretPa$$w0rd! " };
            var authResponse = GetValidAuthenticationResult();
            _identityService.Setup(x => x.LoginAsync(It.IsAny<string>(), It.IsAny<string>())).Returns(authResponse);

            // Act
            var result = await sut.Login(userLoginRequest);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
            var okObjectResult = result as OkObjectResult;
            var authSuccessResponse = okObjectResult.Value as AuthSuccessResponse;
            authSuccessResponse.RefreshToken.Should().BeEquivalentTo(authResponse.Result.RefreshToken);
            authSuccessResponse.Token.Should().BeEquivalentTo(authResponse.Result.Token);
            authResponse.Result.Success.Should().BeTrue();
        }

        [Fact]
        public async Task Refresh_ShouldReturn_RefreshToken()
        {
            // Arrange
            var refreshTokenRequest = new RefreshTokenRequest { RefreshToken = Guid.NewGuid().ToString(), Token = "ValidToken" };
            var authResponse = GetValidAuthenticationResult();
            _identityService.Setup(x => x.RefreshTokenAsync(It.IsAny<string>(), It.IsAny<string>())).Returns(authResponse);

            // Act
            var result = await sut.Refresh(refreshTokenRequest);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
            var okObjectResult = result as OkObjectResult;
            var authSuccessResponse = okObjectResult.Value as AuthSuccessResponse;
            authSuccessResponse.RefreshToken.Should().BeEquivalentTo(authResponse.Result.RefreshToken);
            authSuccessResponse.Token.Should().BeEquivalentTo(authResponse.Result.Token);
            authResponse.Result.Success.Should().BeTrue();
        }

        [Fact]
        public async Task Refresh_ShouldReturn_BadRequest()
        {
            // Arrange
            var refreshTokenRequest = new RefreshTokenRequest { RefreshToken = Guid.NewGuid().ToString(), Token = "InvalidToken" };
            var authResponse = GetInvalidAuthenticationResult();
            _identityService.Setup(x => x.RefreshTokenAsync(It.IsAny<string>(), It.IsAny<string>())).Returns(authResponse);

            // Act
            var result = await sut.Refresh(refreshTokenRequest);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<BadRequestObjectResult>();
        }

        private async  Task<AuthenticationResult> GetValidAuthenticationResult()
        {
            return await Task.FromResult(new AuthenticationResult
            {
                Success = true,
                Token = "MyValidToken",
                RefreshToken = Guid.NewGuid().ToString()
            });
        }

        private async Task<AuthenticationResult> GetInvalidAuthenticationResult()
        {
            return await Task.FromResult(new AuthenticationResult
            {
                Success = false,
                Token = "MyIvalidToken",
                RefreshToken = Guid.NewGuid().ToString(),
                ErrorMessages = new List<string> { "User/password combination is wrong" }
            });
        }
    }
}
