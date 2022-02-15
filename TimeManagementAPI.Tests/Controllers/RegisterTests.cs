using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TimeManagementAPI.Commands.Authentication;
using TimeManagementAPI.Controllers;
using TimeManagementAPI.Models.Requests.Authentication;
using TimeManagementAPI.Utils;
using Xunit;

namespace TimeManagementAPI.Tests
{
    public class RegisterTests
    {
        [Fact]
        public async Task Register_NotUniqueUsername()
        {
            // Arrange
            var mediator = new Mock<IMediator>();
            var controller = new AuthenticationController(mediator.Object);
            var user = new RegisterRequest()
            {
                Password = "Teste",
                Email = "Teste@gmail.com",
                Username = "Teste@gmail.com",
            };

            mediator.Setup(m => m.Send(It.IsAny<RegisterCommand>(), default))
                .ReturnsAsync(new ResponseModel(400, "Username already exists!"));

            // Act
            var result = await controller.Register(user);

            // Assert
            var response = Assert.IsType<ObjectResult>(result);
            Assert.Equal("Username already exists!", response.Value);
            Assert.Equal(400, response.StatusCode);
        }

        [Fact]
        public async Task Register_NotUniqueEmail()
        {
            // Arrange
            var mediator = new Mock<IMediator>();
            var controller = new AuthenticationController(mediator.Object);
            var user = new RegisterRequest()
            {
                Password = "Teste",
                Email = "Teste@gmail.com",
                Username = "Teste",
            };

            mediator.Setup(m => m.Send(It.IsAny<RegisterCommand>(), default))
                .ReturnsAsync(new ResponseModel(400, "Email already exists!"));

            // Act
            var result = await controller.Register(user);

            // Assert
            var response = Assert.IsType<ObjectResult>(result);
            Assert.Equal("Email already exists!", response.Value);
            Assert.Equal(400, response.StatusCode);
        }
        [Fact]
        public async Task Register_FailSendingEmailConfirmation()
        {
            // Arrange
            var mediator = new Mock<IMediator>();
            var controller = new AuthenticationController(mediator.Object);
            var user = new RegisterRequest()
            {
                Password = "Teste",
                Email = "Teste@gmail.com",
                Username = "Teste",
            };

            mediator.Setup(m => m.Send(It.IsAny<RegisterCommand>(), default))
                .ReturnsAsync(new ResponseModel(400, 
                "User registered with success! but there was an " +
                "error sending email confirmation! Please try to log in."!));

            // Act
            var result = await controller.Register(user);

            // Assert
            var response = Assert.IsType<ObjectResult>(result);
            Assert.Equal("User registered with success! but there was an " +
                "error sending email confirmation! Please try to log in.", response.Value);
            Assert.Equal(400, response.StatusCode);
        }
    }

}