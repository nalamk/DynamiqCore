using Microsoft.AspNetCore.Identity;

namespace DynamiqCore.Domain.Entities;

public class ApplicationUser : IdentityUser
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public DateOnly? DateOfBirth { get; set; }
    public string? Nationality { get; set; }
    public Guid? OrganizationId { get; set; }
    public Organization? Organization { get; set; }
}