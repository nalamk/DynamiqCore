using DynamiqCore.Shared.Results;
using MediatR;

namespace DynamiqCore.Application.Users.Commands.ResetPassword;

public class ResetPasswordCommand : IRequest<Result<string>>
{
    public string Email { get; set; }
    public string Token { get; set; } // The token generated during Forgot Password
    public string NewPassword { get; set; }

    public ResetPasswordCommand(string email, string token, string newPassword)
    {
        Email = email;
        Token = token;
        NewPassword = newPassword;
    }
}