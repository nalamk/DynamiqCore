using System.ComponentModel.DataAnnotations;

namespace DynamiqCore.Domain.Entities;

public class OrganizationAddress
{
    [Required, StringLength(100)]
    public string Country { get; set; } = "United States"; // Default to "United States"
    
    [StringLength(100)]
    public string? City { get; set; }
    
    [StringLength(200)]
    public string? Street { get; set; }
    
    [StringLength(2)]
    public string? State { get; set; } // Use 2-letter state abbreviations
    
    [StringLength(10)]
    public string? PostalCode { get; set; } // ZIP code (5-digit or ZIP+4 format)
    
    [StringLength(50)]
    public string? UnitNumber { get; set; } // Optional
}