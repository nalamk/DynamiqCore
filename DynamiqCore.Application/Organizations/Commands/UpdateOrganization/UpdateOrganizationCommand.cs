using MediatR;

namespace DynamiqCore.Application.Organizations.Commands.UpdateOrganization;

public class UpdateOrganizationCommand : IRequest
{
    public Guid OrganizationId { get; set; }
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
}