using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Threading.Tasks;
using TimeManagementAPI.Dtos;
using TimeManagementAPI.Models;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using MediatR;
using TimeManagementAPI.Commands.Authentication;

namespace TimeManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IMediator _mediator;

        public static UserModel user { get; set; } = new UserModel();

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
            return Ok(await _mediator.Send(new LoginCommand(request.Username, request.Password)));
        }
    }
}
