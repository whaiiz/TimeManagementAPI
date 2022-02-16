using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TimeManagementAPI.Commands.Authentication;
using TimeManagementAPI.Controllers;
using TimeManagementAPI.Models.Requests.Authentication;
using TimeManagementAPI.Utils;
using Xunit;

namespace TimeManagementAPI.Tests.Controllers.Authentication
{
    public class LoginTests
    {
        private static (Mock<IMediator>, AuthenticationController, LoginRequest) GetBaseMocks()
        {
            var mediator = new Mock<IMediator>();
            var controller = new AuthenticationController(mediator.Object);
            var user = new LoginRequest()
            {
                Username = "Teste",
                Password = "Teste"
            };

            return (mediator, controller, user);
        }

        [Fact]
        public async Task Login_UserDoesNotExists()
        {
            // Arrange
            var (mediator, controller, user) = GetBaseMocks();

            mediator.Setup(m => m.Send(It.IsAny<LoginCommand>(), default))
                .ReturnsAsync(new ResponseModel(400, "The user doesn't exist!"));

            // Act
            var result = await controller.Login(user);

            // Assert
            var response = Assert.IsType<ObjectResult>(result);
            Assert.Equal("The user doesn't exist!", response.Value);
            Assert.Equal(400, response.StatusCode);
        }
    }
}
