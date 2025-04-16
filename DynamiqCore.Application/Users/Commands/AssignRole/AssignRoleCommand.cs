using DynamiqCore.Application.Users.Dtos.Role;
using DynamiqCore.Shared.Results;
using MediatR;

namespace DynamiqCore.Application.Users.Commands.AssignRole;

public class AssignRoleCommand : IRequest<Result<AssignRoleDto>>
{
    public string RoleName { get; set; } = default!;
}
