using AutoMapper;
using DynamiqCore.Application.Organizations.Commands.CreateOrganization.Dtos;
using DynamiqCore.Domain.Entities;
using DynamiqCore.Domain.Repositories;
using DynamiqCore.Shared.Results;
using MediatR;
using Microsoft.Extensions.Logging;

namespace DynamiqCore.Application.Organizations.Commands.CreateOrganization;

public class CreateOrganizationCommandHandler : IRequestHandler<CreateOrganizationCommand, Result<CreateOrganizationDto>>
{
    #region Fields
    
    private readonly IMapper _mapper;
    private readonly IOrganizationsRepository _organizationsRepository;
    private readonly ILogger<CreateOrganizationCommandHandler> _logger; 
    
    #endregion

    #region Constructor

    public CreateOrganizationCommandHandler(
        IMapper mapper,
        IOrganizationsRepository organizationsRepository,
        ILogger<CreateOrganizationCommandHandler> logger)
    {
        _mapper = mapper ?? throw new NullReferenceException(nameof(mapper));
        _organizationsRepository = organizationsRepository ?? throw new NullReferenceException(nameof(organizationsRepository));
        _logger = logger ?? throw new NullReferenceException(nameof(logger));
    }

    #endregion
    
    
   public async Task<Result<CreateOrganizationDto>> Handle(CreateOrganizationCommand request, CancellationToken cancellationToken)
   {
       try
       {
           var organization = _mapper.Map<Organization>(request);
           await _organizationsRepository.Create(organization);
           _logger.LogInformation("Organization created successfully with ID: {OrganizationId}", organization.OrganizationId);
           var createOrganizationDto = _mapper.Map<CreateOrganizationDto>(organization);
           createOrganizationDto.Message = "Organization created successfully";
           return Result<CreateOrganizationDto>.Success(createOrganizationDto);
       }
       catch (Exception exception)
       {
           _logger.LogError(exception, "Error occurred while creating organization");
           return Result<CreateOrganizationDto>.InternalServerError("Internal server error occured while creating organization");
       }
   }
}