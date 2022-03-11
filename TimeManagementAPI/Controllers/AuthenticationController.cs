using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using MediatR;
using TimeManagementAPI.Commands.Authentication;
using TimeManagementAPI.Filters;
using TimeManagementAPI.Models.Requests.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

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
            
            if (response.StatusCode == 200)
            {
                HttpContext.Response.Cookies.Append("access_token", response.AccessToken, 
                    new CookieOptions { HttpOnly = true });
                HttpContext.Response.Cookies.Append("refresh_token", response.RefreshToken,
                    new CookieOptions { HttpOnly = true });
            }

            return StatusCode(response.StatusCode, response.Message);
        }

        [HttpGet("confirmEmail")]
        public async Task<IActionResult> ConfirmEmail(string token)
        {
            var response = await _mediator.Send(new ConfirmEmailCommand(token));
            return StatusCode(response.StatusCode, response.Message);
        }

        [HttpPost("forgotPassword")]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            var response = await _mediator.Send(new ForgotPasswordCommand(email));
            return StatusCode(response.StatusCode, response.Message);
        }

        [HttpPost("resetPassword")]
        [Authorize]
        public async Task<IActionResult> ResetPassword([FromBody] string password)
        {
            var response = await _mediator.Send(new ResetPasswordCommand(User, password));
            return StatusCode(response.StatusCode, response.Message);
        }

        [HttpPost("refreshToken")]
        public async Task<IActionResult> RefreshToken()
        {
            var accessToken = Request.Cookies["access_token"];
            var refreshToken = Request.Cookies["refresh_token"];
            await _mediator.Send(new RefreshTokenCommand(accessToken, refreshToken));

            return Ok();
        }

        [HttpPost("refreshToken")]
        public async Task<IActionResult> RevokeToken()
        {

        }
    }
}
