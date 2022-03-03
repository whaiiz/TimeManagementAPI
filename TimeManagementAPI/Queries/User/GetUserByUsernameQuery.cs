using MediatR;
using TimeManagementAPI.Models.Responses;

namespace TimeManagementAPI.Queries.User
{
    public record GetUserByUsernameQuery(string Username) : IRequest<GetByUsernameResponse>;
}
