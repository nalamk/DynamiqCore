using MediatR;

namespace DynamiqCore.Application.Users.Commands.AssignUserRole;

public class AssignUserRoleCommand : IRequest
{
    public string UsernameOrEmail { get; set; }
    public string RoleName { get; set; }
}