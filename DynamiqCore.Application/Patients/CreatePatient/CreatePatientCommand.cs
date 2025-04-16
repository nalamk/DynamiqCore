using System.ComponentModel;
using DynamiqCore.Application.Users.Commands.PatientSignup.Dtos;
using DynamiqCore.Shared.Results;
using MediatR;

namespace DynamiqCore.Application.Users.Commands.PatientSignup;

public class CreatePatientCommand : IRequest<Result<CreatePatientDto>>
{
    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string FirstName { get; set; }
    public string? MiddleName { get; set; }
    public string LastName { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public string Gender { get; set; }
    public string MaritalStatus { get; set; }
    public string BloodType { get; set; }
    public string Nationality { get; set; }
    public string PreferredLanguage { get; set; }
    public string Country { get; set; }
    public string? City { get; set; }
    public string? Street { get; set; }
    public string? State { get; set; }
    public string? PostalCode { get; set; }
    
    [DefaultValue("00000000-0000-0000-0000-000000000000")]
    public Guid? OrganizationId { get; set; }
    
    public CreatePatientCommand(
        string firstName, 
        string middleName, 
        string lastName, 
        DateOnly dateOfBirth,
        string gender,
        string maritalStatus,
        string bloodType,
        string nationality,
        string preferredLanguage,
        string country,
        string? city,
        string? street,
        string? state,
        string? postalCode, 
        Guid? organizationId)
    {
        FirstName = firstName;
        MiddleName = middleName;
        LastName = lastName;
        DateOfBirth = dateOfBirth;
        Gender = gender;
        MaritalStatus = maritalStatus;
        BloodType = bloodType;
        Nationality = nationality;
        PreferredLanguage = preferredLanguage;
        Country = country;
        City = city;
        Street = street;
        State = state;
        PostalCode = postalCode;
        OrganizationId = organizationId;
    }
}