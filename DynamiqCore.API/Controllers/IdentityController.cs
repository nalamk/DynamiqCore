using DynamiqCore.Application.Users.Commands;
using DynamiqCore.Application.Users.Commands.AssignUserRole;
using DynamiqCore.Application.Users.Commands.UnassignUserRole;
using DynamiqCore.Domain.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DynamiqCore.API.Controllers;

[ApiController]
[Authorize(Roles = UserRoles.SystemAdmin)]
[Route("api/identity/v1")]
public class IdentityController : ControllerBase
{
    #region Fields

    private readonly IMediator _mediator;

    #endregion
    
    #region Constructor

    public IdentityController(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }
    
    #endregion

    #region APIs

    [HttpPatch("UpdateUserDetail")]
    public async Task<IActionResult> UpdateUserDetails([FromBody]UpdateUserDetailsCommand command)
    {
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpPost("AssignRoleToUser")]
    [Authorize(Roles = UserRoles.SystemAdmin)]
    public async Task<IActionResult> AssignUserRole(AssignUserRoleCommand command)
    {
        await _mediator.Send(command);
        return NoContent();
    }
    
    [HttpDelete("UnAssignUserRole")]
    [Authorize(Roles = UserRoles.SystemAdmin)]
    public async Task<IActionResult> UnassignUserRole(UnassignUserRoleCommand command)
    {
        await _mediator.Send(command);
        return NoContent();
    }

    #endregion
    
}