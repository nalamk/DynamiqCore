using DynamiqCore.Shared.Results;
using MediatR;

namespace DynamiqCore.Application.Users.Commands.ActivateAccount
{
    public class ActivateAccountCommand : IRequest<Result<string>>
    {
        public string UserId { get; set; } = default!;
        public string Token { get; set; } = default!;

        public ActivateAccountCommand(string userId, string token)
        {
            UserId = userId;
            Token = token;
        }
    }
}
