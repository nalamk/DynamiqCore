namespace DynamiqCore.Application.Organizations.Queries.Dtos;

public class OrganizationDto
{
    public Guid? OrganizationId { get; set; }
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public string Country { get; set; } = "United States"; // Default to "United States"
    public string? City { get; set; }
    public string? Street { get; set; }
    public string? State { get; set; } // Use 2-letter state abbreviations
    public string? PostalCode { get; set; } // ZIP code (5-digit or ZIP+4 format)
    public string? UnitNumber { get; set; } // Optional
}