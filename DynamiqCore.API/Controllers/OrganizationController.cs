using DynamiqCore.Application.Organizations.Commands.CreateOrganization;
using DynamiqCore.Application.Organizations.Commands.DeleteOrganization;
using DynamiqCore.Application.Organizations.Queries.Dtos;
using DynamiqCore.Application.Organizations.Queries.GetAllOrganizations;
using DynamiqCore.Application.Organizations.Queries.GetOrganizationById;
using DynamiqCore.Domain.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DynamiqCore.API.Controllers;

[ApiController]
[Route("api/organizations/v1")]
//[Authorize(Roles = UserRoles.SystemSuperAdmin+","+UserRoles.SystemAdmin+","+UserRoles.SystemManager+","+UserRoles.SystemSupport+","+UserRoles.OrganizationSuperAdmin+","+UserRoles.OrganizationAdmin+","+UserRoles.OrganizationDoctor+","+UserRoles.OrganizationDoctor+","+UserRoles.OrganizationNurse)]
[Authorize]
public class OrganizationController : ControllerBase
{
    #region Fields
    
    private readonly IMediator _mediator; 
    private readonly ILogger<OrganizationController> _logger;
    
    #endregion

    #region Constructor

    public OrganizationController(
        IMediator mediator,
        ILogger<OrganizationController> logger)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    #endregion

    #region APIs

    [HttpPost("CreateOrganization")]
    public async Task<IActionResult> CreateOrganization(CreateOrganizationCommand command)
    {
        var result = await _mediator.Send(command);
        return StatusCode(result.StatusCode, result);
    }
    
    [HttpGet("GetAllOrganizations")]
    public async Task<ActionResult<IEnumerable<OrganizationDto>>> GetAll([FromQuery] GetAllOrganizationsQuery query)
    {
        var result = await _mediator.Send(query);
        return StatusCode(result.StatusCode, result);
    }
    
    [HttpGet("GetOrganizationById/{OrganizationId}")]
    public async Task<IActionResult> GetById([FromRoute]Guid OrganizationId)
    {
        var result = await _mediator.Send(new GetOrganizationByIdQuery(OrganizationId));
        return StatusCode(result.StatusCode, result);
    }
    
    [HttpDelete("DeleteOrganizationById/{OrganizationId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteOrganization([FromRoute] Guid OrganizationId)
    {
        var result = await _mediator.Send(new DeleteOrganizationCommand(OrganizationId));
        return StatusCode(result.StatusCode, result);
    }

    #endregion
    
}
