using MediatR;

namespace TimeManagementAPI.Commands.Email
{
    public record EmailSenderCommand(string DestinyEmail, string Subject, string Body) : IRequest<bool>;
}
