using DynamiqCore.Application.Users.Dtos.Role;
using DynamiqCore.Domain.Repositories;
using DynamiqCore.Shared.Results;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace DynamiqCore.Application.Users.Commands.AssignRole;

public class AssignRoleCommandHandler : IRequestHandler<AssignRoleCommand, Result<AssignRoleDto>>
{
    #region Fields
    
    private readonly ILogger<AssignRoleCommandHandler> _logger;
    private readonly RoleManager<IdentityRole> _roleManager;
    
    #endregion

    #region Constructor
    
    public AssignRoleCommandHandler(ILogger<AssignRoleCommandHandler> logger, RoleManager<IdentityRole> roleManager)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _roleManager = roleManager ?? throw new ArgumentNullException(nameof(roleManager));
    }
    
    #endregion
    
    #region Handler

    public async Task<Result<AssignRoleDto>> Handle(AssignRoleCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Add new role: {@Request}", request);

        var existingRole = await _roleManager.FindByNameAsync(request.RoleName);
        if (existingRole != null)
        {
            _logger.LogInformation($"Role already exists: {request.RoleName}", request.RoleName);
            return Result<AssignRoleDto>.Conflict("Role already exists");
        }

        var newRole = new IdentityRole(request.RoleName);
        var result = await _roleManager.CreateAsync(newRole);

        if (result.Succeeded)
        {
            _logger.LogInformation($"Role created successfully: {request.RoleName}", request.RoleName);
            var assignRoleDto = new AssignRoleDto
            {
                RoleName = request.RoleName
            };
            return Result<AssignRoleDto>.Created(assignRoleDto, $"Role created successfully: {request.RoleName}");
        }
        else
        {
            _logger.LogError($"Failed to create role: {request.RoleName}", request.RoleName);
            return Result<AssignRoleDto>.InternalServerError($"Failed to create role: {request.RoleName}");
        }
    }
    
    #endregion
}
