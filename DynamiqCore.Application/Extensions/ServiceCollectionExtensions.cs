using DynamiqCore.Application.JWT;
using DynamiqCore.Application.Users;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DynamiqCore.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        var applicationAssembly = typeof(ServiceCollectionExtensions).Assembly;

        services.AddMediatR(configuration => configuration.RegisterServicesFromAssembly(applicationAssembly));
        services.AddAutoMapper(applicationAssembly);
        services.AddValidatorsFromAssembly(applicationAssembly).AddFluentValidationAutoValidation();

        services.AddScoped<IUserContext, UserContext>();
        
        services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
        
        services.AddHttpContextAccessor();
    }
}