using DynamiqCore.Application.Common;
using DynamiqCore.Application.Organizations.Queries.Dtos;
using DynamiqCore.Domain.Constants;
using DynamiqCore.Shared.Results;
using MediatR;

namespace DynamiqCore.Application.Organizations.Queries.GetAllOrganizations;

public class GetAllOrganizationsQuery : IRequest<Result<PagedResult<OrganizationDto>>>
{
    public string? SearchPhrase { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public string? SortBy { get; set; }
    public SortDirection SortDirection { get; set; }
}