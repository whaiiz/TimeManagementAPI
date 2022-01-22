﻿using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TimeManagementAPI.Dtos;
using MediatR;
using TimeManagementAPI.Commands.Authentication;
using TimeManagementAPI.Filters;

namespace TimeManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [CustomExceptionFilterAttribute]
    public class AuthenticationController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthenticationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserDto request)
        {
            await _mediator.Send(new RegisterCommand(request));
            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserDto request)
        {
            var token = await _mediator.Send(new LoginCommand(request.Username, request.Password));

            if (token == "") return BadRequest("The username or password you’ve entered is incorrect.");

            return Ok(token);
        }
    }
}
