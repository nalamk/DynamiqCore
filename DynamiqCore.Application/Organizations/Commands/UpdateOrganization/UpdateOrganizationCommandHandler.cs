using AutoMapper;
using DynamiqCore.Domain.Entities;
using DynamiqCore.Domain.Exceptions;
using DynamiqCore.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace DynamiqCore.Application.Organizations.Commands.UpdateOrganization;

public class UpdateOrganizationCommandHandler(
    ILogger<UpdateOrganizationCommandHandler> logger,
    IOrganizationsRepository organizationsRepository,
    IMapper mapper) : IRequestHandler<UpdateOrganizationCommand>
{
    public async Task Handle(UpdateOrganizationCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Updating organization with OrganizationID: {OrganizationID} with {@UpdatedOrganization}", request.OrganizationId, request);
        var organization = await organizationsRepository.GetByOrganizationIdAsync(request.OrganizationId);
        if (organization is null)
            throw new NotFoundException(nameof(Organization), request.OrganizationId.ToString());


        mapper.Map(request, organization);
        //restaurant.Name = request.Name;
        //restaurant.Description = request.Description;
        //restaurant.HasDelivery = request.HasDelivery;

        await organizationsRepository.SaveChanges();
    }
}