using DynamiqCore.Application.Users.Commands.ActivateAccount;
using DynamiqCore.Application.Users.Commands.AssignRole;
using DynamiqCore.Application.Users.Commands.ForgotPassword;
using DynamiqCore.Application.Users.Commands.Login;
using DynamiqCore.Application.Users.Commands.ResetPassword;
using DynamiqCore.Application.Users.Commands.Signup;
using DynamiqCore.Domain.Constants;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DynamiqCore.API.Controllers;

[ApiController]
[Route("api/account/v1")]
public class AccountController : ControllerBase
{
    #region Fields

    private readonly IMediator _mediator;

    #endregion

    #region Constructor

    public AccountController(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    #endregion

    #region APIs

    [HttpPost("Login")]
    // [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] LoginCommand command)
    {
        var result = await _mediator.Send(command);
        return StatusCode(result.StatusCode, result);
    }
    
    [HttpPost("Test")]
    [Authorize(Roles="Admin", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public IActionResult Test([FromBody]string value)
    {
        return Ok(value);
    }

    [HttpPost("Signup")]
    //[AllowAnonymous]
    public async Task<IActionResult> Signup([FromBody] SignupCommand command)
    {
        var result = await _mediator.Send(command);
        return StatusCode(result.StatusCode, result);
    }

    [HttpPost("ForgotPassword")]
    public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordCommand command)
    {
        var result = await _mediator.Send(command);
        return StatusCode(result.StatusCode, result);
    }
    
    [HttpPost("ResetPassword")]
    public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordCommand command)
    {
        var result = await _mediator.Send(command);
        return StatusCode(result.StatusCode, result);
    }
    
    [HttpPost("AddRole")]
    public async Task<IActionResult> AddRole([FromBody] AssignRoleCommand command)
    {
        var result = await _mediator.Send(command);
        return StatusCode(result.StatusCode, result);
    }

    [HttpGet("ActivateAccount")]
    //[AllowAnonymous]
    public async Task<IActionResult> ActivateAccount([FromQuery] string userId, [FromQuery] string token)
    {
        var command = new ActivateAccountCommand(userId, token);
        var result = await _mediator.Send(command);
        return StatusCode(result.StatusCode, result);
    }

    #endregion

}