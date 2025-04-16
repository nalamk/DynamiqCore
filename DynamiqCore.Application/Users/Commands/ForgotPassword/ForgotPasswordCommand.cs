using DynamiqCore.Shared.Results;
using MediatR;

namespace DynamiqCore.Application.Users.Commands.ForgotPassword;

public class ForgotPasswordCommand : IRequest<Result<string>>
{
    public string Email { get; set; }
    public string ResetUrl { get; set; }
    
    public ForgotPasswordCommand(string email, string resetUrl)
    {
        Email = email;
        ResetUrl = resetUrl;
    }
}