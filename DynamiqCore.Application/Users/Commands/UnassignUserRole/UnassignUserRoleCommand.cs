using MediatR;

namespace DynamiqCore.Application.Users.Commands.UnassignUserRole;

public class UnassignUserRoleCommand : IRequest
{
    public string UserEmail { get; set; } = default!;
    public string RoleName { get; set; } = default!;
}