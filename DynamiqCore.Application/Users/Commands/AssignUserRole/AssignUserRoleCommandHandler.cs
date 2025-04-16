using DynamiqCore.Domain.Entities;
using DynamiqCore.Domain.Exceptions;
using DynamiqCore.Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace DynamiqCore.Application.Users.Commands.AssignUserRole;

public class AssignUserRoleCommandHandler : IRequestHandler<AssignUserRoleCommand>
{
    #region Fields

    private readonly ILogger<AssignUserRoleCommandHandler> _logger;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly UserManager<ApplicationUser> _userManager;

    #endregion

    #region Constructors

    public AssignUserRoleCommandHandler(
        ILogger<AssignUserRoleCommandHandler> logger,
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        _roleManager = roleManager ?? throw new ArgumentNullException(nameof(roleManager));
    }

    #endregion

    #region Handler

    public async Task Handle(AssignUserRoleCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Assigning user role: {@Request}", request);

        var user = (await _userManager.FindByEmailAsync(request.UsernameOrEmail) ??
                   await _userManager.FindByNameAsync(request.UsernameOrEmail))
                   ?? throw new NotFoundException(nameof(ApplicationUser), request.UsernameOrEmail);
        
        var role = await _roleManager.FindByNameAsync(request.RoleName)
                   ?? throw new NotFoundException(nameof(IdentityRole), request.RoleName);

        await _userManager.AddToRoleAsync(user, role.Name!);
    }

    #endregion

}