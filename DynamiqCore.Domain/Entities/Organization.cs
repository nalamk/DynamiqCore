using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DynamiqCore.Domain.Entities;

public class Organization
{
    [Key, Required, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid OrganizationId { get; set; }
    [Required, MaxLength(500)]
    public string Name { get; set; } = default!;
    
    [MaxLength(500)]
    public string? Description { get; set; }
    
    [Required, Timestamp, ConcurrencyCheck, DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public byte[] RowVersion { get; set; }

    [Required] 
    public DateTime CreateAt { get; set; } = default!;

    public DateTime? UpdateAt { get; set; }

    [Required, MaxLength(500)]
    public string CreatedBy { get; set; } = default!;

    [MaxLength(500)]
    public string? UpdatedBy { get; set; }
    public ICollection<Patient>? Patients { get; set; } // Collection of patients belonging to this organization
    public OrganizationAddress Address { get; set; }
    
}