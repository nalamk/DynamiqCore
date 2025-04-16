using DynamiqCore.Application.Organizations.Commands.CreateOrganization.Dtos;
using DynamiqCore.Application.Users.Commands.PatientSignup;
using DynamiqCore.Shared.Results;
using MediatR;

namespace DynamiqCore.Application.Organizations.Commands.CreateOrganization;

public class CreateOrganizationCommand : IRequest<Result<CreateOrganizationDto>>
{
    public string Name { get; set; } = default!;
    
    public string? Description { get; set; }
    
    public string Country { get; set; } = "United States"; // Default to "United States"
    
    public string? City { get; set; }
    
    public string? Street { get; set; }
    
    public string? State { get; set; } // Use 2-letter state abbreviations
    
    public string? PostalCode { get; set; } // ZIP code (5-digit or ZIP+4 format)
    
    public string? UnitNumber { get; set; } // Optional
}