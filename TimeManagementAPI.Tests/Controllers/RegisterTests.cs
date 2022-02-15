using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TimeManagementAPI.Commands.Authentication;
using TimeManagementAPI.Controllers;
using TimeManagementAPI.Models.Requests.Authentication;
using TimeManagementAPI.Utils;
using Xunit;

namespace TimeManagementAPI.Tests.Controllers
{
    public class RegisterTests
    {
        private (Mock<IMediator>, AuthenticationController, RegisterRequest) GetBaseMocks()
        {
            var mediator = new Mock<IMediator>();
            var controller = new AuthenticationController(mediator.Object);
            var user = new RegisterRequest()
            {
                Password = "Teste",
                Email = "Teste@gmail.com",
                Username = "Teste@gmail.com",
            };

            return (mediator, controller, user);
        }


        [Fact]
        public async Task Register_NotUniqueUsername()
        {
            // Arrange
            var (mediator, controller, user) = GetBaseMocks();
            
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
            var (mediator, controller, user) = GetBaseMocks();

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
            var (mediator, controller, user) = GetBaseMocks();

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

        [Fact]
        public async Task Register_Success()
        {
            // Arrange
            var (mediator, controller, user) = GetBaseMocks();

            mediator.Setup(m => m.Send(It.IsAny<RegisterCommand>(), default))
                .ReturnsAsync(new ResponseModel(200, 
                "User registered with success! Please go to your email to activate your account."));

            // Act
            var result = await controller.Register(user);

            // Assert
            var response = Assert.IsType<ObjectResult>(result);
            Assert.Equal("User registered with success! Please go to your email to activate your account.", response.Value);
            Assert.Equal(200, response.StatusCode);
        }
    }
}