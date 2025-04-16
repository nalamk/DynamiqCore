using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using DynamiqCore.Application.Users.Commands.OrganizationSignup.Dtos;
using DynamiqCore.Domain.Enums;
using DynamiqCore.Shared.Results;
using MediatR;

namespace DynamiqCore.Application.Users.Commands.OrganizationSignup;

public class OrganizationSignupCommand : IRequest<Result<OrganizationSignupDto>>
{
        public string? Username { get; set; }
    
        [Required]
        public string Email { get; set; }
    
        public string Password { get; set; }
    
        [DefaultValue("00000000-0000-0000-0000-000000000000")]
        public Guid? OrganizationId { get; set; }

        [DefaultValue(UserRoles.OrganizationAdmin)]
        public UserRoles UserRoles { get; set; }
    
        public OrganizationSignupCommand(string username, string email, string password, Guid? organizationId)
        {
            Username = username;
            Email = email;
            Password = password;
            OrganizationId = organizationId;
        }
    
}