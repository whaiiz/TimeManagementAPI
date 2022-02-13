using MediatR;
using Microsoft.AspNetCore.Mvc;
using TimeManagementAPI.Controllers;
using TimeManagementAPI.Dtos;
using Xunit;

namespace TimeManagementAPI.Tests
{
    public class RegisterTests
    {
        [Fact]
        public async Task Register_UsernameShouldBeRequired([FromServices] IMediator mediator)
        {



        }
        public void Register_EmailShouldBeRequired()
        {

        }

        public void Register_EmailShouldHaveAValidFormat()
        {

        }

        public void Register_PasswordShouldBeRequired()
        {

        }

        public void Register_UsernameShouldBeUnique()
        {

        }
        public void Register_EmailShouldBeUnique()
        {

        }

        public void Register_ShouldWork()
        {

        }

    }
}