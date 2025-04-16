using DynamiqCore.Application.Users.Commands.PatientSignup;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DynamiqCore.API.Controllers;

[ApiController]
[Authorize]
[Route("api/patients/v1")]
public class PatientController : ControllerBase
{
    #region Fields
    
    private readonly IMediator _mediator; 
    private readonly ILogger<PatientController> _logger;
    
    #endregion

    #region Constructor 

    public PatientController(
        IMediator mediator,
        ILogger<PatientController> logger)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    #endregion

    #region APIs 

    [HttpPost("CreatePatient")]
    public async Task<IActionResult> CreatePatient(CreatePatientCommand command)
    {
        var result = await _mediator.Send(command);
        return StatusCode(result.StatusCode, result);
    }

    #endregion
    
}
