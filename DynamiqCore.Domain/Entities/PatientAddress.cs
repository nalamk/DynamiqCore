using System.ComponentModel.DataAnnotations;

namespace DynamiqCore.Domain.Entities;

public class PatientAddress
{
    [StringLength(100)]
    public string Country { get; set; }
    
    [StringLength(100)]
    public string? City { get; set; }
    
    [StringLength(200)]
    public string? Street { get; set; }
    
    [StringLength(200)]
    public string? State { get; set; }
    
    [StringLength(20)]
    public string? PostalCode { get; set; }
    
    
}