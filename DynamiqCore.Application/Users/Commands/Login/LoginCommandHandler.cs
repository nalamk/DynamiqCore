using DynamiqCore.Application.JWT;
using DynamiqCore.Application.Users.Dtos.Login;
using DynamiqCore.Domain.Entities;
using DynamiqCore.Shared.Results;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using UserRoles = DynamiqCore.Domain.Enums.UserRoles;

namespace DynamiqCore.Application.Users.Commands.Login;

public class LoginCommandHandler : IRequestHandler<LoginCommand, Result<LoginDto>>
{
    #region Fields
    
    private readonly ILogger<LoginCommandHandler> _logger;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    #endregion

    #region Constructor

    public LoginCommandHandler(
        ILogger<LoginCommandHandler> logger,
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        IJwtTokenGenerator jwtTokenGenerator)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
        _jwtTokenGenerator = jwtTokenGenerator ?? throw new ArgumentNullException(nameof(jwtTokenGenerator));
    }

    #endregion

    #region Handler

    public async Task<Result<LoginDto>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByNameAsync(request.UsernameOrEmail) ??
                   await _userManager.FindByEmailAsync(request.UsernameOrEmail);
        if (user is null)
        {
            return Result<LoginDto>.Unauthorized("Invalid Username/Email or password");
        }

        if(!user.EmailConfirmed)
        {
            return Result<LoginDto>.Unauthorized("Your account is not yet activated. Please check your email for activation link.");
        }

        var signInResult = await _signInManager.PasswordSignInAsync(user.UserName, request.Password, request.RememberMe, lockoutOnFailure: false);
        if (signInResult.Succeeded)
        {
            // Retrieve the user roles
            var roles = await _userManager.GetRolesAsync(user);
            
            // Generate the JWT token (replace "JWT Token" with your actual token generation logic)
            var token = _jwtTokenGenerator.GenerateJwtToken(user, roles);
            
            // Prepare the response object
            var loginDto = new LoginDto
            {
                AccessToken = token,
            };
            
            // Return success with the token and roles
            return Result<LoginDto>.Success(loginDto);
        }

        // Handle account lockout
        if (signInResult.IsLockedOut)
        {
            return Result<LoginDto>.CustomError(423, "User account is locked.");
        }

        // Handle invalid login attempts
        return Result<LoginDto>.Unauthorized("Invalid login attempt.");
    }

    #endregion

}
