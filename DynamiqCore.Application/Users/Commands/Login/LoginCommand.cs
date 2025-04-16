using System.ComponentModel;
using DynamiqCore.Application.Users.Dtos.Login;
using DynamiqCore.Shared.Results;
using MediatR;

namespace DynamiqCore.Application.Users.Commands.Login;

public class LoginCommand : IRequest<Result<LoginDto>>
{
    [DefaultValue("")]
    public string UsernameOrEmail { get; set; } = default!;
    
    [DefaultValue("")]
    public string Password { get; set; } = default!;
    
    [DefaultValue("00000000-0000-0000-0000-000000000000")]
    public Guid? OrganizationId { get; set; }
    
    [DefaultValue("00000000-0000-0000-0000-000000000000")]
    public Guid? PatientId { get; set; }
    public bool RememberMe { get; set; } = default!;

    public LoginCommand(string usernameOrEmail, string password,Guid? organizationId, Guid? patientId , bool rememberMe = false)
    {
        UsernameOrEmail = usernameOrEmail;
        Password = password;
        OrganizationId = organizationId;
        PatientId = patientId;
        RememberMe = rememberMe;
    }
}
