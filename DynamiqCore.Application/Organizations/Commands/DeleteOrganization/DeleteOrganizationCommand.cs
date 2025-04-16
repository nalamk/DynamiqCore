using DynamiqCore.Application.Organizations.Commands.DeleteOrganization.Dtos;
using DynamiqCore.Shared.Results;
using MediatR;

namespace DynamiqCore.Application.Organizations.Commands.DeleteOrganization;

public class DeleteOrganizationCommand : IRequest<Result<DeleteOrganizationDto>>
{
    public DeleteOrganizationCommand(Guid organizationId)
    {
        OrganizationId = organizationId;
    }
    
    public Guid OrganizationId { get; set; }
    
}