using DynamiqCore.Application.Users.Commands.Signup.Dtos;
using DynamiqCore.Domain.Entities;
using DynamiqCore.Domain.Interfaces;
using DynamiqCore.Domain.Repositories;
using DynamiqCore.Shared.Results;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace DynamiqCore.Application.Users.Commands.Signup;

public class SignupCommandHandler : IRequestHandler<SignupCommand, Result<SignupDto>>
{
    #region Fields

    private readonly ILogger<SignupCommandHandler> _logger;
    private readonly IOrganizationsRepository _organizationsRepository;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IEmailService _emailService;

    #endregion

    #region Constructor

    public SignupCommandHandler(
        ILogger<SignupCommandHandler> logger,
        IOrganizationsRepository organizationsRepository,
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager,
        IEmailService emailService)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _organizationsRepository = organizationsRepository ?? throw new ArgumentNullException(nameof(organizationsRepository));
        _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        _roleManager = roleManager ?? throw new ArgumentNullException(nameof(roleManager));
        _emailService = emailService ?? throw new ArgumentNullException(nameof(emailService));
    }

    #endregion

    #region Handler

    public async Task<Result<SignupDto>> Handle(SignupCommand request, CancellationToken cancellationToken)
    {
        try
        {
                
            // Create the new user
            var newUser = new ApplicationUser
            {
                UserName = request.Username,
                Email = request.Email,
                OrganizationId = (!request.OrganizationId.HasValue || request.OrganizationId == Guid.Empty) ? null : request.OrganizationId,
                EmailConfirmed = false
            };

            var creationResult = await _userManager.CreateAsync(newUser, request.Password);
            if (!creationResult.Succeeded)
            {
                var errors = creationResult.Errors.Select(e => e.Description).ToArray();
                return Result<SignupDto>.BadRequest(errors);
            }
            
            // Ensure role Exists in AspNetRoles table
            string roleName = request.UserRoles.ToString();
            var roleExists = await _roleManager.RoleExistsAsync(roleName);
            if (!roleExists)
            {
                var roleCreationResult = await _roleManager.CreateAsync(new IdentityRole(roleName));
                if (!roleCreationResult.Succeeded)
                {
                    return Result<SignupDto>.InternalServerError("Failed to create role.");
                }
            }
            
            // Assign Role a user
            var roleAssignmentResult = await _userManager.AddToRoleAsync(newUser, roleName);
            if (!roleAssignmentResult.Succeeded)
            {
                return Result<SignupDto>.InternalServerError("Failed to assign role to the user.");
            }

            // Generate the email confirmation token
            var emailConfirmationToken = await _userManager.GenerateEmailConfirmationTokenAsync(newUser);

            // Generate the confirmation link
            var activationLink = $"http://localhost:5137/api/account/v1/ActivateAccount/?userId={newUser.Id}&token={Uri.EscapeDataString(emailConfirmationToken)}";

            // Send activation email
            var subject = "Activate your account";
            var message = $"Please activate your account by clicking the following link: {activationLink}";
            await _emailService.SendEmailAsync(request.Email, subject, message);
            
            var signupDto = new SignupDto
            {
                Message = "Confirmation email sent. Please verify your email.",
                Email = newUser.Email
            };

            // Return success with a message that the email has been sent
            return Result<SignupDto>.Success(signupDto);

        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Error occurred during user signup");
            return Result<SignupDto>.InternalServerError("An error occurred while processing your request.");
        }
        
    }

    #endregion

}