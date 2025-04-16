using DynamiqCore.Application.Organizations.Queries.Dtos;
using DynamiqCore.Shared.Results;
using MediatR;

namespace DynamiqCore.Application.Organizations.Queries.GetOrganizationById;

public class GetOrganizationByIdQuery : IRequest<Result<OrganizationDto>>
{
    public GetOrganizationByIdQuery(Guid organizationId)
    {
        OrganizationId = organizationId;
    }
    public Guid? OrganizationId { get; set; }
}