using AutoMapper;
using DynamiqCore.Application.Common;
using DynamiqCore.Application.Organizations.Queries.Dtos;
using DynamiqCore.Domain.Repositories;
using DynamiqCore.Shared.Results;
using MediatR;
using Microsoft.Extensions.Logging;

namespace DynamiqCore.Application.Organizations.Queries.GetAllOrganizations;

public class GetAllOrganizationsQueryHandler : IRequestHandler<GetAllOrganizationsQuery, Result<PagedResult<OrganizationDto>>>
{
    #region Fields

    private readonly IOrganizationsRepository _organizationsRepository;
    private readonly ILogger<GetAllOrganizationsQueryHandler> _logger;
    private readonly IMapper _mapper;
    
    #endregion

    #region Constructor

    public GetAllOrganizationsQueryHandler(
        IOrganizationsRepository organizationsRepository,
        IMapper mapper,
        ILogger<GetAllOrganizationsQueryHandler> logger
    )
    {
        _organizationsRepository = organizationsRepository ?? throw new ArgumentNullException(nameof(organizationsRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    #endregion

    #region Handler

    public async Task<Result<PagedResult<OrganizationDto>>> Handle(GetAllOrganizationsQuery request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Getting all organizations");
        
            var (organizations, totalCount) = await _organizationsRepository.GetAllMatchingAsync(request.SearchPhrase,
                request.PageSize,
                request.PageNumber,
                request.SortBy,
                request.SortDirection);

            var organizationDtos = _mapper.Map<IEnumerable<OrganizationDto>>(organizations);

            var result = new PagedResult<OrganizationDto>(organizationDtos, totalCount, request.PageSize, request.PageNumber);
            return Result<PagedResult<OrganizationDto>>.Success(result);
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Error fetching organizations");
            return Result<PagedResult<OrganizationDto>>.InternalServerError("Error fetching organizations");
        }
        
    }

    #endregion
    
}