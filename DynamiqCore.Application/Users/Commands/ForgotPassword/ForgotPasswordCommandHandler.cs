using DynamiqCore.Domain.Entities;
using DynamiqCore.Domain.Interfaces;
using DynamiqCore.Domain.Repositories;
using DynamiqCore.Shared.Results;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace DynamiqCore.Application.Users.Commands.ForgotPassword;

public class ForgotPasswordCommandHandler : IRequestHandler<ForgotPasswordCommand, Result<string>>
{
    
    #region Fields
    
    private readonly ILogger<ForgotPasswordCommandHandler> _logger;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IEmailService _emailService;
    
    #endregion
    
    #region Constructors

    public ForgotPasswordCommandHandler(
        ILogger<ForgotPasswordCommandHandler> logger,
        UserManager<ApplicationUser> userManager,
        IEmailService emailService)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        _emailService = emailService ?? throw new ArgumentNullException(nameof(emailService));
    }
    
    #endregion
    
    #region Handler

    public async Task<Result<string>> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Forgot user password: {@Request}", request);

            // Step 1: Find the user by their email address
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user is null)
            {
                _logger.LogWarning("User not found: {Email}", request.Email);
                return Result<string>.NotFound("User with this email does not exist.");
            }

            // Step 2: Generate a password reset token for the user
            var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
            _logger.LogInformation("Generated reset token for user: {Email}", request.Email);

            // Step 3: Create the password reset link
            var resetUrl = $"{request.ResetUrl}?token={Uri.EscapeDataString(resetToken)}&email={Uri.EscapeDataString(user.Email)}";

            // Step 4: Send the password reset email
            var emailSubject = "Reset Your Password";
            var emailBody = $"Please reset your password by clicking this link: {resetUrl}";

            await _emailService.SendEmailAsync(user.Email, "Reset Your Password", emailBody);
            _logger.LogInformation("Password reset email sent to {Email}", request.Email);
            return Result<string>.Success("Password reset email sent successfully.");

        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Error sending password reset email to {Email}", request.Email);
            return Result<string>.InternalServerError("Failed to send password reset email.");
        }
    }
    
    #endregion
}
