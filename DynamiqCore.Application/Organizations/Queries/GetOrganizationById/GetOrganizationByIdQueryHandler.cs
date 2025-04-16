using AutoMapper;
using DynamiqCore.Application.Organizations.Queries.Dtos;
using DynamiqCore.Domain.Entities;
using DynamiqCore.Domain.Exceptions;
using DynamiqCore.Domain.Repositories;
using DynamiqCore.Shared.Results;
using MediatR;
using Microsoft.Extensions.Logging;

namespace DynamiqCore.Application.Organizations.Queries.GetOrganizationById;

public class GetOrganizationByIdQueryHandler: IRequestHandler<GetOrganizationByIdQuery, Result<OrganizationDto>>
{
    #region Fields

    private readonly ILogger<GetOrganizationByIdQueryHandler> _logger;
    private readonly IOrganizationsRepository _restaurantsRepository;
    private readonly IMapper _mapper;
    
    #endregion

    #region Constructor

    public GetOrganizationByIdQueryHandler(
        IMapper mapper,
        IOrganizationsRepository restaurantsRepository,
        ILogger<GetOrganizationByIdQueryHandler> logger)
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _restaurantsRepository = restaurantsRepository ?? throw new ArgumentNullException(nameof(restaurantsRepository));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    #endregion

    #region Handler

    public async Task<Result<OrganizationDto>> Handle(GetOrganizationByIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Getting Organization {OrganizationId}", request.OrganizationId);
            
            if(!request.OrganizationId.HasValue || request.OrganizationId.Value == Guid.Empty)
            {
                return Result<OrganizationDto>.InternalServerError("OrganizationId can not be empty.");
            }
            
            var restaurant = await _restaurantsRepository.GetByOrganizationIdAsync(request.OrganizationId) 
                             ?? throw new NotFoundException(nameof(Organization), request.OrganizationId.ToString());

            var restaurantDto = _mapper.Map<OrganizationDto>(restaurant);

            // restaurantDto.LogoSasUrl = blobStorageService.GetBlobSasUrl(restaurant.LogoUrl);
            return Result<OrganizationDto>.Success(restaurantDto);
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Error fetching organizations by id");
            return Result<OrganizationDto>.InternalServerError("Error fetching organizations by id");
        }
        
    }

    #endregion
    
    
}