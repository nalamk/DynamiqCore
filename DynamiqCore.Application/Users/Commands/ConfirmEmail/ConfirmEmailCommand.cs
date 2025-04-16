using DynamiqCore.Shared.Results;
using MediatR;

namespace DynamiqCore.Application.Users.Commands.ConfirmEmail;

public class ConfirmEmailCommand : IRequest<Result<Unit>>
{
    public string UserId { get; set; }
    public string Token { get; set; }

    public ConfirmEmailCommand(string userId, string token)
    {
        UserId = userId;
        Token = token;
    }
}
