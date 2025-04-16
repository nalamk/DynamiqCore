using DynamiqCore.Application.Users.Commands.ForgotPassword;
using DynamiqCore.Application.Users.Commands.Login;
using DynamiqCore.Domain.Entities;
using DynamiqCore.Domain.Repositories;
using DynamiqCore.Shared.Results;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace DynamiqCore.Application.Users.Commands.ResetPassword;

public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand, Result<string>>
{
    #region Fields
    
    private readonly ILogger<ResetPasswordCommandHandler> _logger;
    private readonly UserManager<ApplicationUser> _userManager;

    #endregion
    
    #region Constructor

    public ResetPasswordCommandHandler(
        ILogger<ResetPasswordCommandHandler> logger,
        UserManager<ApplicationUser> userManager)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
    }

    
    #endregion
    
    #region Handler
    
    public async Task<Result<string>> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handling password reset for email: {Email}", request.Email);
        
        // Step 1: Find the user by email
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user is null)
        {
            _logger.LogWarning("User not found: {Email}", request.Email);
            return Result<string>.NotFound("Invalid email address.");
        }
        
        // Step 2: Reset the user's password
        var result = await _userManager.ResetPasswordAsync(user, request.Token, request.NewPassword);

        // Step 3: Check for errors
        if (!result.Succeeded)
        {
            _logger.LogWarning("Failed to reset password for {Email}", request.Email);
            return Result<string>.BadRequest(result.Errors.Select(e => e.Description).ToArray());
        }

        _logger.LogInformation("Password successfully reset for {Email}", request.Email);
        return Result<string>.Success("Password reset successfully.");
        
    }
    
    #endregion
}