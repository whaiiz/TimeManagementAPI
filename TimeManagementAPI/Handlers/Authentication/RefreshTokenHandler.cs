using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TimeManagementAPI.Commands.Authentication;
using TimeManagementAPI.Exceptions;
using TimeManagementAPI.Models.Responses;
using TimeManagementAPI.Repositories.Interfaces;
using TimeManagementAPI.Utils;

namespace TimeManagementAPI.Handlers.Authentication
{
    public class RefreshTokenHandler : IRequestHandler<RefreshTokenCommand, RefreshTokenResponse>
    {
        private readonly IConfiguration _configuration;
        private readonly IUserRepository _userRepository;
        private readonly IMediator _mediator;

        public RefreshTokenHandler(IConfiguration configuration, IUserRepository userRepository,
            IMediator mediator)
        {
            _configuration = configuration;
            _userRepository = userRepository;
            _mediator = mediator;
        }

        private ClaimsPrincipal GetClaimsFromExpiredAccessToken(string accessToken)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:TokenKey"])),
                ValidateLifetime = false
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(accessToken, tokenValidationParameters, out SecurityToken securityToken);
            
            if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(
                SecurityAlgorithms.HmacSha512Signature, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException(Messages.InvalidToken);

            return principal;
        }

        public async Task<RefreshTokenResponse> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var principal = GetClaimsFromExpiredAccessToken(request.AccessToken);
            var username = principal.Identity.Name;
            var user = await _userRepository.GetByUsername(username);

            if (user == null) throw new UserNotFoundException();
            if (user.RefreshToken != request.RefreshToken) throw new InvalidTokenException();
            if (user.RefreshTokenExpiryTime <= DateTime.Now) return new RefreshTokenResponse()
            {
                RefreshTokenExpired = true,
                Message = Messages.RefreshTokenExpired,
                StatusCode = 401
            };

            return new RefreshTokenResponse()
            {
                StatusCode = 200,
                RefreshToken = await _mediator.Send(new GenerateRefreshTokenCommand(user), cancellationToken),
                AccessToken = await _mediator.Send(new GenerateAccessTokenCommand(user), cancellationToken),
            };
        }
    }
}
