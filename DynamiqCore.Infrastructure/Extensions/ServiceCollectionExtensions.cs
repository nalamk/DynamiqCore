using Azure.Communication.Email;
using DynamiqCore.Application.AEC;
using DynamiqCore.Domain.Entities;
using DynamiqCore.Domain.Interfaces;
using DynamiqCore.Domain.Repositories;
using DynamiqCore.Infrastructure.Persistence;
using DynamiqCore.Infrastructure.Repositories;
using DynamiqCore.Infrastructure.Seeders;
using DynamiqCore.Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DynamiqCore.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DynamiqCoreDb");
        services.AddDbContext<DynamiqCoreDbContext>(options => options.UseSqlServer(connectionString));

        services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<DynamiqCoreDbContext>()
            .AddDefaultTokenProviders();

        services.AddScoped<ISeeder, Seeder>();

        // Bind AzureCommunicationSettings
        var azureCommunicationSettings = new AzureCommunicationSettings();
        configuration.Bind(nameof(AzureCommunicationSettings), azureCommunicationSettings);
        services.AddSingleton(azureCommunicationSettings);

        // Register EmailClient
        services.AddSingleton(provider =>
        {
            var settings = provider.GetRequiredService<AzureCommunicationSettings>();
            return new EmailClient(settings.ConnectionString);
        });
        
        // Register EmailService with dependencies
        services.AddScoped<IEmailService, EmailService>(provider =>
        {
            var emailClient = provider.GetRequiredService<EmailClient>();
            var azureCommunicationSettings = provider.GetRequiredService<AzureCommunicationSettings>();
            return new EmailService(emailClient, azureCommunicationSettings.SenderEmail);
        });

        services.AddScoped<IOrganizationsRepository, OrganizationsRepository>();
        services.AddScoped<IPatientRepository, PatientRepository>();
    }
}