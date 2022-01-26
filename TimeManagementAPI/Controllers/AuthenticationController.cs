using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TimeManagementAPI.Dtos;
using MediatR;
using TimeManagementAPI.Commands.Authentication;
using TimeManagementAPI.Filters;
using Microsoft.AspNetCore.Authorization;

namespace TimeManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [CustomExceptionFilter]
    public class AuthenticationController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthenticationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterDto request)
        {
            var response = await _mediator.Send(new RegisterCommand(request));
            return StatusCode(response.StatusCode, response.Message);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto request)
        {
            var response = await _mediator.Send(new LoginCommand(request.Username, request.Password));
            return StatusCode(response.StatusCode, response.Message);
        }

        [Authorize]
        [HttpPost("confirmEmail")]
        public async Task<IActionResult> ConfirmEmail(string token)
        {
            var response = await _mediator.Send(new ConfirmEmailCommand(User.Identity.Name, token));
            return StatusCode(response.StatusCode, response.Message);
        }
    }
}
