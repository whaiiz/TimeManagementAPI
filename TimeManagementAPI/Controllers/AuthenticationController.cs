using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TimeManagementAPI.Dtos;
using MediatR;
using TimeManagementAPI.Commands.Authentication;
using TimeManagementAPI.Filters;

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
            var token = await _mediator.Send(new LoginCommand(request.Username, request.Password));

            if (token == "") return BadRequest("The username or password you’ve entered is incorrect.");

            return Ok(token);
        }
    }
}
