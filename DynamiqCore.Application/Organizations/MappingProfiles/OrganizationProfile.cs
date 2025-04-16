using AutoMapper;
using DynamiqCore.Application.Organizations.Commands.CreateOrganization;
using DynamiqCore.Application.Organizations.Commands.CreateOrganization.Dtos;
using DynamiqCore.Application.Organizations.Commands.DeleteOrganization.Dtos;
using DynamiqCore.Application.Organizations.Queries.Dtos;
using DynamiqCore.Domain.Entities;

namespace DynamiqCore.Application.Organizations.MappingProfiles;

public class OrganizationProfile : Profile
{
    public OrganizationProfile()
    {
        CreateMap<CreateOrganizationCommand, Organization>()
            .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => "DefaultUser"))
            .ForPath(dest => dest.Address.Country, opt => opt.MapFrom(src => src.Country))
            .ForPath(dest => dest.Address.City, opt => opt.MapFrom(src => src.City))
            .ForPath(dest => dest.Address.Street, opt => opt.MapFrom(src => src.Street))
            .ForPath(dest => dest.Address.State, opt => opt.MapFrom(src => src.State))
            .ForPath(dest => dest.Address.PostalCode, opt => opt.MapFrom(src => src.PostalCode))
            .ForPath(dest => dest.Address.UnitNumber, opt => opt.MapFrom(src => src.UnitNumber))
            .ReverseMap();
        
        CreateMap<Organization, OrganizationDto>()
            .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Address.Country))
            .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.Address.City))
            .ForMember(dest => dest.Street, opt => opt.MapFrom(src => src.Address.Street))
            .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.Address.State))
            .ForMember(dest => dest.PostalCode, opt => opt.MapFrom(src => src.Address.PostalCode))
            .ForMember(dest => dest.UnitNumber, opt => opt.MapFrom(src => src.Address.UnitNumber))
            .ReverseMap();
        
        CreateMap<Organization, CreateOrganizationDto>()
            .ReverseMap();
        
        CreateMap<Organization, DeleteOrganizationDto>()
            .ReverseMap();
    }
}