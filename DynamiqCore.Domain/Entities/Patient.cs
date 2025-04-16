using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DynamiqCore.Domain.Entities;

public class Patient
{
    [Key, Required, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid PatientId { get; set; }
    
    [Required]
    [StringLength(250)]
    public string FirstName { get; set; }

    [StringLength(250)]
    public string? MiddleName { get; set; }

    [Required]
    [StringLength(250)]
    public string LastName { get; set; }

    [Required]
    public DateOnly DateOfBirth { get; set; }

    [StringLength(10)]
    public string Gender { get; set; }

    [StringLength(20)]
    public string MaritalStatus { get; set; }

    [StringLength(5)]
    public string BloodType { get; set; }
    
    [StringLength(100)]
    public string Nationality { get; set; }

    [StringLength(50)]
    public string PreferredLanguage { get; set; }

    public PatientAddress Address { get; set; }
    
    [Required, Timestamp, ConcurrencyCheck, DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public byte[] RowVersion { get; set; }

    [Required] 
    public DateTime CreateAt { get; set; } = default!;

    public DateTime? UpdateAt { get; set; }

    [Required, MaxLength(500)]
    public string CreatedBy { get; set; } = default!;

    [MaxLength(500)]
    public string? UpdatedBy { get; set; }
    
    [Required] 
    public Guid OrganizationId { get; set; }

    public Organization Organization { get; set; }
}