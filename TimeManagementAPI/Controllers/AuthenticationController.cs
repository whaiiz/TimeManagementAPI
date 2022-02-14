using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using MediatR;
using TimeManagementAPI.Commands.Authentication;
using TimeManagementAPI.Filters;
using TimeManagementAPI.Models.Requests.Authentication;

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
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            var response = await _mediator.Send(new RegisterCommand(request));
            return StatusCode(response.StatusCode, response.Message);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var response = await _mediator.Send(new LoginCommand(request.Username, request.Password));
            return StatusCode(response.StatusCode, response.Message);
        }

        [HttpGet("confirmEmail")]
        public async Task<IActionResult> ConfirmEmail(string token)
        {
            var response = await _mediator.Send(new ConfirmEmailCommand(token));
            return StatusCode(response.StatusCode, response.Message);
        }
    }
}
