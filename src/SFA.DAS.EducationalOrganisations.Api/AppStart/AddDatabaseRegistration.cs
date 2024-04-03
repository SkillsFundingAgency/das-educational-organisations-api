using Azure.Identity;
using Microsoft.Azure.Services.AppAuthentication;
using Microsoft.EntityFrameworkCore;
using SFA.DAS.EducationalOrganisations.Data;
using SFA.DAS.EducationalOrganisations.Data.Repository;
using SFA.DAS.EducationalOrganisations.Domain.Configuration;
using SFA.DAS.EducationalOrganisations.Domain.Interfaces;

namespace SFA.DAS.EducationalOrganisations.Api.AppStart;

public static class DatabaseExtensions
{
    public static void AddDatabaseRegistration(this IServiceCollection services, EducationalOrganisationsConfiguration config, string? environmentName)
    {
        var serviceProvider = services.BuildServiceProvider();

        var logger = serviceProvider.GetLogger(nameof(Program));

        logger.LogInformation("Logger added in AddDatabaseRegistration");
        logger.LogInformation("EnvironmentName: {environmentName}", environmentName);
        logger.LogInformation("EnvironmentName: {DatabaseConnectionString}", config.DatabaseConnectionString);

        services.AddHttpContextAccessor();
        if (environmentName.Equals("DEV", StringComparison.CurrentCultureIgnoreCase))
        {
            services.AddDbContext<EducationalOrganisationDataContext>(options => options.UseInMemoryDatabase("SFA.DAS.EducationalOrganisation"), ServiceLifetime.Transient);
        }
        else if (environmentName.Equals("LOCAL", StringComparison.CurrentCultureIgnoreCase))
        {
            services.AddDbContext<EducationalOrganisationDataContext>(options => options.UseSqlServer(config.DatabaseConnectionString), ServiceLifetime.Transient);
        }
        else
        {
            services.AddDbContext<EducationalOrganisationDataContext>(ServiceLifetime.Transient);
        }

        services.AddSingleton(new EnvironmentConfiguration(environmentName));

        services.AddScoped<IEducationalOrganisationDataContext, EducationalOrganisationDataContext>(provider => provider.GetService<EducationalOrganisationDataContext>()!);
        services.AddScoped(provider => new Lazy<EducationalOrganisationDataContext>(provider.GetService<EducationalOrganisationDataContext>()!));
        services.AddSingleton(new ChainedTokenCredential(
            new ManagedIdentityCredential(),
            new AzureCliCredential(),
            new VisualStudioCodeCredential(),
            new VisualStudioCredential())
        );

        services.AddTransient<IImportAuditRepository, ImportAuditRepository>();
        services.AddTransient<IEducationalOrganisationImportRepository, EducationalOrganisationImportRepository>();
        services.AddTransient<IEducationalOrganisationEntityRepository, EducationalOrganisationEntityRepository>();
    }
}