using DynamiqCore.Application.Users.Commands.OrganizationSignup.Dtos;
using DynamiqCore.Domain.Entities;
using DynamiqCore.Domain.Interfaces;
using DynamiqCore.Shared.Results;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace DynamiqCore.Application.Users.Commands.OrganizationSignup;

public class OrganizationSignupCommandHandler : IRequestHandler<OrganizationSignupCommand, Result<OrganizationSignupDto>>
{
     #region Fields

    private readonly ILogger<OrganizationSignupCommandHandler> _logger;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IEmailService _emailService;

    #endregion

    #region Constructor

    public OrganizationSignupCommandHandler(
        ILogger<OrganizationSignupCommandHandler> logger,
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager,
        IEmailService emailService)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        _roleManager = roleManager ?? throw new ArgumentNullException(nameof(roleManager));
        _emailService = emailService ?? throw new ArgumentNullException(nameof(emailService));
    }

    #endregion

    #region Handler

    public async Task<Result<OrganizationSignupDto>> Handle(OrganizationSignupCommand request, CancellationToken cancellationToken)
    {
        try
        {
            if (request.OrganizationId == Guid.Empty)
            {
                return Result<OrganizationSignupDto>.NotFound("OrganizationId can not be empty.");
            }
                
            // Create the new user
            var newUser = new ApplicationUser
            {
                UserName = request.Username,
                Email = request.Email,
                OrganizationId = request.OrganizationId,
                EmailConfirmed = false
            };

            var creationResult = await _userManager.CreateAsync(newUser, request.Password);
            if (!creationResult.Succeeded)
            {
                var errors = creationResult.Errors.Select(e => e.Description).ToArray();
                return Result<OrganizationSignupDto>.BadRequest(errors);
            }
            
            // Ensure role Exists in AspNetRoles table
            string roleName = request.UserRoles.ToString();
            var roleExists = await _roleManager.RoleExistsAsync(roleName);
            if (!roleExists)
            {
                var roleCreationResult = await _roleManager.CreateAsync(new IdentityRole(roleName));
                if (!roleCreationResult.Succeeded)
                {
                    return Result<OrganizationSignupDto>.InternalServerError("Failed to create role.");
                }
            }
            
            // Assign Role a user
            var roleAssignmentResult = await _userManager.AddToRoleAsync(newUser, roleName);
            if (!roleAssignmentResult.Succeeded)
            {
                return Result<OrganizationSignupDto>.InternalServerError("Failed to assign role to the user.");
            }

            // Generate the email confirmation token
            var emailConfirmationToken = await _userManager.GenerateEmailConfirmationTokenAsync(newUser);

            // Generate the confirmation link
            var activationLink = $"http://localhost:5137/api/account/v1/ActivateAccount/?userId={newUser.Id}&token={Uri.EscapeDataString(emailConfirmationToken)}";

            // Send activation email
            var subject = "Activate your account";
            var message = $"Please activate your account by clicking the following link: {activationLink}";
            await _emailService.SendEmailAsync(request.Email, subject, message);
            
            var organizationSignupDto = new OrganizationSignupDto()
            {
                Message = "Confirmation email sent. Please verify your email.",
                Email = newUser.Email
            };

            // Return success with a message that the email has been sent
            return Result<OrganizationSignupDto>.Success(organizationSignupDto);

        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Error occurred during user signup");
            return Result<OrganizationSignupDto>.InternalServerError("An error occurred while processing your request.");
        }
        
    }

    #endregion
    
}