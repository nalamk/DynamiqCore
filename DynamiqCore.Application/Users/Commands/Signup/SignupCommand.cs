using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using DynamiqCore.Application.Users.Commands.Signup.Dtos;
using DynamiqCore.Domain.Enums;
using DynamiqCore.Shared.Results;
using MediatR;

namespace DynamiqCore.Application.Users.Commands.Signup;

public class SignupCommand : IRequest<Result<SignupDto>>
{

    [DefaultValue("")]
    public string? Username { get; set; }
    
    [Required]
    [DefaultValue("")]
    public string Email { get; set; }
    
    [DefaultValue("")]
    public string Password { get; set; }
    
    [DefaultValue("00000000-0000-0000-0000-000000000000")]
    public Guid? OrganizationId { get; set; }

    [DefaultValue(UserRoles.SystemSuperAdmin)]
    public UserRoles UserRoles { get; set; }
    
    public SignupCommand(string username, string email, string password, Guid? organizationId, UserRoles userRoles = UserRoles.SystemSuperAdmin)
    {
        Username = username;
        Email = email;
        Password = password;
        OrganizationId = organizationId;
        UserRoles = userRoles;
    }
}