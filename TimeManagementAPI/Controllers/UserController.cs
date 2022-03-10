using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using MediatR;
using TimeManagementAPI.Filters;
using Microsoft.AspNetCore.Authorization;
using TimeManagementAPI.Commands.User;
using TimeManagementAPI.Queries.User;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace TimeManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [CustomExceptionFilter]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPut("UpdateUserDefaultFocusTime")]
        public async Task<IActionResult> UpdateUserDefaultFocusTime(int focusTime)
        {
            await _mediator.Send(new UpdateUserDefaultFocusTimeCommand(User.Identity.Name, focusTime));
            return Ok();
        }

        [HttpPut("UpdateUserDefaultBreakTime")]
        public async Task<IActionResult> UpdateUserDefaultBreakTime(int breakTime)
        {
            await _mediator.Send(new UpdateUserDefaultFocusTimeCommand(User.Identity.Name, breakTime));
            return Ok();
        }

        [HttpGet("GetUser")]
        public async Task<IActionResult> GetUser()
        {
            return Ok(await _mediator.Send(new GetUserByUsernameQuery(User.Identity.Name)));
        }
    }
}
