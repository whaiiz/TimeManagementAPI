using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TimeManagementAPI.Commands.Authentication;
using TimeManagementAPI.Controllers;
using TimeManagementAPI.Utils;
using Xunit;

namespace TimeManagementAPI.Tests.Controllers.Authentication
{
    public class ConfirmEmailTests
    {
        [Fact]
        public async Task ConfirmEmail_InvalidToken()
        {
            // Arrange
            var mediator = new Mock<IMediator>();
            var controller = new AuthenticationController(mediator.Object);

            mediator.Setup(m => m.Send(It.IsAny<ConfirmEmailCommand>(), default))
                .ReturnsAsync(new ResponseModel(400, "The token sent is invalid or it has already expired!"));

            // Act
            var result = await controller.ConfirmEmail("token");

            // Assert
            var response = Assert.IsType<ObjectResult>(result);
            Assert.Equal("The token sent is invalid or it has already expired!", response.Value);
            Assert.Equal(400, response.StatusCode);
        }

        [Fact]
        public async Task ConfirmEmail_Success()
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
