using AutoMapper;
using DynamiqCore.Application.Users.Commands.PatientSignup;
using DynamiqCore.Application.Users.Commands.PatientSignup.Dtos;
using DynamiqCore.Domain.Entities;

namespace DynamiqCore.Application.Users.Commands.PatientRegistration.MappingProfiles;

public class PatientProfile : Profile
{

    public PatientProfile()
    {
        CreateMap<CreatePatientCommand, Patient>()
            .ReverseMap();

        CreateMap<Patient, CreatePatientDto>()
            .ReverseMap();
    }
    
}