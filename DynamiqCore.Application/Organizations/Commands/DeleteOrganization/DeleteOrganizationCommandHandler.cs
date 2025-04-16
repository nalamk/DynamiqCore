using AutoMapper;
using DynamiqCore.Application.Organizations.Commands.DeleteOrganization.Dtos;
using DynamiqCore.Domain.Repositories;
using DynamiqCore.Shared.Results;
using MediatR;
using Microsoft.Extensions.Logging;

namespace DynamiqCore.Application.Organizations.Commands.DeleteOrganization;

public class DeleteOrganizationCommandHandler: IRequestHandler<DeleteOrganizationCommand ,Result<DeleteOrganizationDto>>
{
    #region Fields

    private readonly IOrganizationsRepository _organizationsRepository;
    private readonly ILogger<DeleteOrganizationCommandHandler> _logger;
    private readonly IMapper _mapper;

    #endregion

    #region Constructor

    public DeleteOrganizationCommandHandler(
        IOrganizationsRepository organizationsRepository,
        ILogger<DeleteOrganizationCommandHandler> logger,
        IMapper mapper)
    {
        _organizationsRepository = organizationsRepository ?? throw new ArgumentNullException(nameof(organizationsRepository));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    #endregion

    #region Handler
    
    public async Task<Result<DeleteOrganizationDto>> Handle(DeleteOrganizationCommand request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Deleting organization with OrganizationId: {OrganizationId}", request.OrganizationId);
        
            var organization = await _organizationsRepository.GetByOrganizationIdAsync(request.OrganizationId);
            if (organization is null)
            {
                return Result<DeleteOrganizationDto>.InternalServerError("Organization not found to delete");
            }
            
            await _organizationsRepository.Delete(organization);

            var deleteOrganizationDto = _mapper.Map<DeleteOrganizationDto>(organization);
            
            deleteOrganizationDto.Message = "Organization deleted successfully";
            return Result<DeleteOrganizationDto>.Success(deleteOrganizationDto);
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Error deleting organization");
            return Result<DeleteOrganizationDto>.InternalServerError("Error deleting organization");
        }
    }
    
    #endregion
    
}