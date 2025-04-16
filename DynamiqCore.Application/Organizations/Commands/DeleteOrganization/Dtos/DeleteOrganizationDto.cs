namespace DynamiqCore.Application.Organizations.Commands.DeleteOrganization.Dtos;

public class DeleteOrganizationDto
{
    public Guid OrganizationId { get; set; }
    public string Message { get; set; } = default!;
}