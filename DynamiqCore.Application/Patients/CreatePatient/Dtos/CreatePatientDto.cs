namespace DynamiqCore.Application.Users.Commands.PatientSignup.Dtos;

public class CreatePatientDto
{
    public string Email { get; set; }
    public string Username { get; set; }
    public Guid PatientId { get; set; } 
    public Guid OrganizationId { get; set; }
    public string Message { get; set; }
    
}